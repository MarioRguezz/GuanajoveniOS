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

			var lect = string.Format("Lectura {0}%", idioma.lectura);
			var escr = string.Format("Escritura {0}%", idioma.escritura);
			var con = string.Format("Conversación {0}%", idioma.conversacion);

			_info.Text = string.Format("{0}\n{1}\n{2}",lect,escr,con);
			_info.TextColor = Color.FromHex("#003464");
		}
	}
}
