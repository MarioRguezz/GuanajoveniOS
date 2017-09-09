using System;
using System.Collections.Generic;
using System.Linq;
using Plugin.Connectivity;
using Xamarin.Forms;

namespace Guanajoven
{
	public partial class AdvertisingPage : ContentPage
	{
		public AdvertisingPage()
		{
			InitializeComponent();
			NavigationPage.SetHasNavigationBar(this, false);


				

			var publicidad = App.CurrentApp.RealmInstance.All<Publicidad>().ToList();
			if (publicidad.Count > 0)
			{
				Random rnd = new Random();
				int image = rnd.Next(0, publicidad.Count);
				var x = publicidad[image].ruta_imagen;
				_advertisingImage.Source = x;

			}
		}

		async void CloseClicked(object sender, EventArgs args)
		{
			App.CurrentApp.publicidad = true;
			await Navigation.PopAsync();
			//RootPage.IsPresented = true;
		}

		#region  Connection
		async void CheckConnection()
		{
			try
			{
				//var res = await CrossConnectivity.Current.IsReachable("http://www.google.com") ? "Reachable" : "Not reachable";
				var res = await CrossConnectivity.Current.IsReachable("http://www.google.com") ? true : false;

				if (res)
				{
					System.Diagnostics.Debug.WriteLine("conexion");
				}
				else
				{
					//await DisplayAlert("Error", "Verifique su conexión a internet", "Aceptar");
					return;
				}
			}
			catch (Exception ex)
			{
			}
		}
		#endregion

	}


}
