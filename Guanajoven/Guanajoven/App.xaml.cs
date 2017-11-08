using System;
using System.Collections.Generic;
using System.Linq;
using Plugin.FirebasePushNotification;
using Plugin.FirebasePushNotification.Abstractions;
using Realms;
using Realms.Exceptions;
using Softweb.Controls;
//using Realms;
using Xamarin.Forms;

namespace Guanajoven
{
	public partial class App : Application
	{

		public static App CurrentApp { get; set; }

		public string DeviceToken { get; set; }
		public bool publicidad { get; set; }
		public Realm RealmInstance;
		public bool FromRemoteNotification { get; set; }
		public Dictionary<string, string> NotificationData { get; set; }
		public bool IsInChat { get; set; }

		public event EventHandler<ChatModel> MensajeRecibido;

		public App()
		{
			CurrentApp = this;
			InitializeComponent();

			//MainPage = new ChatPage(null);
			//return;

			RealmConfiguration realmConfiguration = RealmConfiguration.DefaultConfiguration;

			try
			{
				RealmInstance = Realm.GetInstance();
			}
			catch (RealmMigrationNeededException e)
			{
				try
				{
					Realm.DeleteRealm(realmConfiguration);
					//Realm file has been deleted.
					RealmInstance = Realm.GetInstance(realmConfiguration);
				}
				catch (Exception ex)
				{
					throw ex;
				}
			}


			var eventos = App.CurrentApp.RealmInstance.All<Notificacion>().ToList();

			//MainPage = new NavigationPage(new GuanajovenCodePage());
			// return;

			if (PropertiesManager.IsFirstTime())
			{
				MainPage = new WelcomePage();
			}
			else
			{
				if (PropertiesManager.IsLogedIn())
				{
					//	MainPage = new HomePage();
					MainPage = new NavigationPage(new HomeDrawerPage());
				}
				else
				{

					MainPage = new NavigationPage(new RootPage());
					//MainPage = new NavigationPage(new HomeDrawerPage());
					//	MainPage = new NavigationPage(new PickIdiomas());
				}
			}


			InitPushNotifications();
		}

		public void InitPushNotifications()
		{
			//DependencyService.Get<IPushNotifications>().Register();



			CrossFirebasePushNotification.Current.OnTokenRefresh += (s, p) =>
			 {
				 System.Diagnostics.Debug.WriteLine($"TOKEN : {p.Token}");

				SaveToken(p.Token, 2);
			 };

			CrossFirebasePushNotification.Current.OnNotificationReceived += (s, p) =>
			{
				ProcesarMensaje(p);

			};


			CrossFirebasePushNotification.Current.OnNotificationOpened += (s, p) =>
			 {
				 System.Diagnostics.Debug.WriteLine("Opened");
				 foreach (var data in p.Data)
				 {
					 System.Diagnostics.Debug.WriteLine($"{data.Key} : {data.Value}");
				 }

				 if (!string.IsNullOrEmpty(p.Identifier))
				 {
					 System.Diagnostics.Debug.WriteLine($"ActionId: {p.Identifier}");
				 }

			 };

			var x = CrossFirebasePushNotification.Current.Token;
			SaveToken(x, 2);
		}

		async void ProcesarMensaje(FirebasePushNotificationDataEventArgs p)
		{
			var r = p.Data.Keys;
			if (p.Data.ContainsKey("gcm.notification.link_url"))
			{
				System.Diagnostics.Debug.WriteLine("URL:" + p.Data["gcm.notification.link_url"]);
			}
			else
			{
				System.Diagnostics.Debug.WriteLine("MESSAGE");
			}
			System.Diagnostics.Debug.WriteLine("Received");

			DateTime time = DateTime.Now;
			if (p.Data.ContainsKey("gcm.notification.link_url"))
			{
				if (p.Data["gcm.notification.link_url"] == "chat")
				{
					//CHAT
					if (!IsInChat)
					{
						UIMessage.ShowToast("¡Te enviaron un nuevo mensaje!", ToastMessage.ToastPosition.TOP, ToastMessage.Duration.Long
											 , null, Color.FromHex("#003464"));
					}
					else
					{
						if (MensajeRecibido != null)
						{
							MensajeRecibido(this, new ChatModel()
							{
								mensaje = p.Data["aps.alert.body"],
								created_at = time.ToString("u").Substring(0, time.ToString("u").Length - 1),
								envia_usuario = 0,

							});
						}
					}

				}
				else
				{

					try
					{
						await MainPage.DisplayAlert(p.Data["aps.alert.title"], p.Data["aps.alert.body"], "Aceptar");
					}
					catch { }


					App.CurrentApp.RealmInstance.Write(() =>
					{
						var notifi = new Notificacion();
						notifi.url = p.Data.ContainsKey("gcm.notification.link_url") ? p.Data["gcm.notification.link_url"] : null;
						notifi.titulo = p.Data["aps.alert.title"];
						notifi.mensaje = p.Data["aps.alert.body"];
						notifi.fecha_emision = time.ToString("u");

						App.CurrentApp.RealmInstance.Add(notifi);
					});

					Device.OpenUri(new Uri(p.Data["gcm.notification.link_url"]));
				}
			}
			else
			{
				App.CurrentApp.RealmInstance.Write(() =>
				{
					var notifi = new Notificacion();
					notifi.url = p.Data.ContainsKey("gcm.notification.link_url") ? p.Data["gcm.notification.link_url"] : null;
					notifi.titulo = p.Data["aps.alert.title"];
					notifi.mensaje = p.Data["aps.alert.body"];
					notifi.fecha_emision = time.ToString("u");

					App.CurrentApp.RealmInstance.Add(notifi);
				});
			}


		}

		protected override async void OnStart()
		{
			// Handle when your app starts
			//UpdateUserStatus();
			//TestRealm();


			//var url = await ClientGuanajoven.GetURL("todos", "se salieron");
		}

		/*void TestRealm()
		{
			var eventos = RealmInstance.All<Evento>().ToList();
			RealmInstance.Write(() =>
							{
								foreach (var item in eventos)
								{
									RealmInstance.Remove(item);
								}
								
							});
			eventos = RealmInstance.All<Evento>().ToList();

		}*/

		public void SaveToken(string deviceToken, int OS)
		{

			DeviceToken = deviceToken;

			if (OS == 2)
			{
				//IOS
			}
		}

		/*public async void UpdateUserStatus()
		{
			var user = PropertiesManager.GetUserInfo();
			if (user != null)
			{
				if (user.tipo_usuario_id == 2)
				{
					try
					{
						user.status = 1; //activo
						var res = await ClientLaPurisima.PostObject<User>(user, WEB_METHODS.SetStatusRepartidor);
					}
					catch (Exception ex)
					{
						System.Diagnostics.Debug.WriteLine("error updating status. " + ex.Message);
					}

					LocationHelper.Instance.Geolocator.PositionChanged += async (sender, e) =>
					{
						try
						{
							user.status = 1; //activo
							user.latitud = e.Position.Latitude;
							user.longitud = e.Position.Longitude;
							var res = await ClientLaPurisima.PostObject<User>(user, WEB_METHODS.SetStatusRepartidor);
							System.Diagnostics.Debug.WriteLine("RES UPDATE STATUS REPARIDOR: " + res);
						}
						catch (Exception ex)
						{
							System.Diagnostics.Debug.WriteLine("error updating status. " + ex.Message);
						}
					};
				}
				else
				{
					var x = LocationHelper.Instance.Geolocator;
				}
			}
		}*/

		public void test()
		{

		}


		protected override void OnSleep()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume()
		{
			// Handle when your app resumes
		}
	}
}

