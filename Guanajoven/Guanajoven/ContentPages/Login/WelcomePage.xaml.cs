using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace Guanajoven
{
	public partial class WelcomePage : BasePage
	{
		public WelcomePage()
		{
			InitializeComponent();
		}

		async void Start(object sender, System.EventArgs e)
		{
			PropertiesManager.SaveFirstArrive();
			await Navigation.PushModalAsync(new RootPage());
		}
	}
}
