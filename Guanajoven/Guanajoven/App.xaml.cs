using System;
using System.Collections.Generic;
//using Realms;
using Xamarin.Forms;

namespace Guanajoven
{
	public partial class App : Application
	{

		public static App CurrentApp { get; set; }

		public App()
		{
			CurrentApp = this;
			InitializeComponent();

			/*	RealmConfiguration realmConfiguration = RealmConfiguration.DefaultConfiguration;

				try
				{
					var x = Realm.GetInstance();
				}
				catch (RealmMigrationNeededException e)
				{
					try
					{
						Realm.DeleteRealm(realmConfiguration);
						//Realm file has been deleted.
						var y = Realm.GetInstance(realmConfiguration);
					}
					catch (Exception ex)
					{
						throw ex;
					}
				}*/


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

					//MainPage = new NavigationPage(new RootPage());
					MainPage = new NavigationPage(new RootPage());
				//	MainPage = new NavigationPage(new PickIdiomas());
				}
			}





		}

		protected override void OnStart()
		{
			// Handle when your app starts
			//UpdateUserStatus();
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

