using System;
using Newtonsoft.Json;
using Xamarin.Forms;

namespace Guanajoven
{
	public class PropertiesManager
	{

		static string USER_INFO_KEY = "USER_INFO";
		static string FIRST_TIME_KEY = "FIRST_TIME";
		static string EVENT_DATE_KEY = "EVENT_DATE_KEY";
		static string CALL_DATE_KEY = "CALL_DATE_KEY";
		static string REGION_DATE_KEY = "REGION_DATE_KEY";
		static string PROMOTION_DATE_KEY = "PROMOTION_DATE_KEY";
		static string ADVERTISING_KEY = "ADVERTISING_KEY";




		public static bool IsDateAdvertisingTrue()
		{
			if (!Application.Current.Properties.ContainsKey(ADVERTISING_KEY))
			{
				return true;
			}
			else
			{
				return false;
			}
		}


		public static async void SaveAdvertisingDate(DateTime date)
		{
			Application.Current.Properties[ADVERTISING_KEY] = date;
			await Application.Current.SavePropertiesAsync();
		}


		public static DateTime GetDataAdvertising()
		{

			var date = Application.Current.Properties[ADVERTISING_KEY].ToString();
			DateTime time = DateTime.Parse(date);
			return time;
		}

		public static bool IsDatePromotionTrue()
		{
			if (!Application.Current.Properties.ContainsKey(PROMOTION_DATE_KEY))
			{
				return true;
			}
			else
			{
				return false;
			}
		}


		public static async void SavePromotionDate(DateTime date)
		{
			Application.Current.Properties[PROMOTION_DATE_KEY] = date;
			await Application.Current.SavePropertiesAsync();
		}


		public static DateTime GetDataPromotion()
		{

			var date = Application.Current.Properties[PROMOTION_DATE_KEY].ToString();
			DateTime time = DateTime.Parse(date);
			return time;
		}



		public static bool IsDateRegionTrue()
		{
			if (!Application.Current.Properties.ContainsKey(REGION_DATE_KEY))
			{
				return true;
			}
			else
			{
				return false;
			}
		}


		public static async void SaveRegionDate(DateTime date)
		{
			Application.Current.Properties[REGION_DATE_KEY] = date;
			await Application.Current.SavePropertiesAsync();
		}


		public static DateTime GetDataRegion()
		{

			var date = Application.Current.Properties[REGION_DATE_KEY].ToString();
			DateTime time = DateTime.Parse(date);
			return time;
		}


		public static bool IsDateCallTrue()
		{
			if (!Application.Current.Properties.ContainsKey(CALL_DATE_KEY))
			{
				return true;
			}
			else
			{
				return false;
			}
		}


		public static async void SaveCallDate(DateTime date)
		{
			Application.Current.Properties[CALL_DATE_KEY] = date;
			await Application.Current.SavePropertiesAsync();
		}


		public static DateTime GetDateCall()
		{

			var date = Application.Current.Properties[CALL_DATE_KEY].ToString();
			DateTime time = DateTime.Parse(date);
			return time;
		}




		public static bool IsDateEventTrue()
		{
			if (!Application.Current.Properties.ContainsKey(EVENT_DATE_KEY))
			{
				return true;
			}
			else
			{
				return false;
			}
		}

		public static async void SaveEventDate(DateTime date)
		{
			Application.Current.Properties[EVENT_DATE_KEY] = date;
			await Application.Current.SavePropertiesAsync();
		}


		public static DateTime GetDateEvent()
		{

			var date = Application.Current.Properties[EVENT_DATE_KEY].ToString();
			DateTime time = DateTime.Parse(date);
			return time;
		}




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