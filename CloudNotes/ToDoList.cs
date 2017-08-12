using System;
using System.Collections.Generic;
using System.IO;

namespace CloudNotes
{
	public class ToDoList
	{
		public readonly List<ToDo> list;

		private readonly string PATH;
		private static readonly object locker = 0;


		public int Count { get { return list.Count;} }//得到list的Count


		private ToDoList(string path) {
			PATH = "./Notes/" + path + ".txt";
			list = new List<ToDo>();
            InitListByFile();
		}
		private static ToDoList Instance;
		public static ToDoList GetInstance(string path) {
			if (Instance == null) { 
				lock(locker) {
					if (Instance == null) {
						Instance = new ToDoList(path);
					}
				}
			}

			return Instance;
		}

		//读取存档
		private void InitListByFile() {
			if (!File.Exists(PATH)) {
				FileStream file = File.Create(PATH);
				file.Close();
				return;
			}
			string[] allData = File.ReadAllLines(PATH);
			string subject = null;
			string description = null;
			int priority = 0;
			string data = null;
			foreach (string item in allData)
			{

				//"/"是一条笔记结束标记
				if (item.Equals("~end")) {
					list.Add(new ToDo(subject, description, priority, data));
					//数据置空
					subject = null;
					description = null;
					priority = 0;
					data = null;
				}
				else if (item.Substring(0, 4).Equals("Date"))
				{
					data = item.Substring(5, item.Length - 5);
				}
				else if (item.Substring(0, 7).Equals("Subject"))
				{
					subject = item.Substring(8, item.Length - 8);
				}
				else if (item.Substring(0, 8).Equals("Priority"))
				{
					try
					{
						priority = int.Parse(item.Substring(9, item.Length - 9));
					}
					catch
					{
						priority = 0;
					}
				}
				else if (item.Substring(0, 11).Equals("Description"))
				{
					description = item.Substring(12, item.Length - 12);
				}
			}

		}

		//添加ToDo，不重复添加
		public void AddToDo(ToDo td) {
			list.Add(td);
			Console.WriteLine("添加备忘录成功！");
			WriteTo(td);
		}
		//写入文件
		private void WriteTo(ToDo td) { 
			FileStream file = File.Open(PATH, FileMode.Append);
			StreamWriter writer = new StreamWriter(file);
			//写入文件
			writer.WriteLine("Subject:" + td.Subject);
			writer.WriteLine("Description:" + td.Description);
			writer.WriteLine("Priority:" + td.Priority);
			writer.WriteLine("Date:" + td.Date);
			writer.WriteLine("~end");

			writer.Flush();
			writer.Close();
			file.Close();
		}

		//重写文件内容
		private void RewriteFile() {
			//删除原文件，新建文件，相当与删除文件所有内容
			File.Delete(PATH);
			FileStream file = File.Create(PATH);

			//写入文件
			StreamWriter writer = new StreamWriter(file);
			foreach (ToDo td in list) { 
				writer.WriteLine("Subject:" + td.Subject);
				writer.WriteLine("Description:" + td.Description);
				writer.WriteLine("Priority:" + td.Priority);
				writer.WriteLine("Date:" + td.Date);
				writer.WriteLine("~end");
			}
			writer.Flush();
			writer.Close();
			file.Close();
		}

		public void RemoveToDoByIndex(int index) {
			try
			{
				list.RemoveAt(index);
				Console.WriteLine("删除备忘录成功！");
				RewriteFile();
			}
			catch { 
				Console.WriteLine("index越界错误，Remove失败！");
			}

		}
		public bool RemoveToDoByItem(ToDo td) {
			if (list.Remove(td)) { 
                RewriteFile();
				return true;
			}

			return false;
		}
		public void Clear() {
			list.Clear();
			Console.WriteLine("备忘录已清空！");
			RewriteFile();
		}

		public bool Insert(int index, ToDo td) {
			try
			{
				list.Insert(index, td);
			    RewriteFile();
				return true;
			}
			catch { 
				Console.WriteLine("index越界错误，Insert失败！");
				return false;
			}

		}

		public int GetIndexByItem(ToDo td) {
			for (int i = 0; i < Count; ++i) {
				if (list[i].Equals(td)) {
					return i;
				}
			}
			Console.WriteLine("未找到匹配的item");
			return -1;
		}
		public ToDo GetItemByIndex(int index) {
			try
			{
				return list[index];
			}
			catch {
				Console.WriteLine("index越界错误，GetItemByIndex失败");
				return null;
			}
		}
		//日期从早到晚排
		public void SortByDate() {
			for (int i = 0; i < Count; ++i) {
				for (int j = 1; j < Count - i; ++j) {
					if (list[j - 1].CompareByDate(list[j]) == State.GREATER) {
						ToDo temp = list[j - 1];
						list[j - 1] = list[j];
						list[j] = temp;
					}
				}
			}
            RewriteFile();
            ShowAll();
		}
		//Priority按高到低排
		public void SortByPriority() { 
			for (int i = 0; i<Count; ++i) {
				for (int j = 1; j<Count - i; ++j) {
					if (list[j - 1].CompareByPriority(list[j]) == State.LESS) {
						ToDo temp = list[j - 1];
						list[j - 1] = list[j];
						list[j] = temp;
					}
				}
			}
            RewriteFile();
            ShowAll();
		}

		//打印所有ToDo
		public void ShowAll() {
			int i = 0;
			foreach (ToDo item in list) { 
				Console.WriteLine("---------------------第{0}条笔记-----------------------", ++i);
				Console.WriteLine(item);
				Console.WriteLine("------------------------------------------------------");
			}
		}
		//打印所有ToDo（仅标题和时间）
		public void ShowOnlyTheme()
		{
			Console.WriteLine("------------------------------------------------------");
			int i = 0;
			foreach (ToDo item in list)
			{
				Console.WriteLine(++i + ". " + item.Subject.PadRight(20) +"级别: " + item.Priority.ToString().PadRight(5) + "----" + item.Date);
				Console.WriteLine("------------------------------------------------------");
			}
			if (i == 0) { 
				Console.WriteLine("暂无笔记！");
			}
		}
		//通过index打印单条备忘录
		public void ShowByIndex(int index){
			try
			{
				Console.WriteLine(list[index]);
			}
			catch { 
				Console.WriteLine("index错误！");
			}
		}
	}
}
