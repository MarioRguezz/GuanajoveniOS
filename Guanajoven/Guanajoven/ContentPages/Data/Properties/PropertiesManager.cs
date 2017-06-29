using System;
using Newtonsoft.Json;
using Xamarin.Forms;

namespace Guanajoven
{
	public class PropertiesManager
	{

		static string USER_INFO_KEY = "USER_INFO";
		static string FIRST_TIME_KEY = "FIRST_TIME";



		public static bool IsFirstTime()
		{
			if (!Application.Current.Properties.ContainsKey(FIRST_TIME_KEY))
			{
				return true;
			}
			else
			{
				return false;
			}
		}

		public static async void SaveFirstArrive()
		{
			Application.Current.Properties[FIRST_TIME_KEY] = "1";
			await Application.Current.SavePropertiesAsync();
		}

		public static bool IsLogedIn()
		{
			if (Application.Current.Properties.ContainsKey(USER_INFO_KEY))
			{
				return true;
			}
			else
			{
				return false;
			}
		}

		public static async void SaveUserInfo(ResponseUsuario user)
		{
			Application.Current.Properties[USER_INFO_KEY] = JsonConvert.SerializeObject(user);
			await Application.Current.SavePropertiesAsync();
		}

		public static ResponseUsuario GetUserInfo()
		{
			if (IsLogedIn())
			{
				var userJson = Application.Current.Properties[USER_INFO_KEY].ToString();
				return JsonConvert.DeserializeObject<ResponseUsuario>(userJson);
			}

			return null;
		}

		public static async void LogOut()
		{
			Application.Current.Properties.Remove(USER_INFO_KEY);
			await Application.Current.SavePropertiesAsync();
		}
	}
}