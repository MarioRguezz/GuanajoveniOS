using System;
using Xamarin.Forms;

namespace Guanajoven
{
	public class HomeDrawerPage : MasterDetailPage
	{

		public DrawerListPage _drawer;

		public HomeDrawerPage(DrawerPage page = DrawerPage.HomeView)
		{

			_drawer = new DrawerListPage(this);
			_drawer.PageSelected += (pageType) =>
			{
				bool presented = false;
				switch (pageType)
				{
					case DrawerPage.HomeView:
						Detail.Navigation.PushAsync(new HomeDrawerPage());
						break;
					case DrawerPage.ProfileView:
						Detail.Navigation.PushAsync(new ProfilePage());
						break;
					case DrawerPage.CodigoView:
						Detail.Navigation.PushAsync(new GuanajovenCodePage());
						break;
					case DrawerPage.Cerrar:
						//Cerrar Sesión 
						break;
					case DrawerPage.EventosView:
						Detail.Navigation.PushAsync(new EventsView());
						break;
					case DrawerPage.ConvocatoriasView:
						Detail.Navigation.PushAsync(new CallPage());
						break;
					case DrawerPage.CalendarioView:
						Detail.Navigation.PushAsync(new CalendarPage());
						break;
					case DrawerPage.NotificacionesView:
						Detail.Navigation.PushAsync(new NotificationPage());
						break;
					case DrawerPage.RedesSocialesView:
						Detail.Navigation.PushAsync(new SocialMediaPage());
						break;
					case DrawerPage.ChatView:
						Detail.Navigation.PushAsync(new ChatPage());
						break;
					case DrawerPage.RegionesView:
						Detail.Navigation.PushAsync(new RegionPage());
						break;
					case DrawerPage.AcercaView:
						Detail.Navigation.PushAsync(new AboutPage());
						break;

				}

				IsPresented = presented;
			};
			Master = _drawer;

			if (page == DrawerPage.HomeView)
			{
				Detail = new NavigationPage(new MasterHomePage(this));
			}
			else
			{
				switch (page)
				{
					case DrawerPage.ConvocatoriasView:
						//Detail = new NavigationPage(new MapInfoDelivery(this));
						break;
				}
			}

			MasterBehavior = MasterBehavior.Popover;

			IsPresentedChanged += (sender, e) =>
			{
				//_drawer.UpdateView();
			};

			NavigationPage.SetHasNavigationBar(this, false);
		}

	}
}