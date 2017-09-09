using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Softweb.Controls;
using Xamarin.Forms;

namespace Guanajoven
{
	public partial class MasterHomePage : ContentPage
	{
		string codigo = null;
		string promocion = null;
		int edad = 0;
		void Handle_Clicked(object sender, System.EventArgs e)
		{
			throw new NotImplementedException();
		}

		HomeDrawerPage RootPage;
		public MasterHomePage(HomeDrawerPage _rootPage)
		{
			InitializeComponent();
			RootPage = _rootPage;

			NavigationPage.SetHasNavigationBar(this, false);

			var user = PropertiesManager.GetUserInfo();
			int fechainicio = Int32.Parse(DateTime.Parse(user.data.datos_usuario.fecha_nacimiento).ToString("yyyy"));
			DateTime date = DateTime.Today;
			int fechaactual = Int32.Parse(DateTime.Parse(date.ToString()).ToString("yyyy"));
			edad = fechaactual - fechainicio;
			if (edad >= 30)
			{
				_codigoText.Text = "Regiones";
				_codigoText2.Text = "";
				_imagecodigo.Source = "regionesmenu.png";
				_imagepromocion.Source = "notificacionmenu";
				_promocionText.Text = "Notificaciones";

			}
			InitViews();

			UpdateView();
		}

		protected override void OnAppearing()
		{
			base.OnAppearing();

			InitLocationListening();

			CheckNotifications();
		}

		async void CheckNotifications()
		{

			if (App.CurrentApp.FromRemoteNotification)
			{
				/*await DisplayAlert(App.CurrentApp.NotificationData["title"],
									   //App.CurrentApp.NotificationData["body"],
									   string.Join(",", App.CurrentApp.NotificationData.Keys)
									   + ":" + string.Join(",", App.CurrentApp.NotificationData.Values),
									   "Aceptar");*/



				if (App.CurrentApp.NotificationData.ContainsKey("url"))
				{
					if (App.CurrentApp.NotificationData["url"] != "chat")
					{


						await DisplayAlert(App.CurrentApp.NotificationData["title"],
								   App.CurrentApp.NotificationData["body"],
																		   "Aceptar");

						App.CurrentApp.RealmInstance.Write(() =>
						{
							var notifi = new Notificacion();
							notifi.url = App.CurrentApp.NotificationData["url"];
							notifi.titulo = App.CurrentApp.NotificationData["title"];
							notifi.mensaje = App.CurrentApp.NotificationData["body"];
							notifi.fecha_emision = DateTime.Now.ToString("u");
							App.CurrentApp.RealmInstance.Add(notifi);
						});

						Device.OpenUri(new System.Uri(App.CurrentApp.NotificationData["url"]));
					}
					else
					{
						//ES CHAT
						UIMessage.ShowToast("¡Te enviaron un nuevo mensaje!", ToastMessage.ToastPosition.TOP, ToastMessage.Duration.Long
										 , null, Color.FromHex("#003464"));
					}

				}
				else
				{
					string url = null;

					if (App.CurrentApp.NotificationData.ContainsKey("content"))
					{
						url = await ClientGuanajoven.GetURL(App.CurrentApp.NotificationData["title"], App.CurrentApp.NotificationData["body"]);
						url = ClientGuanajoven.Data(url);
						//await DisplayAlert("","url:"+ ClientGuanajoven.Data(url),"k");
					}

					App.CurrentApp.RealmInstance.Write(() =>
					{
						var notifi = new Notificacion();
						notifi.url = url;
						notifi.titulo = App.CurrentApp.NotificationData["title"];
						notifi.mensaje = App.CurrentApp.NotificationData["body"];
						notifi.fecha_emision = DateTime.Now.ToString("u");

						App.CurrentApp.RealmInstance.Add(notifi);
					});

					if (url != null)
					{
						if (url != "chat" && url != "")
						{
							await DisplayAlert(App.CurrentApp.NotificationData["title"],
								   App.CurrentApp.NotificationData["body"],
																			   "Aceptar");
							Device.OpenUri(new System.Uri(url));
						}
						else
						{
							//chat
							UIMessage.ShowToast("¡Te enviaron un nuevo mensaje!", ToastMessage.ToastPosition.TOP, ToastMessage.Duration.Long
										 , null, Color.FromHex("#003464"));
						}


					}
				}
			}
		}

		async void InitLocationListening()
		{
			LocationHelper.Instance.Geolocator.PositionChanged += Geolocator_PositionChanged;

			if (!LocationHelper.Instance.Geolocator.IsListening)
			{
				try { await LocationHelper.Instance.Geolocator.StartListeningAsync(TimeSpan.FromSeconds(10), 1); } catch (Exception ex) { }
				//already listening
			}

		}

		void Geolocator_PositionChanged(object sender, Plugin.Geolocator.Abstractions.PositionEventArgs e)
		{
			System.Diagnostics.Debug.WriteLine("ubicacion: {0},{1}", e.Position.Latitude, e.Position.Longitude);
		}

		protected override void OnDisappearing()
		{
			base.OnDisappearing();

			LocationHelper.Instance.Geolocator.PositionChanged -= Geolocator_PositionChanged;
			LocationHelper.Instance.Geolocator.StopListeningAsync();
		}

		void InitViews()
		{

		}

		void UpdateView()
		{

		}

		async void VideoClicked(object sender, EventArgs args)
		{
			await Navigation.PushAsync(new videoPage());
		}

		async void MenuClicked(object sender, EventArgs args)
		{
			RootPage.IsPresented = true;
		}


		async void Advertising(object sender, EventArgs args)
		{
			await Navigation.PushAsync(new AdvertisingPage());
			//await Navigation.PushModalAsync(new AdvertisingPage());
		}


		async void GuanajovenClicked(object sender, EventArgs args)
		{
			var image = sender as Image;
			image.Opacity = 0.6;
			if (edad >= 30)
			{
				await Navigation.PushAsync(new RegionPage(RootPage));

			}
			else
			{
				await Navigation.PushAsync(new GuanajovenCodePage(RootPage));
			}
		}

		async void EventosClicked(object sender, EventArgs args)
		{
			var image = sender as Image;
			image.Opacity = 0.6;
			await Navigation.PushAsync(new EventsView(RootPage));
		}

		async void CheckClicked(object sender, EventArgs args)
		{
			var image = sender as Image;
			image.Opacity = 0.6;
			if (edad >= 30)
			{
				await Navigation.PushAsync(new NotificationPage(RootPage));

			}
			else
			{
				await Navigation.PushAsync(new EmpresaPage(RootPage));
			}
		}

		async void ConvocaClicked(object sender, EventArgs args)
		{
			var image = sender as Image;
			image.Opacity = 0.6;
			await Navigation.PushAsync(new CallPage(RootPage));
		}

		async void RedClicked(object sender, EventArgs args)
		{
			var image = sender as Image;
			image.Opacity = 0.6;
			await Navigation.PushAsync(new SocialMediaPage(RootPage));
		}

		async void ChatClicked(object sender, EventArgs args)
		{
			var image = sender as Image;
			image.Opacity = 0.6;
			await Navigation.PushAsync(new ChatPage(RootPage));
		}

		async void bolsadetrabajo(object sender, EventArgs args)
		{
			Device.OpenUri(new Uri("http://www.guanajuato.gob.mx/empleo.php"));
		}

		void SignUpClicked(object sender, EventArgs args)
		{

			RootPage.IsPresented = true;
		}




	}

}