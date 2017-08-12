using System;
/**
 * 笔记类
 */
namespace CloudNotes
{
	public enum State { 
		GREATER,
		EQUAL,
		LESS
	}
	public class ToDo
	{
		public string Subject { get; set; }     			//待办事务
		public string Description { get; set; }             //备注
		private int priority; 								//优先级
		public int Priority { 
			get { return priority;}
			set {
				//优先级为1-10，输入其他赋值为1
				if (value > 0 && value < 11)
				{
					priority = value;
				}
				else {
					priority = 1;
				}
			}
		}                  
		private string date;
		public string Date {
			get { return date;}
			set {   //日期输入格式必须为2017*08*10***...,其中*可以为任意字符，都格式化为2017-08-10的格式。否则赋值为0000-00-00.这里没有对日期的合理性进行检测
				try
				{
					date = int.Parse(value.Substring(0, 4)).ToString().PadLeft(4,'0') + "-";
					date += int.Parse(value.Substring(5, 2)).ToString().PadLeft(2,'0') + "-";
					date += int.Parse(value.Substring(8, 2)).ToString().PadLeft(2,'0');
				}
				catch {
					Console.WriteLine("日期格式错误！");
					date = "0000-00-00";
				}
			}
		}        

		public ToDo(string subject, string description, int _priority, string date) {
			this.Subject = subject;
			this.Description = description;
			this.Priority = _priority;
			this.Date = date;
		}

		//根据Priority比较
		public State CompareByPriority(ToDo td) {
			//大于
			if (this.Priority > td.Priority)
			{
				return State.GREATER;
			}
			//小于
			else if (this.Priority < td.Priority)
			{
				return State.LESS;
			}
			//等于
			else {
				return State.EQUAL;
			}
		}
		//根据date比较
		public State CompareByDate(ToDo td) {
			//大于
			if (GetNumByDate(this.Date) > GetNumByDate(td.Date))
			{
				return State.GREATER;
			}
			//小于
			else if (GetNumByDate(this.Date) < GetNumByDate(td.Date))
			{
				return State.LESS;
			}
			//等于
			else {
				return State.EQUAL;
			}
		}

		//将日期转换为int返回，用于CompareByDate调用
		private int GetNumByDate(string _date) {
			//date已经在赋值时进行了类型检测，故这里不用担心异常抛出
			string str;
			//去'-'字符
			str = _date.Substring(0, 4) + _date.Substring(5, 2) + _date.Substring(8, 2);

			return int.Parse(str);
		}
		public override string ToString()
		{
			return string.Format("Subject: \n{0} \nDescription: \n{1} \nPriority: {2} \nDate: \n{3}]\n", Subject, Description, Priority, Date);
		}
	}
}
