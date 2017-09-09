using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Plugin.Connectivity;
using Xamarin.Forms;
using ZXing;

namespace Guanajoven
{

	public interface IBarcodeService
	{
		Stream ConvertImageStream(string text, int width = 500, int height = 500);
	}

	public partial class GuanajovenCodePage : BasePage
	{
		HomeDrawerPage RootPage;
		public GuanajovenCodePage(HomeDrawerPage _rootPage)
		{
			InitializeComponent();
			RootPage = _rootPage;
			var user = PropertiesManager.GetUserInfo();
			_background.Source = user.data.datos_usuario.ruta_imagen;
			_name.Text = user.data.datos_usuario.nombre + " " + user.data.datos_usuario.apellido_paterno + " " + user.data.datos_usuario.apellido_materno;
			_email.Text = user.data.email;
			_numero.Text = user.data.codigo_guanajoven.id_usuario + "";
			var stream = DependencyService.Get<IBarcodeService>().ConvertImageStream(user.data.codigo_guanajoven.token);
			_qrImage.Source = ImageSource.FromStream(() => { return stream; });
			_curpcode.Text = user.data.datos_usuario.curp;
			_genre.Text = user.data.datos_usuario.genero.nombre;
			_municipio.Text = user.data.datos_usuario.municipio.nombre;
			DateTime myDate = DateTime.Parse(user.data.datos_usuario.fecha_nacimiento);
			_fechanacimiento.Text = myDate.ToString("dd-MM-yyyy");
			_estadonacimiento.Text = user.data.datos_usuario.estado_nacimiento.nombre;
			Background.BackgroundColor = Color.FromHex("#b7C7E1F5");
			NavigationPage.SetHasNavigationBar(this, false);
		}

		protected override void OnAppearing()
		{
			base.OnAppearing();
			GetToken();
		}

		async void GetToken()
		{
			string token = "";
			var user = PropertiesManager.GetUserInfo();
			if (user != null)
			{
				CheckConnection();
				ShowProgress("Validando");
				var response = await ClientGuanajoven.getToken(user.data.api_token);
				if (ValidateResponse(response))
				{
					var codigo = JsonConvert.DeserializeObject<CodigoPOJO>(response);
					token = codigo.data;
					ShowProgress(IProgressType.LogedIn);
					await Task.Delay(600);
				}
				HideProgress();
			}
			if (!string.IsNullOrEmpty(token))
			{
				var stream = DependencyService.Get<IBarcodeService>().ConvertImageStream(token);
				_qrImage.Source = ImageSource.FromStream(() => { return stream; });
			}

		}
		bool ValidateResponse(string response)
		{
			if (ClientGuanajoven.IsError(response))
			{
				DisplayAlert("Error", "No se pudo recuperar su código", "Aceptar");
				return false;
			}
			else
			{
				return true;
			}
		}

		async void DownloadClicked(object sender, EventArgs args)
		{
			//PERMITE GUARDAR UNA VISTA DE LA IMAGEN SE CAMBIA POR ENVÍO DE CORREO
			/*var stream = await DependencyService.Get<IScreenshotManager>().CaptureAsync();
			//var stream1 = new MemoryStream(stream);
			//imageTest.Source = ImageSource.FromStream(() => stream1);
			//uses the IPicture interface to use the appropriate saving mechanism from the picture class in each individual project
			DependencyService.Get<IPicture>().SavePictureToDisk("Guanajoven", stream);
			//generic success message
			await DisplayAlert("Guanajoven", "La imagen ha sido guardada", "Aceptar");*/
			CheckConnection();
			ShowProgress("Validando");
			var user = PropertiesManager.GetUserInfo();
			var response = await ClientGuanajoven.sendEmailID(user.data.api_token + "");

			if (response)
			{
				await Task.Delay(600);
				await DisplayAlert("Guanajoven", "Se te ha enviado un correo electrónico con tu credencial, gracias por hacer de Guanajuato tu proyecto de vida", "Aceptar");
			}

			HideProgress();

		}





		async void MenuClicked(object sender, EventArgs args)
		{
			RootPage.IsPresented = true;
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
