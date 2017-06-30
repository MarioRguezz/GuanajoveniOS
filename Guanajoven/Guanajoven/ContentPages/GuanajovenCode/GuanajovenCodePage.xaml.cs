using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Newtonsoft.Json;
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
				ShowProgress("Validando");
				var response = await ClientGuanajoven.getToken(user.data.api_token);
				if (ValidateResponse(response))
				{
					var codigo = JsonConvert.DeserializeObject<CodigoPOJO>(response);
					token = codigo.data;
					ShowProgress(IProgressType.LogedIn);
					await Task.Delay(600);
					HideProgress();
				}
			}
			if(!string.IsNullOrEmpty(token)){
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

	}
}
