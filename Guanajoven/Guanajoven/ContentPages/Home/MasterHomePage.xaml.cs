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


		void SignUpClicked(object sender, EventArgs args)
		{

			RootPage.IsPresented = true;
			var x = "hola";
		}




	}

}