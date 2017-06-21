using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Guanajoven
{
	public partial class SocialMediaPage : ContentPage
	{
		HomeDrawerPage RootPage;
		public SocialMediaPage(HomeDrawerPage _rootPage)
		{
			InitializeComponent();
			RootPage = _rootPage;
			showPage();
			NavigationPage.SetHasNavigationBar(this, false);
		}



		async void CloseClicked(object sender, System.EventArgs e)
		{
			var image = sender as Image;
			image.Opacity = 0.6;
			image.FadeTo(1);
			//await Navigation.PopAsync();
			//MessagingCenter.Send<ProfilePage>(this, "show_home");
			RootPage.IsPresented = true;
				}

		public async void showPage()
		{
			var request = HttpWebRequest.Create("https://mobile.twitter.com/guanajoven");
			var response = await Task.Factory.FromAsync<WebResponse>(request.BeginGetResponse, request.EndGetResponse, null);

			string html = string.Empty;
			using (var s = new StreamReader(response.GetResponseStream()))
				html = await s.ReadToEndAsync();


			var htmlSource = new HtmlWebViewSource();
			htmlSource.Html = html;
			_webView.Source = htmlSource;
		}
	}
}
