using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Plugin.Connectivity;
using Xamarin.Forms;

namespace Guanajoven
{
	public partial class LoginPage : BasePage
	{
		public LoginPage()
		{
			InitializeComponent();
			NavigationPage.SetHasNavigationBar(this, false);
			//Background.BackgroundColor = Color.FromHex("#b7C7E1F5");
			Background.BackgroundColor = Color.FromHex("#CCFFFFFF");
			SetLoginActions();
		}

		void SetLoginActions()
		{
			FBLoginResponse += (fb_user) =>
			{
				System.Diagnostics.Debug.WriteLine("FB LOGIN, INFO:");
				System.Diagnostics.Debug.WriteLine("{0} : {1} {2} , {3}, {4}", fb_user.id,
												   fb_user.first_name,
												   fb_user.first_name,
												   fb_user.email,
												   fb_user.picture.data.url);
				FacebookCheck(fb_user.id, 0, fb_user.email, fb_user.picture.data.url);
			};

			GoogleLoginResponse += (google_user) =>
			{
				System.Diagnostics.Debug.WriteLine("GOOGLE, INFO:");
				System.Diagnostics.Debug.WriteLine("{0} : {1} {2} , {3}, {4}", google_user.id,
												   google_user.given_name,
												   google_user.family_name,
												   google_user.email,
												   google_user.picture);
				GoogleCheck(google_user.id, 1, google_user.email, google_user.picture);
			};
		}


		async void GoogleCheck(string id, int social, string email, string picture)
		{
			var responsefirst = await ClientGuanajoven.verifyemail(email);
			var y = checkError(responsefirst);
			if (checkError(responsefirst) != "Correo no encontrado")
			{
				//Enter to the system
				ShowProgress("Validando");
				var response = await ClientGuanajoven.loginGoogle(email, id);
				// checkError(response);
				if (ValidateResponse(response, checkError(response)))
				{
					var user = JsonConvert.DeserializeObject<ResponseUsuario>(response);
					var posicion = await ClientGuanajoven.getPosition(user.data.api_token);
					user.data.posicion = ClientGuanajoven.Data(posicion);
					PropertiesManager.SaveUserInfo(user);
					ShowProgress(IProgressType.LogedIn);
					await Task.Delay(600);
					HideProgress();
					await Navigation.PushModalAsync(new HomeDrawerPage());
				}
				HideProgress();
			}
			else
			{
				await Navigation.PushAsync(new SignUpPage(id, social, email, picture));
			}
		}



		async void FacebookCheck(string id, int social, string email, string picture)
		{
			var responsefirst = await ClientGuanajoven.verifyemail(email);
			var y = checkError(responsefirst);
			if (checkError(responsefirst) != "Correo no encontrado")
			{
				//Enter to the system
				ShowProgress("Validando");
				var response = await ClientGuanajoven.loginFacebook(email, id);
				// checkError(response);
				if (ValidateResponse(response, checkError(response)))
				{
					var user = JsonConvert.DeserializeObject<ResponseUsuario>(response);
					var posicion = await ClientGuanajoven.getPosition(user.data.api_token);
					user.data.posicion = ClientGuanajoven.Data(posicion);
					PropertiesManager.SaveUserInfo(user);
					ShowProgress(IProgressType.LogedIn);
					await Task.Delay(600);
					HideProgress();
					await Navigation.PushModalAsync(new HomeDrawerPage());
				}
				HideProgress();
			}
			else
			{
				await Navigation.PushAsync(new SignUpPage(id, social, email, picture));
			}
		}



		async void CloseClicked(object sender, System.EventArgs e)
		{
			var image = sender as Image;
			image.Opacity = 0.6;
			image.FadeTo(1);
			await Navigation.PopAsync();
		}

		protected override void OnAppearing()
		{
			base.OnAppearing();
		}



		void LoginGoogleTapped(object sender, System.EventArgs e)
		{
			GoogleLoginAction();
		}

		void LoginFacebookTapped(object sender, System.EventArgs e)
		{
			FBLoginAction();
		}


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


		async void forgotpassword(object sender, System.EventArgs e)
		{
			await Navigation.PushAsync(new ForgotPassword());
		}

		async void LoginClicked(object sender, System.EventArgs e)
		{
			if (ValidateUI())
			{
				CheckConnection();
				ShowProgress("Validando");
				var response = await ClientGuanajoven.signin(_email.Text, _password.Text);
				// checkError(response);
				if (ValidateResponse(response, checkError(response)))
				{
					var user = JsonConvert.DeserializeObject<ResponseUsuario>(response);
					var posicion = await ClientGuanajoven.getPosition(user.data.api_token);
					user.data.posicion = ClientGuanajoven.Data(posicion);
					PropertiesManager.SaveUserInfo(user);
					FirebaseObject firebase= new FirebaseObject();
					firebase.os = "ios";
 					firebase.id_usuario = user.data.id +"";
					firebase.device_token = App.CurrentApp.DeviceToken; //id de firebase_rg
					var sendtoken = await ClientGuanajoven.getIdFirebase(firebase);
					ShowProgress(IProgressType.LogedIn);
					await Task.Delay(600);
					HideProgress();
					await Navigation.PushModalAsync(new HomeDrawerPage());
				}
				HideProgress();
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
				if (text == "")
				{
					DisplayAlert("Error", "Verifique sus datos", "Aceptar");
				}
				else
				{
					DisplayAlert("Error", text, "Aceptar");
				}
				return false;
			}
			else
			{
				return true;
			}
		}

		bool ValidateUI()
		{
			if (string.IsNullOrEmpty(_email.Text) || !Regex.IsMatch(_email.Text, @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z", RegexOptions.IgnoreCase))
			{
				DisplayAlert("Error", "Verifique su correo", "Aceptar");

				return false;
			}


			if (string.IsNullOrEmpty(_password.Text))
			{
				DisplayAlert("Error", "Verifique su contraseña", "Aceptar");
				return false;
			}

			return true;
		}

	}
}
