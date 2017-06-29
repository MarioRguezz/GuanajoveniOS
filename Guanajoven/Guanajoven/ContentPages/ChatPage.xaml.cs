using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace Guanajoven
{
	public partial class ChatPage : ContentPage
	{
		HomeDrawerPage RootPage;
		public ChatPage(HomeDrawerPage _rootPage)
		{
			InitializeComponent();
			RootPage = _rootPage;
		}
	}
}
