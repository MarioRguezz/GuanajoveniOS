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
		string id_promocion = null;
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
			id_promocion = promotion.id_promocion + "";
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
			var answer = await DisplayAlert("Guanajoven", "Al aplicar esta promoción quedará registrado en la base de datos que has sido beneficiado por la empresa, ¿Estas seguro de Aplicar esta promoción?", "Sí", "No");
			if (!answer)
			{
			}
			else
			{
				var user = PropertiesManager.GetUserInfo();
				if (user != null)
				{
					CheckConnection();
					ShowProgress("Validando");
					var response = await ClientGuanajoven.setPromotion(id_promocion, user.data.codigo_guanajoven.token);
					if (ValidateResponse(response, checkError(response)))
					{
						ShowProgress(IProgressType.LogedIn);
						_qrImage.IsVisible = true;
						await Task.Delay(600);
						await DisplayAlert("Guanajoven", "Promoción aplicada", "Aceptar");
					}
					HideProgress();
				}
			}

		}



		string checkError(string response)
		{
			return ClientGuanajoven.IsErrorType(response);
		}


		bool ValidateResponse(string response, string text)
		{
			if (ClientGuanajoven.IsError(response))
			{
				if (text != "")
				{
					DisplayAlert("Error", text, "Aceptar");
				}
				/*else
				{
					DisplayAlert("Error", "Código Aplicado", "Aceptar");
				}*/
				return false;
			}
			else
			{
				return true;
			}
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


