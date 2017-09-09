using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Xamarin.Forms;

namespace Guanajoven
{
	public partial class NotificationPage : BasePage
	{
		HomeDrawerPage RootPage;
		public static List<Notificacion> _notification = null;
		ObservableCollection<Notificacion> _itemsList = new ObservableCollection<Notificacion>();
		public NotificationPage(HomeDrawerPage _rootPage)
		{
			InitializeComponent();
			RootPage = _rootPage;
			NavigationPage.SetHasNavigationBar(this, false);

		}

		protected override void OnAppearing()
		{
			base.OnAppearing();
			if (App.CurrentApp.publicidad == true)
			{
				RootPage.IsPresented = true;
			}
			App.CurrentApp.publicidad = false;
			GetNotification();
		}



		async void GetNotification()
		{

			var notificaciones = App.CurrentApp.RealmInstance.All<Notificacion>().ToList();
			//Notificacion not = new Notificacion();
			//not.titulo = "nuevo titulo";
			//not.mensaje = "nuevo mensaje que decide que decide que decid";
			//not.fecha_emision = "25/08/17";
			//Notificacion not2 = new Notificacion();
			//not2.titulo = "nuevo titulo 2";
			//not2.mensaje = "nuevo mensaje 2";
			//not2.url = null;
			//not2.fecha_emision = "22/04/17";
			//_notification = new List<Notificacion>();
			//_notification.Add(not);
			//_notification.Add(not2);
			//var notificaciones = _notification;
			if (notificaciones != null && notificaciones.Count() > 0)
			{

				notificaciones = notificaciones.OrderByDescending(x => x.fecha_emision).ToList();
				_itemsList.Clear();
				foreach (var item in notificaciones)
				{
					if (item.url == null)
					{
						item.url = "";
					}
					_itemsList.Add(item);
				}


				Device.BeginInvokeOnMainThread(() =>
				{
					if (ListView.ItemsSource != _itemsList)
						ListView.ItemsSource = _itemsList;
				});
			}
			HideProgress();
		}


		async void Deleted(object sender, System.EventArgs e)
		{
			var answer = await DisplayAlert("Guanajoven", "¿Quieres borrar esta notificación?", "Sí", "No");
			if (answer)
			{
				var mi = (Image)sender;
				await mi.ScaleTo(0.9, 100, Easing.SinIn);
				await mi.ScaleTo(1, 100, Easing.SinIn);
				if (mi.BindingContext is Notificacion)
				{
					var notificacion = mi.BindingContext as Notificacion;
					if (_itemsList.Contains(notificacion))
					{
						//quitar de realm
						App.CurrentApp.RealmInstance.Write(() =>
						{
							App.CurrentApp.RealmInstance.Remove(notificacion);
						});

						_itemsList.Remove(notificacion);
					}
				}
			}
		}



		async void TapItem(object sender, System.EventArgs e)
		{
			var stack = (StackLayout)sender;
			await stack.ScaleTo(0.9, 100, Easing.SinIn);
			await stack.ScaleTo(1, 100, Easing.SinIn);
			if (stack.BindingContext is Notificacion)
			{
				
				var notificacion = stack.BindingContext as Notificacion;
				string stringUrl = (string)notificacion.url;
				if (stringUrl == "" || stringUrl == null)
				{

				}
				else
				{
					Device.OpenUri(new Uri(notificacion.url));
				}
			}
		}



		public void OnDelete(object sender, System.EventArgs e)
		{
			/*var mi = ((MenuItem)sender);

			var notificacion = (Notificacion)mi.CommandParameter;
			if (notificacion != null && _itemsList != null)
			{
				if (_itemsList.Contains(notificacion))
				{
					//quitar de realm
					///	App.CurrentApp.RealmInstance.Write(() =>
					//	{
					//		App.CurrentApp.RealmInstance.Remove(evento);
					//	});

					_itemsList.Remove(notificacion);


				}
			}*/

			//DisplayAlert("Delete Context Action", mi.CommandParameter + " delete context action", "ok"
		}

		async void CloseClicked(object sender, System.EventArgs e)
		{
			/* return to before page
			 * var image = sender as Image;
			image.Opacity = 0.6;
			image.FadeTo(1);
			await Navigation.PopAsync();*/
			var image = sender as Image;
			image.Opacity = 0.6;
			image.FadeTo(1);
			//RootPage.IsPresented = true;
			await Navigation.PushAsync(new AdvertisingPage());
		}
	}
}
