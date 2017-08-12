using System;
using System.Collections.Generic;

namespace CloudNotes
{
	public class UserInfoController
	{
		private static readonly object locker = 0;
		private List<UserInfo> list;

		private UserInfoController()
		{
			list = AnalysisXml.GetUserInfoList();
		}
		private static UserInfoController Instance = null;
		public static UserInfoController GetInstance() {
			if (Instance == null) { 
				lock(locker) {
					if (Instance == null) {
						Instance = new UserInfoController();
					}
				}

			}
			return Instance;
		}

		public void AddUserInfo(UserInfo user) {
			list.Add(user);
			AnalysisXml.AddUserInfoToXml(user);
			Console.WriteLine("添加用户成功！");
		}
		public void RemoveByUserName(string name) {
			foreach (UserInfo item in list) {
				if (item.UserName == name) {
					list.Remove(item);
					Console.WriteLine("移除用户成功！");
					return;
				}
			}
			Console.WriteLine("未找到此用户！");
		}
		//打印所有用户
		public void Show() {
			int i = 0;
			foreach (UserInfo item in list) { 
				Console.WriteLine(++i + ". " + item.UserName);
			}
		}

	//通过userName和Password与list进行匹配检测
		public bool MatchUser(string userName, string password)
		{
			foreach (UserInfo user in list)
			{
				if (user.UserName == userName)
				{
					if (user.Password == password)
					{
						return true;
					}
					else
					{
						Console.WriteLine("密码错误！");
						return false;
					}
				}
			}
			Console.WriteLine("不存在此用户");
			return false;
		}
	}
}
