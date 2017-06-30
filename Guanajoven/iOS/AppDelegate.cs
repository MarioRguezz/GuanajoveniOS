using System;
using System.Collections.Generic;
using System.Linq;
using Foundation;
//using KeyboardOverlap.Forms.Plugin.iOSUnified;
//using Realms;
using UIKit;

namespace Guanajoven.iOS
{
	[Register("AppDelegate")]
	public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
	{
		public override bool FinishedLaunching(UIApplication app, NSDictionary options)
		{

			#region NOTIFICATIONS
			InitNotifiactions(app, options);
			#endregion

			global::Xamarin.Forms.Forms.Init();
			global::Xamarin.Auth.Presenters.XamarinIOS.AuthenticationConfiguration.Init();

			//Xamarin.FormsMaps.Init();

			FFImageLoading.Forms.Touch.CachedImageRenderer.Init();
			var dummy = new FFImageLoading.Forms.Touch.CachedImageRenderer();
			var ignore = new FFImageLoading.Transformations.CircleTransformation();

			//	KeyboardOverlapRenderer.Init();
			LoadApplication(new App());

			return base.FinishedLaunching(app, options);
		}

		#region Notifications

		void InitNotifiactions(UIApplication app, NSDictionary options)
		{
			if (options != null)
			{
				if (options.ContainsKey(UIApplication.LaunchOptionsRemoteNotificationKey))
				{
					var notificationData = options[UIApplication.LaunchOptionsRemoteNotificationKey] as NSDictionary;
					App.CurrentApp.FromRemoteNotification = true;
				}
			}
		}

		public override void RegisteredForRemoteNotifications(UIApplication application, NSData deviceToken)
		{
			var newDeviceToken = deviceToken.ToString().Replace("<", "").Replace(">", "").Replace(" ", "");
			Console.WriteLine("Device Token: " + newDeviceToken);
			App.CurrentApp.SaveToken(newDeviceToken, 2);
		}

		public override void DidRegisterUserNotificationSettings(UIApplication application, UIUserNotificationSettings notificationSettings)
		{
			UIApplication.SharedApplication.RegisterForRemoteNotifications();
		}

		public override void FailedToRegisterForRemoteNotifications(UIApplication application, NSError error)
		{
			Console.WriteLine("Failed to register for notifications");
		}

		public override void ReceivedRemoteNotification(UIApplication application, NSDictionary userInfo)
		{
			Console.WriteLine("Received Remote Notification!");
			processNotification(userInfo, false);
		}

		public void processNotification(NSDictionary options, bool fromFinishedLaunching)
		{
			if (options != null)
			{

				//if (App.CurrentApp.User == null)
				//{
				//	return;
				//}

				if (options.ContainsKey(new NSString("aps")) || options.ContainsKey(new NSString("UIApplicationLaunchOptionsRemoteNotificationKey")))
				{
					UIApplication.SharedApplication.CancelAllLocalNotifications();

					NSDictionary aps = null;

					if (options.ContainsKey(new NSString("aps")))
					{
						aps = options.ObjectForKey(new NSString("aps")) as NSDictionary;
					}
					else if (options.ContainsKey(new NSString("UIApplicationLaunchOptionsRemoteNotificationKey")))
					{
						NSDictionary temp = options.ObjectForKey(new NSString("UIApplicationLaunchOptionsRemoteNotificationKey")) as NSDictionary;
						aps = temp.ObjectForKey(new NSString("aps")) as NSDictionary;
					}

					string alert = string.Empty;
					string sound = string.Empty;
					int badge = -1;

					Dictionary<string, string> data = new Dictionary<string, string>();

					if (aps.ContainsKey(new NSString("alert")))
					{
						alert = aps[new NSString("alert")].ToString();
						data.Add("alert", alert);

					}

					if (aps.ContainsKey(new NSString("sound")))
					{
						sound = aps[new NSString("sound")].ToString();
						data.Add("sound", sound);

					}

					if (aps.ContainsKey(new NSString("badge")))
					{
						string badgeStr = aps[new NSString("badge")].ToString();
						int.TryParse(badgeStr, out badge);
						data.Add("badge", badge.ToString());
					}

					if (badge >= 0)
						UIApplication.SharedApplication.ApplicationIconBadgeNumber = badge;

					//if (!string.IsNullOrEmpty(sound))
					//{
					//	var soundObj = AudioToolbox.SystemSound.FromFile(sound);
					//	soundObj.PlaySystemSound();
					//	AudioToolbox.SystemSound.Vibrate.PlaySystemSound();
					//}

					NotifyService();
				}
			}
		}

		private void NotifyService()
		{
			//try
			//{
			//	// retrieve the current xamarin forms page instance
			//	var rootPage = (RootPage)Xamarin.Forms.Application.Current.MainPage;

			//	if (rootPage.Detail is NavigationPage)
			//	{
			//		var navigationPage = (NavigationPage)rootPage.Detail;
			//		var currentPage = (BaseContentPage)navigationPage.CurrentPage;
			//		if (currentPage.NotificationAction != null)
			//		{
			//			currentPage?.NotificationAction.Invoke();
			//		}
			//		else
			//		{
			//		}
			//	}
			//	else
			//	{
			//		var currentPage = (BaseContentPage)rootPage.Detail;
			//		if (currentPage?.NotificationAction != null)
			//		{
			//			currentPage?.NotificationAction.Invoke();
			//		}
			//		else
			//		{
			//		}
			//	}
			//}
			//catch (Exception ex)
			//{
			//	Console.WriteLine("NotifyService Error: " + ex.Message);
			//} 
		}

		#endregion
	}


}


