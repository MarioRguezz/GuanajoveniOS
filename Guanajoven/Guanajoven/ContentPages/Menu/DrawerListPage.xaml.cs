using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Guanajoven;
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
		CalendarioView,
		RedesSocialesView,
		ChatView,
		RegionesView,
		AcercaView
	}
	public partial class DrawerListPage : BasePage
	{

		HomeDrawerPage _rootPage;
		public Action<DrawerPage> PageSelected;

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
				List<PageTypeGroup> Groups = new List<PageTypeGroup> {
			new PageTypeGroup ("Inicio", "Inicio"){
				new ItemDrawer ("Home",DrawerPage.HomeView, "home.png"),
				new ItemDrawer("Modificar perfil", DrawerPage.ProfileView, "profile.png"),
				new ItemDrawer("Codigo Guanajoven", DrawerPage.CodigoView, "code.png"),
				new ItemDrawer("Cerrar sesión", DrawerPage.Cerrar, "turn-on.png")
			},
			new PageTypeGroup("Actividades", "Actividades")
			{
				new ItemDrawer("Eventos", DrawerPage.EventosView, "event.png"),
				new ItemDrawer("Convocatorias", DrawerPage.ConvocatoriasView, "convocatorias.png"),
				//new ItemDrawer("Calendario de eventos", DrawerPage.CalendarioView, "calendar.png"),
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
