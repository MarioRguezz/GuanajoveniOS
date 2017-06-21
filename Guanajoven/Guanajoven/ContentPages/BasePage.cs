using System;
using Xamarin.Forms;
using System.Text;
using Xamarin.Auth;

namespace Guanajoven
{
	public class BasePage : ContentPage
	{
		#region FB.Auth
		//it's set on the render
		public Action FBLoginAction;
		//it's set on InitActions();
		public Action<FacebookUserModel> FBLoginResponse;

		public Action GoogleLoginAction;
		public Action<GoogleUserModel> GoogleLoginResponse;
		#endregion



		public Action<string> ShowProgressMessage;
		public Action<IProgressType> ShowProgressType;
		public Action HideProgressAction;

		IProgress progressDependency;

		protected BasePage()
		{
			progressDependency = DependencyService.Get<IProgress>();

			GoogleLoginAction = GoogleLogin;

			FBLoginAction = FBLogin;
		}

		void FBLogin()
		{
 			var authenticator = new Xamarin.Auth.OAuth2Authenticator(
				clientId: OAuthSettingsFacebook.ClientId,
								//clientSecret: OAuthSettingsFacebook.ClientSecret,   // null or ""
								authorizeUrl: new Uri(OAuthSettingsFacebook.AuthorizeUrl),
								 //accessTokenUrl: new Uri(OAuthSettingsFacebook.AccessTokenUrl),
								 redirectUrl: new Uri($"fb{OAuthSettingsFacebook.ClientId}://authorize"),
								 scope: OAuthSettingsFacebook.Scope,
				isUsingNativeUI: true
				)
			{
				AllowCancel = true,
			};

			authenticator.Completed +=
				async (s, ea) =>
					{
						var sb = new StringBuilder();

						if (ea.Account != null && ea.Account.Properties != null)
						{
							sb.Append("Token = ").AppendLine($"{ea.Account.Properties["access_token"]}");

							var request = new OAuth2Request("GET", new Uri("https://graph.facebook.com/me?locale=en_US&fields=id,first_name,last_name,email,picture"), null, ea.Account);
							var res = await request.GetResponseAsync();

							string json = res.GetResponseText();
							System.Diagnostics.Debug.WriteLine(json);
							try
							{
								var user = Newtonsoft.Json.JsonConvert.DeserializeObject<FacebookUserModel>(json);
								if (FBLoginResponse != null)
									FBLoginResponse(user);
							}
							catch(Exception ex)
							{
								System.Diagnostics.Debug.WriteLine(ex.Message);
							}
							
						}
						else
						{
							sb.Append("Not authenticated ").AppendLine($"Account.Properties does not exist");
						}

						//DisplayAlert
						//		(
						//			"Authentication Results",
						//			sb.ToString(),
						//			"OK"
						//		);

						return;
					};

			authenticator.Error +=
				(s, ea) =>
					{
						StringBuilder sb = new StringBuilder();
						sb.Append("Error = ").AppendLine($"{ea.Message}");

						DisplayAlert
								(
									"Authentication Error",
									sb.ToString(),
									"OK"
								);
						return;
					};

			AuthenticationState.Authenticator = authenticator;

			Xamarin.Auth.Presenters.OAuthLoginPresenter presenter = null;
			presenter = new Xamarin.Auth.Presenters.OAuthLoginPresenter();
			presenter.Login(authenticator);

			//Xamarin.Auth.XamarinForms.AuthenticatorPage ap;
			//ap = new Xamarin.Auth.XamarinForms.AuthenticatorPage()
			//{
			//	Authenticator = authenticator,
			//};

			//NavigationPage np = new NavigationPage(ap);
			//await Navigation.PushModalAsync(np);

		}


		async void GoogleLogin()
		{
			var authenticator = new Xamarin.Auth.OAuth2Authenticator(
								 clientId: OAuthSettingsGoogle.ClientId,
				clientSecret: OAuthSettingsGoogle.ClientSecret,   // null or ""
								 authorizeUrl: new Uri(OAuthSettingsGoogle.AuthorizeUrl),
								 accessTokenUrl: new Uri(OAuthSettingsGoogle.AccessTokenUrl),
								 redirectUrl: new Uri(OAuthSettingsGoogle.RedirectUrl),
								 scope: OAuthSettingsGoogle.Scope,
					getUsernameAsync: null,
					isUsingNativeUI: true
				)
			{
				AllowCancel = true,
			};

			authenticator.Completed +=
				async (s, ea) =>
					{
						var sb = new StringBuilder();

						if (ea.Account != null && ea.Account.Properties != null)
						{
							sb.Append("Token = ").AppendLine($"{ea.Account.Properties["access_token"]}");

							var userInfoUrl = "https://www.googleapis.com/oauth2/v2/userinfo";
							var request = new OAuth2Request("GET", new Uri(userInfoUrl), null, ea.Account);
							var response = await request.GetResponseAsync();
							if (response != null)
							{
								string userJson = response.GetResponseText();
								System.Diagnostics.Debug.WriteLine(userJson);
								try
								{
								var user = Newtonsoft.Json.JsonConvert.DeserializeObject<GoogleUserModel>(userJson);
									if (GoogleLoginResponse != null)
										GoogleLoginResponse(user);
								}
								catch (Exception ex)
								{
									System.Diagnostics.Debug.WriteLine(ex.Message);
								}
							}
						}
						else
						{
							sb.Append("Not authenticated ").AppendLine($"Account.Properties does not exist");
						}

						//DisplayAlert
						//		(
						//			"Authentication Results",
						//			sb.ToString(),
						//			"OK"
						//		);

						return;
					};

			authenticator.Error +=
				(s, ea) =>
					{
						StringBuilder sb = new StringBuilder();
						sb.Append("Error = ").AppendLine($"{ea.Message}");

						DisplayAlert
								(
									"Authentication Error",
									sb.ToString(),
									"OK"
								);
						return;
					};

			AuthenticationState.Authenticator = authenticator;

			Xamarin.Auth.Presenters.OAuthLoginPresenter presenter = null;
			presenter = new Xamarin.Auth.Presenters.OAuthLoginPresenter();
			presenter.Login(authenticator);

			//Xamarin.Auth.XamarinForms.AuthenticatorPage ap;
			//ap = new Xamarin.Auth.XamarinForms.AuthenticatorPage()
			//{
			//	Authenticator = authenticator,
			//};

			////NavigationPage np = new NavigationPage(ap);
			//await Navigation.PushModalAsync(ap);

		}


		public void ShowProgress(string message)
		{
			if (progressDependency != null)
				progressDependency.ShowProgress(message);
			if (ShowProgressMessage != null)
				ShowProgressMessage(message);
		}

		public void ShowProgress(IProgressType type)
		{
			progressDependency = DependencyService.Get<IProgress>();
			if (progressDependency != null)
				progressDependency.ShowProgress(type);
			if (ShowProgressType != null)
				ShowProgressType(type);
		}

		public void HideProgress()
		{
			if (progressDependency != null)
				progressDependency.Dismiss();
			if (HideProgressAction != null)
				HideProgressAction();
		}

		#region PICTURES


		public string imagePath = null;
		public Action TakePicture, SelectFromGallery;

		public bool imageChanged = false;

		public string ImagePath
		{
			get
			{
				return imagePath;
			}

			set
			{
				imagePath = value;
				imageChanged = true;
				//if (_imageView != null)
				//	Device.BeginInvokeOnMainThread(() =>
				//	{
				//		_imageView.Source = imagePath;
				//	});
			}
		}

		public object LastView;
		public Action ImageSourceChanged;

		ImageSource _source;
		public ImageSource Source
		{
			set
			{
				imageChanged = true;
				_source = value;

				if (ImageSourceChanged != null)
					ImageSourceChanged();
			}
			get
			{
				return _source;
			}
		}

		byte[] _bytes;

		public byte[] bytes
		{
			get
			{
				return _bytes;
			}

			set
			{
				_bytes = value;

				Base64Image = Convert.ToBase64String(_bytes);
			}
		}

		public string Base64Image { get; set; }



		public async void TakePictureActionSheet(object imageView = null)
		{
			LastView = imageView;
			var n = await DisplayActionSheet("Elige una imagen", "cancelar", null, new string[] { "Cámara", "Galería" });
			switch (n)
			{
				case "Cámara":
					if (TakePicture != null)
					{
						TakePicture();
					}
					break;
				case "Galería":
					if (SelectFromGallery != null)
					{
						SelectFromGallery();
					}
					break;
			}
		}


		#endregion

	}
}

