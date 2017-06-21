using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace Guanajoven
{
	public partial class SetInfoIdioma : ContentPage
	{
		public Action<SetInfoIdioma> NextPage;
		public Action<SetInfoIdioma> PreviousPage;
		public Action<int> GoToPage;

		InfoIdioma InfoIdioma;

		public SetInfoIdioma(InfoIdioma idioma, bool last = false)
		{
			InitializeComponent();

			InfoIdioma = idioma;
			_nombre.Text = idioma.Nombre;

			if (idioma.Lectura > 0)
				_lectura.Text = idioma.Lectura + "";
			if (idioma.Escritura > 0)
				_escritura.Text = idioma.Escritura + "";
			if (idioma.Redaccion > 0)
				_redaccion.Text = idioma.Redaccion + "";



			if (last)
				_continuar.Text = "Finalizar";
		}


		void ContinuarClicked(object sender, System.EventArgs e)
		{
			if (!Valid())
				return;

			var infoIdioma = new InfoIdioma()
			{
				Nombre = InfoIdioma.Nombre,
				Lectura = int.Parse(_lectura.Text),
				Escritura = int.Parse(_escritura.Text),
				Redaccion = int.Parse(_redaccion.Text),
			};

			if (HelperIdioma.InfioIdiomas.ContainsKey(InfoIdioma.Nombre))
			{
				HelperIdioma.InfioIdiomas.Remove(InfoIdioma.Nombre);
			}

			HelperIdioma.InfioIdiomas.Add(InfoIdioma.Nombre, infoIdioma);

			if (NextPage != null)
			{
				NextPage(this);
			}
			//}



		}

		bool Valid()
		{
			if (string.IsNullOrEmpty(_lectura.Text))
			{
				DisplayAlert("", "Ingresa un porcentaje de lectura", "Ok");
				return false;
			}
			else {
				var x = int.Parse(_lectura.Text);
				if (x < 0 || x > 100)
				{
					DisplayAlert("", "Ingresa un porcentaje de lectura correcto", "Ok");
					return false;
				}
			}

			if (string.IsNullOrEmpty(_escritura.Text))
			{
				DisplayAlert("", "Ingresa un porcentaje de escritura", "Ok");
				return false;
			}
			else {
				var x = int.Parse(_escritura.Text);
				if (x < 0 || x > 100)
				{
					DisplayAlert("", "Ingresa un porcentaje de escritura correcto", "Ok");
					return false;
				}
			}

			if (string.IsNullOrEmpty(_redaccion.Text))
			{
				DisplayAlert("", "Ingresa un porcentaje de redacción", "Ok");
				return false;
			}
			else {
				var x = int.Parse(_redaccion.Text);
				if (x < 0 || x > 100)
				{
					DisplayAlert("", "Ingresa un porcentaje de redacción correcto", "Ok");
					return false;
				}
			}




			return true;
		}
	}
}
