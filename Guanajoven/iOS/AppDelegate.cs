using System;
using System.Collections.Generic;
using System.Linq;
using Foundation;
using HockeyApp.iOS;
using Plugin.FirebasePushNotification;
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
			FirebasePushNotificationManager.Initialize(options, true);

			global::Xamarin.Forms.Forms.Init();
			KeyboardOverlap.Forms.Plugin.iOSUnified.KeyboardOverlapRenderer.Init();

			global::Xamarin.Auth.Presenters.XamarinIOS.AuthenticationConfiguration.Init();

			Xamarin.FormsMaps.Init();

			FFImageLoading.Forms.Touch.CachedImageRenderer.Init();
			var dummy = new FFImageLoading.Forms.Touch.CachedImageRenderer();
			var ignore = new FFImageLoading.Transformations.CircleTransformation();



			//	KeyboardOverlapRenderer.Init();
			LoadApplication(new App());



			var manager = BITHockeyManager.SharedHockeyManager;
			manager.Configure("440c8d3c2763449c86a17a06d73c26e6");
			manager.StartManager();

			InitNotifiactions(app, options);

			return base.FinishedLaunching(app, options);
		}

		void InitNotifiactions(UIApplication app, NSDictionary options)
		{
			if (options != null)
			{
				if (options.ContainsKey(UIApplication.LaunchOptionsRemoteNotificationKey))
				{
					var notificationData = options[UIApplication.LaunchOptionsRemoteNotificationKey] as NSDictionary;
					ProcessNotification(options, true);
					App.CurrentApp.FromRemoteNotification = true;

				}
			}
		}

public const string KEY_APS = "aps";
public const string KEY_APP_LAUNCH_RMOTE = "UIApplicationLaunchOptionsRemoteNotificationKey";

		public void ProcessNotification(NSDictionary options, bool fromFinishedLaunching)
		{
			if (options != null)
			{

				//if (App.CurrentApp.User == null)
				//{
				//	return;
				//}

				if (options.ContainsKey(new NSString(KEY_APS)) || options.ContainsKey(new NSString(KEY_APP_LAUNCH_RMOTE)))
				{
					UIApplication.SharedApplication.CancelAllLocalNotifications();

					NSDictionary aps = null;

					if (options.ContainsKey(new NSString(KEY_APS)))
					{
						aps = options.ObjectForKey(new NSString(KEY_APS)) as NSDictionary;
					}
					else if (options.ContainsKey(new NSString(KEY_APP_LAUNCH_RMOTE)))
					{
						NSDictionary temp = options.ObjectForKey(new NSString(KEY_APP_LAUNCH_RMOTE)) as NSDictionary;
						aps = temp.ObjectForKey(new NSString(KEY_APS)) as NSDictionary;
					}

					Dictionary<string, string> data = new Dictionary<string, string>();

					if (aps.ContainsKey(new NSString("alert")))
					{
						var alert = aps[new NSString("alert")] as NSDictionary;

						if (alert.ContainsKey(new NSString("body")))
						{
							var body = alert[new NSString("body")].ToString();
							data.Add("body", body);
						}

						if (alert.ContainsKey(new NSString("title")))
						{
							var title = alert[new NSString("title")].ToString();
							data.Add("title", title);
						}
					}




					string url = null;

					if (options.ContainsKey(new NSString("gcm.notification.link_url")))
					{
						var nsUSL = options.ObjectForKey(new NSString("gcm.notification.link_url")) as NSString;
						url = nsUSL.ToString();
					}
					else if (aps.ContainsKey(new NSString("gcm.notification.link_url")))
					{
						var nsUSL = aps.ObjectForKey(new NSString("gcm.notification.link_url")) as NSString;
						url = nsUSL.ToString();
					}





					if (url != null)
					{
						data.Add("url", url);
					}

					if (aps.Keys.Contains(new NSString("content-available")))
					{
						data.Add("content","true");
					}

					//var asd = string.Join("_", aps.Keys.Select(x => (x as NSString).ToString()));
					//data.Add("shit", asd);

					App.CurrentApp.NotificationData = data;

					//App.CurrentApp.TestVar = Newtonsoft.Json.JsonConvert.SerializeObject(notificationData.Keys.Select(x => x.ToString()+"--"));


					//if (!string.IsNullOrEmpty(sound))
					//{
					//	var soundObj = AudioToolbox.SystemSound.FromFile(sound);
					//	soundObj.PlaySystemSound();
					//	AudioToolbox.SystemSound.Vibrate.PlaySystemSound();
					//}

				}
			}
		}


		#region FIREBASE
		public override void RegisteredForRemoteNotifications(UIApplication application, NSData deviceToken)
		{
#if DEBUG
			//App.CurrentApp.SaveToken(deviceToken+"",2);
			FirebasePushNotificationManager.DidRegisterRemoteNotifications(deviceToken, FirebaseTokenType.Sandbox);
#endif
#if RELEASE
                    FirebasePushNotificationManager.DidRegisterRemoteNotifications(deviceToken,FirebaseTokenType.Production);
#endif
		}

		public override void FailedToRegisterForRemoteNotifications(UIApplication application, NSError error)
		{
			//base.FailedToRegisterForRemoteNotifications(application, error);
			FirebasePushNotificationManager.RemoteNotificationRegistrationFailed(error);

		}
		// To receive notifications in foregroung on iOS 9 and below.
		// To receive notifications in background in any iOS version
		public override void DidReceiveRemoteNotification(UIApplication application, NSDictionary userInfo, Action<UIBackgroundFetchResult> completionHandler)
		{
			// If you are receiving a notification message while your app is in the background,
			// this callback will not be fired 'till the user taps on the notification launching the application.

			// If you disable method swizzling, you'll need to call this method. 
			// This lets FCM track message delivery and analytics, which is performed
			// automatically with method swizzling enabled.
			FirebasePushNotificationManager.DidReceiveMessage(userInfo);
			// Do your magic to handle the notification data
			System.Console.WriteLine(userInfo);
			ProcessNotification(userInfo, false);
		}

		public override void ReceivedLocalNotification(UIApplication application, UILocalNotification notification)
		{
			base.ReceivedLocalNotification(application, notification);
		}

		public override void ReceivedRemoteNotification(UIApplication application, NSDictionary userInfo)
		{
			base.ReceivedRemoteNotification(application, userInfo);

			Console.WriteLine("Received Remote Notification!");
			ProcessNotification(userInfo, false);
		}

		public override void OnActivated(UIApplication uiApplication)
		{
			FirebasePushNotificationManager.Connect();
			base.OnActivated(uiApplication);

		}
		public override void DidEnterBackground(UIApplication application)
		{
			// Use this method to release shared resources, save user data, invalidate timers and store the application state.
			// If your application supports background exection this method is called instead of WillTerminate when the user quits.
			FirebasePushNotificationManager.Disconnect();
		}

		#endregion

	}


}