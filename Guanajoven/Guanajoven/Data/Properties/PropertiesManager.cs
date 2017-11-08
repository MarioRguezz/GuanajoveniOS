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
		static string EVENT_FIRST_DATE_KEY = "EVENT_FIRST_DATE_KEY";


		static string CALL_DATE_KEY = "CALL_DATE_KEY";
		static string CALL_FIRST_DATE_KEY = "CALL_FIRST_DATE_KEY";


		static string REGION_DATE_KEY = "REGION_DATE_KEY";
		static string REGION_FIRST_DATE_KEY = "REGION_FIRST_DATE_KEY";

		static string PROMOTION_DATE_KEY = "PROMOTION_DATE_KEY";
		static string PROMOTION_FIRST_DATE_KEY = "PROMOTION_FIRST_DATE_KEY";

		static string ADVERTISING_KEY = "ADVERTISING_KEY";
		static string ADVERTISING_FIRST_KEY = "ADVERTISING_FIRST_KEY";




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


		public static bool IsDateFirstAdvertisingTrue()
		{
			if (!Application.Current.Properties.ContainsKey(ADVERTISING_FIRST_KEY))
			{
				return true;
			}
			else
			{
				return false;
			}
		}

		public static async void SaveFirstAdvertisingDate(int firsttime)
		{
			Application.Current.Properties[ADVERTISING_FIRST_KEY] = firsttime;
			await Application.Current.SavePropertiesAsync();
		}


		public static int GetFirstDataAdvertising()
		{

			var times = Int32.Parse(Application.Current.Properties[ADVERTISING_FIRST_KEY].ToString());
			return times;
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



		public static bool IsFirstDatePromotionTrue()
		{
			if (!Application.Current.Properties.ContainsKey(PROMOTION_FIRST_DATE_KEY))
			{
				return true;
			}
			else
			{
				return false;
			}
		}

		public static async void SaveFirstPromotionDate(int firsttime)
		{
			Application.Current.Properties[PROMOTION_FIRST_DATE_KEY] = firsttime;
			await Application.Current.SavePropertiesAsync();
		}


		public static int GetFirstDataPromotion()
		{

			var times = Int32.Parse(Application.Current.Properties[PROMOTION_FIRST_DATE_KEY].ToString());
			return times;
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

		public static bool IsDateFirstRegionTrue()
		{
			if (!Application.Current.Properties.ContainsKey(REGION_FIRST_DATE_KEY))
			{
				return true;
			}
			else
			{
				return false;
			}
		}

		public static async void SaveFirstRegionDate(int firsttime)
		{
			Application.Current.Properties[REGION_FIRST_DATE_KEY] = firsttime;
			await Application.Current.SavePropertiesAsync();
		}


		public static int GetFirstDataRegion()
		{

			var times = Int32.Parse(Application.Current.Properties[REGION_FIRST_DATE_KEY].ToString());
			return times;
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


		public static bool IsFirstDateCallTrue()
		{
			if (!Application.Current.Properties.ContainsKey(CALL_FIRST_DATE_KEY))
			{
				return true;
			}
			else
			{
				return false;
			}
		}

		public static async void SaveFirstCallDate(int firsttime)
		{
			Application.Current.Properties[CALL_FIRST_DATE_KEY] = firsttime;
			await Application.Current.SavePropertiesAsync();
		}


		public static int GetFirstDataCall()
		{

			var times = Int32.Parse(Application.Current.Properties[CALL_FIRST_DATE_KEY].ToString());
			return times;
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



		public static bool IsFirstDateEventTrue()
		{
			if (!Application.Current.Properties.ContainsKey(EVENT_FIRST_DATE_KEY))
			{
				return true;
			}
			else
			{
				return false;
			}
		}

		public static async void SaveFirstEventDate(int firsttime)
		{
			Application.Current.Properties[EVENT_FIRST_DATE_KEY] = firsttime;
			await Application.Current.SavePropertiesAsync();
		}


		public static int GetFirstDataEvent()
		{

			var times = Int32.Parse(Application.Current.Properties[EVENT_FIRST_DATE_KEY].ToString());
			return times;
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