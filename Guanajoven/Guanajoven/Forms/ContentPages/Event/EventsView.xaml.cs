using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Plugin.Connectivity;
using Xamarin.Forms;

namespace Guanajoven
{
	public partial class EventsView : BasePage
	{
		HomeDrawerPage RootPage;
		public static bool Changed = false;
		public static List<Evento> _events = null;
		ObservableCollection<Evento> _itemsList = new ObservableCollection<Evento>();
		static string timeString = DateTime.Now.ToString("yyyy-MM-dd HH:MM:ss");
		static DateTime time = DateTime.Parse(timeString);
		static DateTime dateToSend = time;

		public EventsView(HomeDrawerPage _rootPage)
		{
			InitializeComponent();
			RootPage = _rootPage;
			NavigationPage.SetHasNavigationBar(this, false);
			ListView.ItemsSource = _itemsList;
		}

		protected override void OnAppearing()
		{
			base.OnAppearing();
			//test();
			getEvents();
			if (App.CurrentApp.publicidad == true)
			{
				RootPage.IsPresented = true;
			}
			App.CurrentApp.publicidad = false;
		}

		async void getEvents()
		{
			if (PropertiesManager.IsDateEventTrue())
			{
				PropertiesManager.SaveEventDate(dateToSend);
			}
			else
			{
				dateToSend = PropertiesManager.GetDateEvent();
			}
			if (PropertiesManager.IsFirstDateEventTrue())
			{
				PropertiesManager.SaveFirstEventDate(1);
			}
			else
			{
				PropertiesManager.SaveFirstEventDate(PropertiesManager.GetFirstDataEvent() + 1);
			}
			CheckConnection();
			ShowProgress("Validando");
			String times = "";
			if (PropertiesManager.GetFirstDataEvent() > 1)
			{
				times = dateToSend.ToString("yyyy-MM-dd HH:MM:ss");

			}
			else
			{
				times = "0000-00-00 00:00:00";
			}
			_events = await ClientGuanajoven.getEvents(times);
			var eventos = App.CurrentApp.RealmInstance.All<Evento>().ToList();
			if (_events != null && _events.Count > 0)
			{
				foreach (var item in _events)
				{
					var fechainicio = DateTime.Parse(item.fecha_inicio).ToString("dd/MM/yyyy");
					//var fechafin = DateTime.Parse(item.fecha_fin).ToString("dd/MM/yyyy");
					item.fecha = fechainicio; //+ " - " + fechafin;
					var fecha_fin = DateTime.Parse(item.fecha_fin);
					if (time <= fecha_fin)
					{
						item.evento_estado = "Evento Abierto";
						item.IsEventoActivo = false;
					}
					else
					{
						item.evento_estado = "Evento Cerrado";
						item.IsEventoActivo = true;
					}
				}
				MergeLists(_events);
			}

			eventos = App.CurrentApp.RealmInstance.All<Evento>().ToList();
			if (eventos != null)
			{

				dateToSend = time;
				PropertiesManager.SaveEventDate(dateToSend);
				eventos = eventos.OrderByDescending(x => x.fecha_inicio).ToList();
				_itemsList.Clear();
				//	eventos = App.CurrentApp.RealmInstance.All<Evento>().ToList();
				foreach (var item in eventos)
				{
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

		void MergeLists(List<Evento> eventos)
		{

			App.CurrentApp.RealmInstance.Write(() =>
				{
					foreach (var _evento in eventos)
					{
						App.CurrentApp.RealmInstance.RemoveRange(App.CurrentApp.RealmInstance.All<Evento>().Where(x => x.id_evento == _evento.id_evento));
					}

					foreach (var item in eventos)
					{
						if (item.deleted_at == null)
						{
							App.CurrentApp.RealmInstance.Add(item);
						}
					}
				});
		}




		async void TapItem(object sender, System.EventArgs e)
		{
			var stack = (StackLayout)sender;
			await stack.ScaleTo(0.9, 100, Easing.SinIn);
			await stack.ScaleTo(1, 100, Easing.SinIn);
			if (stack.BindingContext is Evento)
			{
				var evento = stack.BindingContext as Evento;

				await Navigation.PushAsync(new DetailEventPage(evento));
			}
		}

		async void Deleted(object sender, System.EventArgs e)
		{
			var answer = await DisplayAlert("Guanajoven", "¿Quieres borrar este evento?", "Sí", "No");
			if (answer)
			{
				var mi = (Image)sender;
				await mi.ScaleTo(0.9, 100, Easing.SinIn);
				await mi.ScaleTo(1, 100, Easing.SinIn);
				if (mi.BindingContext is Evento)
				{
					var evento = mi.BindingContext as Evento;
					if (_itemsList.Contains(evento))
					{
						//quitar de realm
						App.CurrentApp.RealmInstance.Write(() =>
						{
							App.CurrentApp.RealmInstance.Remove(evento);
						});

						_itemsList.Remove(evento);
					}
				}
			}
		}




		//Tiene el evento de delete por eso lo comenté porque se usa el botón para borrar
		public void OnDelete(object sender, System.EventArgs e)
		{
			/*var mi = ((MenuItem)sender);

			var evento = (Evento)mi.CommandParameter;
			if (evento != null && _itemsList != null)
			{
				if (_itemsList.Contains(evento))
				{
					//quitar de realm
					App.CurrentApp.RealmInstance.Write(() =>
					{
						App.CurrentApp.RealmInstance.Remove(evento);
					});

					_itemsList.Remove(evento);


				}
			}
			//DisplayAlert("Delete Context Action", mi.CommandParameter + " delete context action", "ok");*/
		}


		async void MenuClicked(object sender, System.EventArgs e)

		{
			var image = sender as Image;
			image.Opacity = 0.6;
			image.FadeTo(1);
			RootPage.IsPresented = true;
			//await Navigation.PushAsync(new AdvertisingPage());
			//await Navigation.PopAsync();
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
