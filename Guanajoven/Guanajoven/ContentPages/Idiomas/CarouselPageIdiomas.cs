using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace Guanajoven
{
	public class CarouselPageIdiomas : Xamarin.Forms.CarouselPage
	{

		public static List<string> Idiomas;

		public CarouselPageIdiomas(List<string> idiomas)
		{
			Idiomas = idiomas;

			var n = 0;
			foreach (var idioma in Idiomas)
			{

				InfoIdioma indoIdioma;
				if (HelperIdioma.InfioIdiomas.ContainsKey(idioma))
				{
					indoIdioma = HelperIdioma.InfioIdiomas[idioma];
				}
				else {
					indoIdioma = new InfoIdioma()
					{
						Nombre = idioma,
					};
				}
				

				var p = new SetInfoIdioma(indoIdioma, n == (idiomas.Count - 1));
				p.NextPage = NextPage;
				Children.Add(p);

				n++;
			}

			NavigationPage.SetHasNavigationBar(this, false);

		}

		async void NextPage(SetInfoIdioma obj)
		{
			var n = Children.IndexOf(obj);
			if (Children.Count > (n + 1))
				CurrentPage = Children[n + 1];
			else {

				if (HelperIdioma.InfioIdiomas.Count == Idiomas.Count)
				{
					System.Diagnostics.Debug.WriteLine("INFO IDIOMAS:");
					foreach (var item in HelperIdioma.InfioIdiomas)
					{
						var infoIdioma = item.Value;
						System.Diagnostics.Debug.WriteLine("{0},  Lectura:{1} Escritura:{2} Redaccion:{3}", infoIdioma.Nombre, infoIdioma.Lectura, infoIdioma.Escritura, infoIdioma.Redaccion);
					}

					MessagingCenter.Send<CarouselPageIdiomas>(this, "finished_picking");


					await Navigation.PopModalAsync();
				}
				else
				{
					var index = 0;
					var str = "";

					foreach (var i in Idiomas)
					{
						if (!HelperIdioma.InfioIdiomas.ContainsKey(i))
						{
							str += i+" ";
							index = Idiomas.IndexOf(i);
						}
					}

					await DisplayAlert("","Ingresa la información de "+str,"Ok");

					CurrentPage = Children[index];
				}
			}

		}
	}
}