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
			_labelTitulo.TextColor = Color.FromHex("#636363");

			var lect = string.Format("Lectura {0}%", idioma.Lectura);
			var escr = string.Format("Escritura {0}%", idioma.Escritura);
			var red = string.Format("Redacción {0}%", idioma.Redaccion);

			_info.Text = string.Format("{0} \n {1} \n {2}",lect,escr,red);
			_info.TextColor = Color.FromHex("#003464");
		}
	}
}
