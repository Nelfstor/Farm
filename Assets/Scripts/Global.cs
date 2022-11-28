using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Global
{
	public class Log 
	{
		static int normalSize = 14;
		public static void MyLog(string s)
		{
			Debug.LogWarning($" {System.DateTime.Now.ToString(" MM.dd HH:mm:ss ")} #MyLog  {s} ".Size(normalSize).Color("white"));
		}
		public static void MyLog(string s, int size)
		{
			Debug.LogWarning($" {System.DateTime.Now.ToString(" MM.dd HH:mm:ss ")}  #MyLog  {s} ".Size(size));
		}
		public static void MyLog(string s, Color color)
		{
			Debug.LogWarning($" {System.DateTime.Now.ToString(" MM.dd HH:mm:ss ")}  #MyLog  {s} ".Color(color.ToString()));
		}
		public static void MyBoldLog(string s)
		{
			Debug.LogWarning($" {System.DateTime.Now.ToString(" MM.dd HH:mm:ss ")}  #MyLog  {s} ".Bold());
		}

		public static void MyGreenLog(string s)
		{
			Debug.LogWarning($" {System.DateTime.Now.ToString(" MM.dd HH:mm:ss ")}  #MyLog  {s} ".Color("lime").Size(normalSize));
		}
		public static void MyMagentaLog(string s)
		{
			Debug.Log($" {System.DateTime.Now.ToString(" MM.dd HH:mm:ss ")}  #MyLog  {s} ".Color("magenta").Size(normalSize));
		}

		public static void MyBigLog(string s)
		{
			Debug.LogWarning($" {System.DateTime.Now.ToString(" MM.dd HH:mm:ss ")}  #MyLog  {s} ".Size(16).Color("white"));
		}
		public static void MyYellowLog(string s)
		{
			Debug.LogWarning($" {System.DateTime.Now.ToString(" MM.dd HH:mm:ss ")}  #MyLog  {s} ".Color("yellow").Size(normalSize));
		}
		public static void MyFuchsiaLog(string s)
		{
			Debug.LogWarning($" {System.DateTime.Now.ToString(" MM.dd HH:mm:ss ")}  #MyLog  {s} ".Color("Fuchsia").Size(normalSize));
		}
		public static void MyCyanLog(string s)
		{
			Debug.LogWarning($" {System.DateTime.Now.ToString(" MM.dd HH:mm:ss ")}  #MyLog  {s} ".Color("cyan").Size(normalSize));
		}
	}
	public static class StringExtension
	{
		public static string Bold(this string str) => "<b>" + str + "</b>";
		public static string Color(this string str, string clr) => string.Format("<color={0}>{1}</color>", clr, str);
		public static string Italic(this string str) => "<i>" + str + "</i>";
		public static string Size(this string str, int size) => string.Format("<size={0}>{1}</size>", size, str);

	}

	public static class GameObjectExtension
	{
		public static Transform FindDeepChild(this Transform aParent, string aName)
		{
			var result = aParent.Find(aName);
			if (result != null)
				return result;
			foreach (Transform child in aParent)
			{
				result = child.FindDeepChild(aName);
				if (result != null)
					return result;
			}
			return null;
		}
	}
	public class Sys
	{
		public enum OS { Android, WindowsStandAlone, WindowsUnityEditor, Linux, IOS }
		public static OS currentOS
		{
			get
			{
				OS result;
				switch (SystemInfo.operatingSystemFamily)
				{
					case OperatingSystemFamily.Linux:
						result = OS.Linux;
						Debug.LogWarning($" #MyLog LINUX DETECTED");
						break;
					case OperatingSystemFamily.Windows:
						{
#if UNITY_EDITOR
							result = OS.WindowsUnityEditor;
							Debug.LogWarning($" #MyLog UNITY EDITOR DETECTED");
#else
                    result = OS.WindowsStandAlone;
#endif
						}
						break;
					default:
						result = OS.Android;
						break;
				}
				// return OS.Android;   // Uncomment to immitate Android 
				return result;
			}
		}
	}


	public class Singleton<T> : MonoBehaviour
	where T : Component
	{
		private static T _instance;

		public static T Instance
		{
			get
			{
				if (_instance == null)
				{
					var objs = FindObjectsOfType(typeof(T)) as T[];
					if (objs.Length > 0)
					{
						_instance = objs[0];
					}
					if (objs.Length > 1)
					{
						Debug.LogError("There is more than one " + typeof(T).Name + " in the scene");
					}
					if (_instance == null)
					{
						GameObject obj = new GameObject();
						obj.hideFlags = HideFlags.HideAndDontSave;
						_instance = obj.AddComponent<T>();
					}
				}
				return _instance;
			}
		}
	}

	public class SingletonPersistent<T> : MonoBehaviour
		where T : Component
	{
		private static T instance;
		public static T Instance { get => instance; }

		public virtual void Awake()
		{
			if (instance == null)
			{
				instance = this as T;
				DontDestroyOnLoad(this);
			}
			else
			{
				Destroy(gameObject);
			}
		}
	}
}