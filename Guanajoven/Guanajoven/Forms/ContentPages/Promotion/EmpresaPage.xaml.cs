using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Plugin.Connectivity;
using Xamarin.Forms;

namespace Guanajoven
{
	public partial class EmpresaPage : BasePage
	{


		HomeDrawerPage RootPage;
		public static bool Changed = false;
		public static List<Empresa> _empresas = null;
		ObservableCollection<Empresa> _itemsList = new ObservableCollection<Empresa>();
		static string timeString = DateTime.Now.ToString("yyyy-MM-dd HH:MM:ss");
		static DateTime time = DateTime.Parse(timeString);
		static DateTime dateToSend = time;
		bool filter = false;

		public EmpresaPage(HomeDrawerPage _rootPage)
		{
			InitializeComponent();
			RootPage = _rootPage;
			NavigationPage.SetHasNavigationBar(this, false);
			_flowListView.FlowItemTapped += (sender, e) =>
			{
				//Empresa _next = null;
				//var empresas = e.Item;
				if (filter)
				{
					List<Empresa> pivot = new List<Empresa>();
					var index = _flowListView.FlowItemsSource.IndexOf(e.Item);
					var empresas = App.CurrentApp.RealmInstance.All<Empresa>().ToList().OrderBy(x => x.prioridad).ToList();
					foreach (var x in empresas)
					{
						foreach (var y in EmpresasFilter)
						{
							if (x.id_empresa == y.id_empresa)
							{
								pivot.Add(x);
							}
						}
					}
					var item = pivot[index];
					if (item != null)
					{
						//_next = App.CurrentApp.RealmInstance.All<Empresa>().Where(d => d.id_empresa == item.id_empresa).First(); 
						filter = false;
						filterSearch.Text = "";
						Navigation.PushAsync(new PromotionPage(item));
					}
				}
				else
				{
					var empresas = App.CurrentApp.RealmInstance.All<Empresa>().ToList().OrderBy(x => x.prioridad).ToList();
					var index = _flowListView.FlowItemsSource.IndexOf(e.Item);
					var item = empresas[index];
					if (item != null)
					{
						//_next = App.CurrentApp.RealmInstance.All<Empresa>().Where(d => d.id_empresa == item.id_empresa).First(); 
						Navigation.PushAsync(new PromotionPage(item));
					}
				}
			};

		}


		async void TapItem(object sender, System.EventArgs e)
		{
			var stack = (Button)sender;
			await stack.ScaleTo(0.9, 100, Easing.SinIn);
			await stack.ScaleTo(1, 100, Easing.SinIn);
			if (stack.BindingContext is Empresa)
			{
				var empresa = stack.BindingContext as Empresa;
				var empresas = App.CurrentApp.RealmInstance.All<Empresa>().ToList().OrderBy(x => x.prioridad).ToList();
				var index = _flowListView.FlowItemsSource.IndexOf(empresa);
				var item = empresas[index];

				//var Empresa = from em in App.CurrentApp.RealmInstance.All<Empresa>() where em.id_empresa == Stackempresa.id_empresa select em;

				Device.OpenUri(new Uri(item.url_empresa));

			}



		}




		protected override void OnAppearing()
		{
			base.OnAppearing();
			//test();
			getPromotions();
		}

		/*public void test()
		{
			_promotions = new List<Promotion>();
			Promotion user = new Promotion();
			user.evento_estado = "Abierto";
			user.titulo = "Oxxo";
			user.descripcion = "oferta red bull";
			user.fecha = "25/09/1992";
			user.fecha_fin = "26/09/1992";
			user.fecha_inicio = "27/09/1992";
			Promotion user2 = new Promotion();
			user2.evento_estado = "Cerrado";
			user2.titulo = "7eleven";
			user2.descripcion = "Monster";
			user2.fecha = "27/10/2007";
			user2.fecha_fin = "26/09/1992";
			user2.fecha_inicio = "27/09/1992";
			_promotions.Add(user);
			_promotions.Add(user2);
			ListView.ItemsSource = _promotions;
		}*/



		async void getPromotions()
		{
			if (PropertiesManager.IsDatePromotionTrue())
			{
				PropertiesManager.SavePromotionDate(dateToSend);
			}
			else
			{
				dateToSend = PropertiesManager.GetDataPromotion();
			}
			if (PropertiesManager.IsFirstDatePromotionTrue())
			{
				PropertiesManager.SaveFirstPromotionDate(1);
			}
			else
			{
				PropertiesManager.SaveFirstPromotionDate(PropertiesManager.GetFirstDataPromotion() + 1);
			}
			CheckConnection();
			ShowProgress("Validando");
			String times = "";
			if (PropertiesManager.GetFirstDataPromotion() > 1)
			{
				 times = dateToSend.ToString("yyyy-MM-dd HH:MM:ss");

			}
			else
			{
				 times = "0000-00-00 00:00:00";
			}
			//var times = dateToSend.ToString("yyyy-MM-dd HH:MM:ss");
			_empresas = await ClientGuanajoven.getPromotions(times);
			var empresas = App.CurrentApp.RealmInstance.All<Empresa>().ToList();
			if (_empresas != null && _empresas.Count > 0)
			{
				MergeLists(_empresas);
			}

			empresas = App.CurrentApp.RealmInstance.All<Empresa>().ToList().OrderBy(x => x.prioridad).ToList();
			if (empresas != null)
			{
				dateToSend = time;
				PropertiesManager.SavePromotionDate(dateToSend);
				_flowListView.FlowItemsSource = empresas.ToList();

			}
			HideProgress();
		}

		void MergeLists(List<Empresa> empresas)
		{

			App.CurrentApp.RealmInstance.Write(() =>
				{
					foreach (var _empresa in empresas)
					{
						App.CurrentApp.RealmInstance.RemoveRange(App.CurrentApp.RealmInstance.All<Empresa>().Where(x => x.id_empresa == _empresa.id_empresa));
					}

					foreach (var item in empresas)
					{
						if (item.deleted_at == null && item.estatus == "Activo")
						{
							App.CurrentApp.RealmInstance.Add(item);
						}
					}
				});
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


		private List<Empresa> _EmpresasFilter;

		public List<Empresa> EmpresasFilter
		{
			get
			{
				return _EmpresasFilter;
			}
			set
			{
				if (_EmpresasFilter != value)
				{
					_EmpresasFilter = value;
				}
			}
		}



		public async void DynamicEditor_TextChanged(object sender, TextChangedEventArgs e)
		{
			if (_empresas != null)
			{
				if (String.IsNullOrEmpty(filterSearch.Text))
				{
					filter = false;
					EmpresasFilter = _empresas.ToList();
					_flowListView.FlowItemsSource = EmpresasFilter;
				}
				else
				{
					filter = true;
					EmpresasFilter = _empresas.Where(x => x.empresa.ToLower().Contains(filterSearch.Text.ToLower())).ToList();
																 //  || x.nombre_comercial.ToLower().Contains(filterSearch.Text.ToLower())).ToList();
					_flowListView.FlowItemsSource = EmpresasFilter;
				}

			}
		}
	}
}
