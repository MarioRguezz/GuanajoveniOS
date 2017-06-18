﻿using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace Guanajoven
{
	public partial class AdvertisingPage : ContentPage
	{
		public AdvertisingPage()
		{
			InitializeComponent();
			NavigationPage.SetHasNavigationBar(this, false);
		}

		async void CloseClicked(object sender, EventArgs args)
		{
			await Navigation.PopAsync();
		}

	}


}
