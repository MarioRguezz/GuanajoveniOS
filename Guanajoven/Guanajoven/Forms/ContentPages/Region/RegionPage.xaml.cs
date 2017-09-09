using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Plugin.Connectivity;
using Xamarin.Forms;

namespace Guanajoven
{
	public partial class RegionPage : BasePage
	{
		HomeDrawerPage RootPage;
		public static bool Changed = false;
		public static List<Region> _regionSer = null;
		ObservableCollection<Region> _itemsList = new ObservableCollection<Region>();
		static string timeString = DateTime.Now.ToString("yyyy-MM-dd HH:MM:ss");
		static DateTime time = DateTime.Parse(timeString);
		static DateTime dateToSend = time;

		public RegionPage(HomeDrawerPage _rootPage)
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
			//getEvents();
			getRegion();
		}

		/*public void test()
		{
			_events = new List<UsuarioPOJO>();
			UsuarioPOJO user = new UsuarioPOJO();
			user.apellido_materno = "martinez";
			UsuarioPOJO user2 = new UsuarioPOJO();
			user2.apellido_materno = "rodríguez";
			_events.Add(user);
			_events.Add(user2);
		}*/
		/*
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
	CheckConnection();
	ShowProgress("Validando");
	var times = dateToSend.ToString("yyyy-MM-dd HH:MM:ss");
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
					foreach (var item in _calls)
					{
						App.CurrentApp.RealmInstance.Add(item);
					}
				});
	}

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
	HideProgress();	}

*/


		async void getRegion()
		{
			if (PropertiesManager.IsDateRegionTrue())
			{
				PropertiesManager.SaveRegionDate(dateToSend);
			}
			else
			{
				dateToSend = PropertiesManager.GetDataRegion();
			}
			CheckConnection();
			ShowProgress("Validando");
			var times = dateToSend.ToString("yyyy-MM-dd HH:MM:ss");
			_regionSer = await ClientGuanajoven.getRegion(times);
			var regiones = App.CurrentApp.RealmInstance.All<Region>().ToList();
			if (_regionSer != null && _regionSer.Count > 0)
			{
				MergeLists(_regionSer);
				/*	App.CurrentApp.RealmInstance.Write(() =>
							{
								if (regiones.Count > 0)
								{
									foreach (var region in regiones)
									{
										foreach (var _region in _regionSer)
										{
											if (region.id_region == _region.id_region)
											{
												App.CurrentApp.RealmInstance.Remove(region);
											}

										}
									}
								}

								foreach (var item in _regionSer)
								{
									if (item.deleted_at != null)
									{
									}
									else
									{

										App.CurrentApp.RealmInstance.Add(item);
									}
								}
							});*/
			}

			regiones = App.CurrentApp.RealmInstance.All<Region>().ToList();
			if (regiones != null)
			{

				dateToSend = time;
				PropertiesManager.SaveRegionDate(dateToSend);
				regiones = regiones.OrderByDescending(x => x.created_at).ToList();
				_itemsList.Clear();
				//	eventos = App.CurrentApp.RealmInstance.All<Evento>().ToList();
				foreach (var item in regiones)
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



		void MergeLists(List<Region> Mergeregiones)
		{

			App.CurrentApp.RealmInstance.Write(() =>
				{
					foreach (var _evento in Mergeregiones)
					{
					App.CurrentApp.RealmInstance.RemoveRange(App.CurrentApp.RealmInstance.All<Region>().Where(x => x.id_region == _evento.id_region));
					}

					foreach (var item in Mergeregiones)
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
			if (stack.BindingContext is Region)
			{
				var region = stack.BindingContext as Region;

				await Navigation.PushAsync(new RegionPageDetail(region));
			}
		}

		async void CloseClicked(object sender, System.EventArgs e)
		{
			var image = sender as Image;
			image.Opacity = 0.6;
			image.FadeTo(1);
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
