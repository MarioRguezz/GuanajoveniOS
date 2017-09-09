using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace Guanajoven
{
	public partial class AboutPage : BasePage
	{
		HomeDrawerPage RootPage;
		public AboutPage(HomeDrawerPage _rootPage)
		{
			InitializeComponent();
			RootPage = _rootPage;
			NavigationPage.SetHasNavigationBar(this, false);
			_queHacemos.Text = "En GUANAJOVEN fomentamos la creación y el desarrollo del proyecto de vida de los jóvenes guanajuatenses." +
				" Trabajamos para que los apoyos del Gobierno del Estado dirigidos al sector joven de la población lleguen de manera correcta a cada uno de ellos.";
			_queHacemos.HorizontalTextAlignment = TextAlignment.Start;
		    _logros.Text = "*GUANAJUATO el estado de la República Mexicana más beneficiado con recurso económico por el IMJUVE" +
				" (Federación).\n*En 2016 el Lic. Miguel Márquez Márquez fue reconocido como 'El Gobernador de los Jóvenes' por" +
				" la YABT.\n*Reconocimiento a nivel nacional por el IMJUVE como \"Mejor Operatividad en Centros Poder" +
				" Joven\".\n*Logra GUANAJOVEN el GUINNESS WORLD RECORDS por 'Concentración de mayor número de catrines y catrinas en 2016'.";
			_logros.HorizontalTextAlignment = TextAlignment.Start;
		}

		async void CloseClicked(object sender, System.EventArgs e)
		{
			var image = sender as Image;
			image.Opacity = 0.6;
			image.FadeTo(1);
			RootPage.IsPresented = true;
			//await Navigation.PopAsync()		}
		}

		async void lineas(object sender, EventArgs args)

		{
			Device.OpenUri(new Uri("http://jovenes.guanajuato.gob.mx/index.php/702-2/"));
		}

		async void encuestas(object sender, EventArgs args)

		{
			Device.OpenUri(new Uri("http://jovenes.guanajuato.gob.mx/index.php/encuesta-de-juventud/"));
		}


		async void diagnosticos(object sender, EventArgs args)

		{
			Device.OpenUri(new Uri("http://jovenes.guanajuato.gob.mx/index.php/diagnostico-juvenil/"));
		}


		async void directorios(object sender, EventArgs args)

		{
			Device.OpenUri(new Uri("http://transparencia.guanajuato.gob.mx/transparencia/informacion_publica_directorio.php"));
		}
	}
}
