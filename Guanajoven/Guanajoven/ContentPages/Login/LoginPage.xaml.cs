using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace Guanajoven
{
	public partial class LoginPage : BasePage
	{
		public LoginPage()
		{
			InitializeComponent();
			NavigationPage.SetHasNavigationBar(this, false);
			Background.BackgroundColor = Color.FromHex("#b7C7E1F5");

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
			};

			GoogleLoginResponse += (google_user) =>
			{
				System.Diagnostics.Debug.WriteLine("GOOGLE, INFO:");
				System.Diagnostics.Debug.WriteLine("{0} : {1} {2} , {3}, {4}", google_user.id,
				                                   google_user.given_name,
				                                   google_user.family_name,
				                                   google_user.email,
				                                   google_user.picture);
			};
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



		async void forgotpassword(object sender, System.EventArgs e)
		{
			await Navigation.PushAsync(new ForgotPassword());
		}

		async void LoginClicked(object sender, System.EventArgs e)
		{
			//await Navigation.PushAsync(new HomePage());
			await Navigation.PushModalAsync(new HomeDrawerPage());
		}

	}
}
