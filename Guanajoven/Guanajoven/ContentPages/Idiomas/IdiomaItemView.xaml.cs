using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace Guanajoven
{
	public partial class IdiomaItemView : ContentView
	{
		public IdiomaItemView(InfoIdioma idioma)
		{
			InitializeComponent();


			_labelTitulo.Text = idioma.Nombre;

			var lect = string.Format("Lectura {0}%", idioma.Lectura);
			var escr = string.Format("Escritura {0}%", idioma.Escritura);
			var red = string.Format("Redacción {0}%", idioma.Redaccion);

			_info.Text = string.Format("{0}, {1}, {2}",lect,escr,red); //?
		}
	}
}
