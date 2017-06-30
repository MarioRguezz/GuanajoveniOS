using System;
using Xamarin.Forms;
using UIKit;
using Guanajoven.iOS;
using Foundation;

[assembly: Dependency(typeof(PushNotifications))]
namespace Guanajoven.iOS
{
	public class PushNotifications : IPushNotifications
	{
		public void Register()
		{
			//if (UIDevice.CurrentDevice.SystemVersion[0] >= '8')
			//{
			//	UIUserNotificationType types = UIUserNotificationType.Alert | UIUserNotificationType.Badge | UIUserNotificationType.Sound;
			//	UIUserNotificationSettings settings = UIUserNotificationSettings.GetSettingsForTypes(types, null);
			//	UIApplication.SharedApplication.RegisterUserNotificationSettings(settings);
			//}
			//else {
			//	UIRemoteNotificationType notificationTypes = UIRemoteNotificationType.Alert | UIRemoteNotificationType.Badge | UIRemoteNotificationType.Sound;
			//	UIApplication.SharedApplication.RegisterForRemoteNotificationTypes(notificationTypes);
			//}
			var settings = UIUserNotificationSettings.GetSettingsForTypes(
				UIUserNotificationType.Alert
				| UIUserNotificationType.Badge
				| UIUserNotificationType.Sound,
				new NSSet());

			UIApplication.SharedApplication.RegisterUserNotificationSettings(settings);
			UIApplication.SharedApplication.RegisterForRemoteNotifications();
			 
		}

		public void Unregister()
		{
			UIApplication.SharedApplication.UnregisterForRemoteNotifications();
		}

		public string IdUnique()
		{
			return UIKit.UIDevice.CurrentDevice.IdentifierForVendor.AsString();
		}
	}
}