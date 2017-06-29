using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace Guanajoven
{
	public partial class CallPage : BasePage
	{
		HomeDrawerPage RootPage;
		public CallPage(HomeDrawerPage _rootPage)
		{
			InitializeComponent();
			RootPage = _rootPage;
			NavigationPage.SetHasNavigationBar(this, false);
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
