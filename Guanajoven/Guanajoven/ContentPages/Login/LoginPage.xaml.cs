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
		}

		async void CloseClicked(object sender, System.EventArgs e)
		{
			var image = sender as Image;
			image.Opacity = 0.6;
			image.FadeTo(1);
			await Navigation.PopAsync();
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
