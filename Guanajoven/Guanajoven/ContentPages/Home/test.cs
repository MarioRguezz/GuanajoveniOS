using System;
using Xamarin.Forms;

namespace Guanajoven
{
	public class test : MasterDetailPage
	{

		public DrawerListPage _drawer;

		public test(DrawerPage page = DrawerPage.Mapa)
		{

			_drawer = new DrawerListPage(this);
			_drawer.PageSelected += (pageType) =>
			{
				bool presented = false;
				switch (pageType)
				{
					case DrawerPage.Historial:
						//Detail.Navigation.PushAsync(new DeliveriesPage());
						//Detail.Navigation.PushAsync(new HistorialPage());
						break;
					case DrawerPage.Instrucciones:
						//Detail.Navigation.PushAsync(new InstruccionesPage());
						break;
					case DrawerPage.Privacidad:
						//Detail.Navigation.PushAsync(new PrivacidadPage());
						break;
					case DrawerPage.Perfil:
						//Detail.Navigation.PushAsync(new ProfilePage());
						//Detail.Navigation.PushAsync(new PerfilPage());

						break;
				}

				IsPresented = presented;
			};
			Master = _drawer;

			if (page == DrawerPage.Mapa)
			{
				//Detail = new NavigationPage(new MapPage(this));
				Detail = new NavigationPage(new MasterHomePage(this));
			}
			else
			{
				switch (page)
				{
					case DrawerPage.MapaViajeEnCurso:
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