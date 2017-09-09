using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Xamarin.Forms;
using Newtonsoft.Json;
using Plugin.Connectivity;
using ZXing;




namespace Guanajoven
{


	public partial class DetailPromotionPage : BasePage
	{

		string url = null;
		public DetailPromotionPage(Promotion promotion, string imagen)
		{
			InitializeComponent();
			Title = promotion.titulo;
			_image.Source = imagen;
			_title.Text = promotion.titulo;
			_descripcion.Text = promotion.descripcion;
			_bases.Text = promotion.bases;
			_codigo_promocion.Text = promotion.codigo_promocion;
			url = promotion.url_promocion;
			_fechainicio.Text = DateTime.Parse(promotion.fecha_inicio).ToString("dd/MM/yyyy");
			_fechafin.Text = DateTime.Parse(promotion.fecha_fin).ToString("dd/MM/yyyy");
			var user = PropertiesManager.GetUserInfo();
			var stream = DependencyService.Get<IBarcodeService>().ConvertImageStream(user.data.codigo_guanajoven.token);
			_qrImage.Source = ImageSource.FromStream(() => { return stream; });

		}


		async void information(object sender, System.EventArgs e)
		{
			if (url == null)
			{
				await DisplayAlert("Guanajoven", "Esta promoción no cuenta con sitio", "Aceptar");
			}
			else
			{
				Device.OpenUri(new Uri(url));
			}

		}


		async void _promotionApplied(object sender, System.EventArgs e)
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

			var stream = DependencyService.Get<IBarcodeService>().ConvertImageStream("123");
			_qrImage.IsVisible = true;
			_qrImage.Source = ImageSource.FromStream(() => { return stream; });
			await DisplayAlert("Guanajoven", "Promoción aplicada", "Aceptar");
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


