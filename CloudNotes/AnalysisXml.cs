using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;
using System.IO;

namespace CloudNotes
{
	public class AnalysisXml
	{
		private const string XML_PATH = "XML/UserInfo.xml";

		public static List<UserInfo> GetUserInfoList() {
			List<UserInfo> list = new List<UserInfo>();

			XmlDocument xmlDoc = new XmlDocument();

			//判断xml文件是否存在，存在则读取，不存在则创建
			if (File.Exists(XML_PATH))
			{
				xmlDoc.Load(XML_PATH);  //加载文件
				XmlElement root = xmlDoc.DocumentElement;   //获取根节点
				XmlNodeList nodeList = root.ChildNodes;		//获取根节点的字节点
				foreach (XmlNode node in nodeList) {
					//添加文件的list
					list.Add(new UserInfo(node.SelectSingleNode("UserName").InnerText, node.SelectSingleNode("Password").InnerText));
				}
			}
			else {
				xmlDoc.AppendChild(xmlDoc.CreateXmlDeclaration("1.0", "UTF-8", null));//创建声明信息，并且添加到xml文件中

				//创建Root节点并且添加到xml文件
				XmlElement xRoot = xmlDoc.CreateElement("Root");
				xmlDoc.AppendChild(xRoot);

				//创建UserInfo节点并且添加到xml文件
				XmlElement xUserInfo = xmlDoc.CreateElement("UserInfo");
				xRoot.AppendChild(xUserInfo);

				//创建UserName节点并且添加到xml文件
				XmlElement xUserName = xmlDoc.CreateElement("UserName");
				xUserName.InnerText = "admin";
				xUserInfo.AppendChild(xUserName);

				//创建Password节点并且添加到xml文件
				XmlElement xPassword = xmlDoc.CreateElement("Password");
				xPassword.InnerText = "123456";
				xUserInfo.AppendChild(xPassword);

				xmlDoc.Save(XML_PATH);//保存文件

				list.Add(new UserInfo("admin","123456"));

			}
			return list;
		}

		//往Xml文件追加用户信息
		public static void AddUserInfoToXml(UserInfo user) {
			//XmlSerializer writer = new XmlSerializer(typeof(UserInfo));
			//FileStream file = File.Open(XML_PATH, FileMode.Append);
			//writer.Serialize(file, user);
			//file.Close();

			//加载xml文件
			XmlDocument xmlDoc = new XmlDocument();
			xmlDoc.Load(XML_PATH);

			XmlElement root = xmlDoc.DocumentElement;//获取跟节点
			//新建UserInfo节点并且添加到root节点中
			XmlElement xUserInfo = xmlDoc.CreateElement("UserInfo");
			root.AppendChild(xUserInfo);

			//为UserInfo节点添加字节点
			XmlElement xUserName = xmlDoc.CreateElement("UserName");
			xUserName.InnerText = user.UserName;//	添加节点数据
			xUserInfo.AppendChild(xUserName);
			XmlElement xPassword = xmlDoc.CreateElement("Password");
			xPassword.InnerText = user.Password;
			xUserInfo.AppendChild(xPassword);

			xmlDoc.Save(XML_PATH);//保存文件
		}
	}
}
