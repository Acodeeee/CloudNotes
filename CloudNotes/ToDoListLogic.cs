using System;
namespace CloudNotes
{
	public class ToDoListLogic
	{
		private static string NAME;//存储用户名

		internal static void ManageNotes(string name)
		{
			NAME = name;

			while (true)
			{
				ToDoList.GetInstance(name).ShowOnlyTheme();
				Console.WriteLine("-------------------------------");
				Console.WriteLine("当前用户：" + NAME);
				Console.WriteLine("-------------------------------");
				Console.Write("1.增加备忘录  2.删除备忘录  3.按等级排序  4.按日期排序  5.查看备忘录内容  0.注销账户\n请输入：");
				switch (Console.ReadLine())
				{
					case "1":
						AddNote();
						break;

					case "2":
						RemoveNote();
						break;

					case "3":
						ToDoList.GetInstance(NAME).SortByPriority();
						break;

					case "4":
						ToDoList.GetInstance(NAME).SortByDate();
						break;

					case "5":
						ShowDetail();
						break;

					case "0":
						//
						MainClass.Main(null);
						break;

					default:
						Console.WriteLine("输入错误，请重新输入！");
						break;
				}
			}
		}

		//增加备忘录
		private static void AddNote() { 
			Console.WriteLine("请输入待办事项：");
			string subject = Console.ReadLine();
			Console.WriteLine("请输入备注：");
			string description = Console.ReadLine();
			Console.WriteLine("请输入优先级(1-10)：");
			int priority = 0;
			try
			{
				priority = int.Parse(Console.ReadLine());
			}
			catch {
				Console.WriteLine("优先级输入格式错误，已默认为1");
				priority = 1;
			}
			Console.WriteLine("请输入日期(格式：2000-01-01)：");
			string date = Console.ReadLine();

			ToDoList.GetInstance(NAME).AddToDo(new ToDo(subject, description, priority, date));


		}

		//删除备忘录
		private static void RemoveNote()
		{
			int num = -1;
			while (true)
			{
				Console.Write("请输入要删除的备忘录序号（0全部删除）：");
				try
				{
					num = int.Parse(Console.ReadLine());
					break;
				}
				catch
				{
					Console.WriteLine("输入错误，请重新输入！");
				}
			}

			if (num == 0)
			{
				//全部删除
				string code = LogOrRegistLogic.GetSecurityCode();   //获取验证码
				Console.WriteLine("验证码：" + code);
				Console.WriteLine("请输入验证码：");

				if (Console.ReadLine().Equals(code))
				{
					ToDoList.GetInstance(NAME).Clear();
				}
				else
				{
					Console.WriteLine("验证码错误！");
				}
			}
			else
			{
				ToDoList.GetInstance(NAME).RemoveToDoByIndex(num - 1);
			}
		}

		//查看备忘录内容
		private static void ShowDetail() {
			int num = -1;
			while (true)
			{
				Console.WriteLine("请输入要查看的备忘录序号（0查看全部备忘录内容）：");
				try
				{
					num = int.Parse(Console.ReadLine());
					break;
				}
				catch
				{
					Console.WriteLine("输入错误，请重新输入！");
				}
			}
			if (num == 0)
			{
				ToDoList.GetInstance(NAME).ShowAll();
			}
			else { 
				
			}

		}

	}
}
