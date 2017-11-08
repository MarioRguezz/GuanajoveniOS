using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Plugin.Connectivity;
using Xamarin.Forms;

namespace Guanajoven
{
	public partial class CallPage : BasePage
	{
		HomeDrawerPage RootPage;
		public static bool Changed = false;
		public static List<Convocatoria> _calls = null;
		ObservableCollection<Convocatoria> _itemsList = new ObservableCollection<Convocatoria>();
		static string timeString = DateTime.Now.ToString("yyyy-MM-dd HH:MM:ss");
		static DateTime time = DateTime.Parse(timeString);
		static DateTime dateToSend = time;

		public CallPage(HomeDrawerPage _rootPage)
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
			getCalls();

			if (App.CurrentApp.publicidad == true)
			{
				RootPage.IsPresented = true;
			}
			App.CurrentApp.publicidad = false;

		}

		async void getCalls()
		{
			if (PropertiesManager.IsDateCallTrue())
			{
				PropertiesManager.SaveCallDate(dateToSend);
			}
			else
			{
				dateToSend = PropertiesManager.GetDateCall();
			}
			if (PropertiesManager.IsFirstDateCallTrue())
			{
				PropertiesManager.SaveFirstCallDate(1);
			}
			else
			{
				PropertiesManager.SaveFirstCallDate(PropertiesManager.GetFirstDataCall() + 1);
			}

			CheckConnection();
			ShowProgress("Validando");
			String times = "";
			if (PropertiesManager.GetFirstDataCall() > 1)
			{
				times = dateToSend.ToString("yyyy-MM-dd HH:MM:ss");

			}
			else
			{
				times = "0000-00-00 00:00:00";
			}
			//var times = dateToSend.ToString("yyyy-MM-dd HH:MM:ss");
			_calls = await ClientGuanajoven.getCalls(times);
			var convocatorias = App.CurrentApp.RealmInstance.All<Convocatoria>().ToList();
			if (_calls != null && _calls.Count > 0)
			{
				foreach (var item in _calls)
				{
					var fecha_cierre = DateTime.Parse(item.fecha_cierre);
					if (time <= fecha_cierre)
					{
						item.estatusText = "Activa";
					}
					else
					{
						item.estatusText = "Inactiva";
					}

					foreach (var document in item.documentos)
					{
						if (document.formato.nombre == "docx")
						{
							document.Icono = "ic_doc-web.png";
						}
						else if (document.formato.nombre == "xlsx")
						{
							document.Icono = "ic_xls-web.png";
						}
						else if (document.formato.nombre == "pdf")
						{
							document.Icono = "ic_pdf-web.png";
						}
						else
						{
							document.Icono = "ic_unknow-web.png";
						}
					}
				}

				App.CurrentApp.RealmInstance.Write(() =>
						{


							if (convocatorias.Count > 0)
							{
								foreach (var convocatoria in convocatorias)
								{
									foreach (var _call in _calls)
									{
										if (convocatoria.id_convocatoria == _call.id_convocatoria)
										{
											App.CurrentApp.RealmInstance.Remove(convocatoria);
										}

									}
								}
							}

							foreach (var item in _calls)
							{
								if (item.deleted_at != null)
								{
								}
								else
								{

									App.CurrentApp.RealmInstance.Add(item);
								}
							}
						});
			}

			convocatorias = App.CurrentApp.RealmInstance.All<Convocatoria>().ToList();
			if (convocatorias != null)
			{

				dateToSend = time;
				PropertiesManager.SaveCallDate(dateToSend);
				convocatorias = convocatorias.OrderByDescending(x => x.fecha_inicio).ToList();
				_itemsList.Clear();
				convocatorias = App.CurrentApp.RealmInstance.All<Convocatoria>().ToList();
				foreach (var item in convocatorias)
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





		async void TapItem(object sender, System.EventArgs e)
		{
			var stack = (StackLayout)sender;
			await stack.ScaleTo(0.9, 100, Easing.SinIn);
			await stack.ScaleTo(1, 100, Easing.SinIn);
			if (stack.BindingContext is Convocatoria)
			{
				var call = stack.BindingContext as Convocatoria;

				await Navigation.PushAsync(new DetailCallPage(call));
			}
		}

		async void CloseClicked(object sender, System.EventArgs e)
		{
			var image = sender as Image;
			image.Opacity = 0.6;
			image.FadeTo(1);
			//await Navigation.PushAsync(new AdvertisingPage());
			RootPage.IsPresented = true;
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
