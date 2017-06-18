using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace Guanajoven
{
	public partial class ForgotPassword : BasePage
	{
		public ForgotPassword()
		{
			InitializeComponent();
			NavigationPage.SetHasNavigationBar(this, false);
		}


		async void Start(object sender, System.EventArgs e)
		{
			//await Navigation.PushAsync(new ForgotPassword());
		}

		async void Recuperar(object sender, System.EventArgs e)
		{
			await Navigation.PushAsync(new ForgotPassword());
		}


		async void CloseClicked(object sender, System.EventArgs e)
		{
			var image = sender as Image;
			image.Opacity = 0.6;
			image.FadeTo(1);
			await Navigation.PopAsync();
		}

	}
}
