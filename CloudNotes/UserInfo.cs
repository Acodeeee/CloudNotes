using System;
namespace CloudNotes
{
	public class UserInfo
	{
		public string UserName { get; set; }
		public string Password { get; set; }

		public UserInfo() { }
		public UserInfo(string userName, string passWord)
		{
			UserName = userName;
			Password = passWord;
		}
	}
}
