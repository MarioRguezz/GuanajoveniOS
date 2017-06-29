using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Guanajoven
{
	public partial class MasterHomePage : ContentPage
	{
		void Handle_Clicked(object sender, System.EventArgs e)
		{
			throw new NotImplementedException();
		}

		HomeDrawerPage RootPage;
		public MasterHomePage(HomeDrawerPage _rootPage)
		{
			InitializeComponent();
			RootPage = _rootPage;

			NavigationPage.SetHasNavigationBar(this, false);

			InitViews();

			UpdateView();
		}

		protected async override void OnAppearing()
		{
			base.OnAppearing();
		}



		void InitViews()
		{


		}

		void UpdateView()
		{
		}




		void MenuClicked(object sender, EventArgs args)
		{
			RootPage.IsPresented = true;
		}


		async void Advertising(object sender, EventArgs args)
		{

			await Navigation.PushAsync(new AdvertisingPage());
			//await Navigation.PushModalAsync(new AdvertisingPage());
		}

		async void GuanajovenClicked(object sender, EventArgs args)
		{
			var image = sender as Image;
			image.Opacity = 0.6;
			await Navigation.PushAsync(new GuanajovenCodePage(RootPage));
		}

		async void EventosClicked(object sender, EventArgs args)
		{
			var image = sender as Image;
			image.Opacity = 0.6;
			await Navigation.PushAsync(new EventsView(RootPage));
		}

		async void CheckClicked(object sender, EventArgs args)
		{
			var image = sender as Image;
			image.Opacity = 0.6;
			await Navigation.PushAsync(new CalendarPage(RootPage));
		}

		async void ConvocaClicked(object sender, EventArgs args)
		{
			var image = sender as Image;
			image.Opacity = 0.6;
			await Navigation.PushAsync(new CallPage(RootPage));
		}

		async void RedClicked(object sender, EventArgs args)
		{
			var image = sender as Image;
			image.Opacity = 0.6;
			await Navigation.PushAsync(new SocialMediaPage(RootPage));
		}

		async void ChatClicked(object sender, EventArgs args)
		{
			var image = sender as Image;
			image.Opacity = 0.6;
			await Navigation.PushAsync(new ChatPage(RootPage));
		}






		void SignUpClicked(object sender, EventArgs args)
		{

			RootPage.IsPresented = true;
			var x = "hola";
		}




	}

}