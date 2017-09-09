using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Guanajoven;
using Plugin.Connectivity;
using Xamarin.Forms;

namespace Guanajoven
{

	public enum DrawerPage
	{
		HomeView,
		ProfileView,
		CodigoView,
		Cerrar,
		EventosView,
		ConvocatoriasView,
		NotificacionesView,
		PromotionView,
		RedesSocialesView,
		ChatView,
		RegionesView,
		AcercaView
	}
	public partial class DrawerListPage : BasePage
	{

		HomeDrawerPage _rootPage;
		public Action<DrawerPage> PageSelected;
		static string timeString = DateTime.Now.ToString("yyyy-MM-dd HH:MM:ss");
		static DateTime time = DateTime.Parse(timeString);
		static DateTime dateToSend = time;
		public static List<Publicidad> _advertising = null;
		public DrawerListPage(HomeDrawerPage rootPage)
		{



			_rootPage = rootPage;
			InitializeComponent();
			var user = PropertiesManager.GetUserInfo();
			if (user != null)
			{
				_imageView.Source = user.data.datos_usuario.ruta_imagen;
				_email.Text = user.data.email;
				_nombre.Text = user.data.datos_usuario.nombre;
				_score.Text = user.data.puntaje + " Puntos";
				_podium.Text = "Posición " + user.data.posicion;
			}

			Title = " ";

			ListView.ItemsSource = PageTypeGroup.All;

			ListView.ItemSelected += (sender, e) =>
			{
				if (e.SelectedItem == null)
					return;
				var item = e.SelectedItem as ItemDrawer;
				if (PageSelected != null)
					PageSelected(item.Page);

				/*foreach (var n in PageTypeGroup.All)
				{
					n.Selected = false;
				}*/
				item.Selected = true;
				ListView.ItemsSource = PageTypeGroup.All;
				ListView.SelectedItem = null;
			};

			ImageSourceChanged = async () =>
			{
				if (LastView is FFImageLoading.Forms.CachedImage)
					(LastView as FFImageLoading.Forms.CachedImage).Source = Source;

				_imageView.Source = Source;

				//await PostLastFoto();
			};


			//ImagesUploaded += (folio) =>
			//{
			//	ActualizarFotoCliente(folio);
			//};

			//MessagingCenter.Subscribe<PerfilPage>(this, "update_info_user", (sender) =>
			//{
			//		// do something whenever the "Hi" message is sent
			//		if (App.CurrentApp.User != null)
			//		_labelNombre.Text = App.CurrentApp.User.Nombre;
			//	GetFoto();
			//});

			GetFoto();
			AdvertisingUpdate();


			MessagingCenter.Subscribe<ProfilePage>(this, "Hi", (sender) =>
		{
			var usertmp = PropertiesManager.GetUserInfo();
			if (usertmp != null)
			{
				_imageView.Source = usertmp.data.datos_usuario.ruta_imagen;
			}
		});


			MessagingCenter.Subscribe<DetailEventPage>(this, "Score", (sender) =>
	{
		var usertmp = PropertiesManager.GetUserInfo();
		if (usertmp != null)
		{
					_score.Text = usertmp.data.puntaje+ " Puntos";
					_podium.Text = "Posición " + usertmp.data.posicion;
		}
	});
		}



		async void AdvertisingUpdate()
		{
			if (PropertiesManager.IsDateAdvertisingTrue())
			{
				PropertiesManager.SaveAdvertisingDate(dateToSend);
			}
			else
			{
				dateToSend = PropertiesManager.GetDataAdvertising();
			}
			CheckConnection();
			//ShowProgress("Validando");
			var times = dateToSend.ToString("yyyy-MM-dd HH:MM:ss");
			_advertising = await ClientGuanajoven.getAdvertising(times);
			var publicidad = App.CurrentApp.RealmInstance.All<Publicidad>().ToList();
			if (_advertising != null && _advertising.Count > 0)
			{
				MergeLists(_advertising);
			}

			publicidad = App.CurrentApp.RealmInstance.All<Publicidad>().ToList();
			if (publicidad != null)
			{

				dateToSend = time;
				PropertiesManager.SaveEventDate(dateToSend);
				publicidad = publicidad.OrderByDescending(x => x.fecha_inicio).ToList();
			}
			//HideProgress();
		}

		void MergeLists(List<Publicidad> publicidad)
		{

			App.CurrentApp.RealmInstance.Write(() =>
				{
					foreach (var item in publicidad)
					{
						App.CurrentApp.RealmInstance.RemoveRange(App.CurrentApp.RealmInstance.All<Publicidad>().Where(x => x.id_publicidad == item.id_publicidad));
					}

					foreach (var item in publicidad)
					{
						if (item.deleted_at == null)
						{
							App.CurrentApp.RealmInstance.Add(item);
						}
					}
				});
		}



		async void GetFoto()
		{
			//try
			//{
			//	var cliente = App.CurrentApp.User;
			//	var n = await App.CurrentApp.Services.GetFotos(new List<string>() { "" + cliente.Foto });
			//	if (n != null && n.Count > 0)
			//	{
			//		var str = n[0].ArchivoURL;
			//		str = str.Replace("http://www.totalcase.com.mx/Uploads/TTArchivos/", Configuration.BaseURL + "Uploads/TTArchivos/");
			//		_imageView.Source = str;
			//	}
			//}
			//catch
			//{

			//}
		}



		async void ActualizarFotoCliente(int folio)
		{
			//var cliente = GetCliente();
			//cliente.Foto = folio;

			//var resp = await App.CurrentApp.Services.PutObjectToTable<Cliente>(cliente, cliente.Folio + "", Cliente.TABLE_NAME);

			//if (resp == 0)
			//{

			//}
			//else
			//{

			//}
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

		async void ChangePicture(object sender, EventArgs e)

		{
			TakePictureActionSheet(_imageView);
		}

		void MenuClicked(object sender, System.EventArgs e)
		{
			var image = sender as Image;
			image.Opacity = 0.6;
			image.FadeTo(1);
			_rootPage.IsPresented = false;
		}

		public void SelectPage(DrawerPage page)
		{
			/*foreach (var n in PageTypeGroup.All)
			{
				if (n.Page == page)
					n.Selected = true;
				else n.Selected = false;
			}*/
			ListView.ItemsSource = PageTypeGroup.All;
			//ListView.ItemsSource = new ObservableCollection<DrawerListPage.ItemDrawer>(list);
			ListView.SelectedItem = null;
		}

		public class ItemDrawer
		{
			public string Label { get; set; }
			public DrawerPage Page { get; set; }
			public bool Selected { get; set; }

			public string Image { get; set; }

			public ItemDrawer(string label, DrawerPage page, string image)
			{
				Label = label;
				Page = page;
				Image = image;
			}
		}


		public class PageTypeGroup : List<ItemDrawer>
		{
			public string Title { get; set; }
			public string ShortName { get; set; } //will be used for jump lists
			public string Subtitle { get; set; }
			private PageTypeGroup(string title, string shortname)
			{
				Title = title;
				ShortName = shortname;
			}

			public static IList<PageTypeGroup> All { private set; get; }

			static PageTypeGroup()
			{
				var user = PropertiesManager.GetUserInfo();
				int fechainicio = Int32.Parse(DateTime.Parse(user.data.datos_usuario.fecha_nacimiento).ToString("yyyy"));
				DateTime date = DateTime.Today;
				int fechaactual = Int32.Parse(DateTime.Parse(date.ToString()).ToString("yyyy"));
				int edad = fechaactual - fechainicio;

				if (edad >= 30)
				{
					List<PageTypeGroup> Groups = new List<PageTypeGroup> {
			new PageTypeGroup ("Inicio", "Inicio"){
				new ItemDrawer ("Home",DrawerPage.HomeView, "home.png"),
				new ItemDrawer("Modificar perfil", DrawerPage.ProfileView, "profile.png"),
				new ItemDrawer("Cerrar sesión", DrawerPage.Cerrar, "turn-on.png")
			},
			new PageTypeGroup("Actividades", "Actividades")
			{
				new ItemDrawer("Eventos", DrawerPage.EventosView, "event.png"),
				new ItemDrawer("Convocatorias", DrawerPage.ConvocatoriasView, "convocatorias.png"),
				new ItemDrawer("Notificaciones", DrawerPage.NotificacionesView, "notificacion.png"),
				new ItemDrawer("Redes sociales", DrawerPage.RedesSocialesView, "socialmedia.png"),
				new ItemDrawer("Chat", DrawerPage.ChatView, "chat.png")
			},
			new PageTypeGroup("General", "General")
			{
				new ItemDrawer("Regiones", DrawerPage.RegionesView, "skyline.png"),
				new ItemDrawer("Acerca de ", DrawerPage.AcercaView, "info.png")
			}
				};
					All = Groups; //set the publicly accessible list
				}
				else
				{
					List<PageTypeGroup> Groups = new List<PageTypeGroup> {
			new PageTypeGroup ("Inicio", "Inicio"){
				new ItemDrawer ("Home",DrawerPage.HomeView, "home.png"),
				new ItemDrawer("Modificar perfil", DrawerPage.ProfileView, "profile.png"),
				new ItemDrawer("IDGuanajoven", DrawerPage.CodigoView, "code.png"),
				new ItemDrawer("Cerrar sesión", DrawerPage.Cerrar, "turn-on.png")
			},
			new PageTypeGroup("Actividades", "Actividades")
			{
				new ItemDrawer("Eventos", DrawerPage.EventosView, "event.png"),
				new ItemDrawer("Convocatorias", DrawerPage.ConvocatoriasView, "convocatorias.png"),
				new ItemDrawer("Promociones", DrawerPage.PromotionView, "promotion.png"),
				new ItemDrawer("Notificaciones", DrawerPage.NotificacionesView, "notificacion.png"),
				new ItemDrawer("Redes sociales", DrawerPage.RedesSocialesView, "socialmedia.png"),
				new ItemDrawer("Chat", DrawerPage.ChatView, "chat.png")
			},
			new PageTypeGroup("General", "General")
			{
				new ItemDrawer("Regiones", DrawerPage.RegionesView, "skyline.png"),
				new ItemDrawer("Acerca de ", DrawerPage.AcercaView, "info.png")
			}
				};
					All = Groups; //set the publicly accessible list
				}





			}
		}


	}


}
