using System;

namespace CloudNotes
{
	class MainClass
	{
		public static void Main(string[] args)
		{
			string name;
			//登录 注册 
			while (!LogOrRegistLogic.LogOrRegist(out name)) { }

			ToDoListLogic.ManageNotes(name);
		}


	}
}
