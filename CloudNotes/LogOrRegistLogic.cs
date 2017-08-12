using System;

namespace CloudNotes
{
	public class LogOrRegistLogic
	{
		//用于随机生成验证码
		private const string SECURITY_CODE = "1234567890QWERTYUIOPASDFGHJKLZXCVBNM";

		private const int CODE_SIZE = 4;
		//登录 注册选择
		internal static bool LogOrRegist(out string name)
		{
			Console.WriteLine("------------------------Cloud Note--------------------------");
			while (true)
			{
				Console.WriteLine("1.登录  2.注册  0.退出\n请选择：");
				switch (Console.ReadLine())
				{
					case "1":
						//登录
						return Log(out name);

					case "2":
						//注册
						if (Regist(out name)) {
							return true;
						}

						break;

					case "0":
						name = null;
						Environment.Exit(0);//结束程序
						return false;

					default:
						Console.WriteLine("输入错误，请重写输入：");
						break;
				}
			}
		}
		//注册 return的Bool值用于确认是否登录
		private static bool Regist(out string name)
		{
			Console.Write("用户名：");
			string userName = Console.ReadLine();
			Console.Write("密码：");
			string password = EnterPasswd();
			Console.WriteLine();

			while (true)
			{
				string securityCode = GetSecurityCode();
				Console.WriteLine("验证码：" + securityCode);
				Console.Write("请输入验证码：");
				if (Console.ReadLine().Equals(securityCode))
				{
					break;
				}
				else
				{
					Console.WriteLine("验证码错误，请重新输入！");
				}
			}
			UserInfoController.GetInstance().AddUserInfo(new UserInfo(userName, password));

			while (true)
			{
				//询问是否用新账户登录
				Console.WriteLine("是否使用账户立即登录（Y/N）");
				switch (Console.ReadLine())
				{
					case "Y":
						name = userName;
						return true;

					case "N":
						//登录
						Console.WriteLine("请进行登录");
						return Log(out name);

					default:
						Console.WriteLine("输入错误，请重新输入！");
						break;
				}
			}
		}
		//登录
		private static bool Log(out string name)
		{
			Console.Write("用户名：");
			string userName = Console.ReadLine();
			Console.Write("密码：");
			string password = EnterPasswd();
			Console.WriteLine();

			while (true)
			{
				string securityCode = GetSecurityCode();
				Console.WriteLine("验证码：" + securityCode);
				Console.Write("请输入验证码：");
				if (Console.ReadLine().Equals(securityCode))
				{
					break;
				}
				else
				{
					Console.WriteLine("验证码错误，请重新输入！");
				}
			}
			name = userName;
			return UserInfoController.GetInstance().MatchUser(userName, password);
		}
		//随机生成验证码
		internal static string GetSecurityCode()
		{
			string securityCode = "";
			for (int i = 0; i < CODE_SIZE; ++i)
			{
				int time = (int)DateTime.Now.Ticks;
				int index = new Random(time).Next(0, SECURITY_CODE.Length);//随机生成一个下标

				securityCode += SECURITY_CODE[index];
			}
			return securityCode;
		}

		//加密密码的输入
		private static string EnterPasswd()
		{
			while (true)
			{
				string key = string.Empty;
				while (true)
				{
					ConsoleKeyInfo keyinfo = Console.ReadKey(true);
					if (keyinfo.Key == ConsoleKey.Enter) //按下回车，结束
						break;
					else if (keyinfo.Key == ConsoleKey.Backspace && key.Length > 0) //如果是退格键并且字符没有删光
					{
						Console.Write("\b \b"); //输出一个退格（此时光标向左走了一位），然后输出一个空格取代最后一个星号，然后再往前走一位，也就是说其实后面有一个空格但是你看不见= =
						key = key.Substring(0, key.Length - 1);
					}

					else if (!char.IsControl(keyinfo.KeyChar)) //过滤掉功能按键等
					{
						key += keyinfo.KeyChar.ToString();
						Console.Write("*");
					}
				}
				return key;
			}
		}		
	}
}
