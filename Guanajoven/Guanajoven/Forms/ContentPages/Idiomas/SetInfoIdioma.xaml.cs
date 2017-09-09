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
			//Background.BackgroundColor = Color.FromHex("#b7C7E1F5");
			InfoIdioma = idioma;
			_nombre.Text = idioma.Nombre;

			if (idioma.lectura > 0)
				_lectura.Text = idioma.lectura + "";
			if (idioma.escritura > 0)
				_escritura.Text = idioma.escritura + "";
			if (idioma.conversacion > 0)
				_conversacion.Text = idioma.conversacion + "";



			if (last)
				_continuar.Text = "Finalizar";
		}


		int getIdioma(String name)
		{
			switch (name)
			{
				case "Alemán":
					return 1;
				case "Árabe":
					return 2;
				case "Chino":
					return 3;
				case "Coreano":
					return 4;
				case "Francés":
					return 5;
				case "Inglés":
					return 6;
				case "Italiano":
					return 7;
				case "Japonés":
					return 8;
				case "Polaco":
					return 9;
				case "Portugués":
					return 10;
				case "Ruso":
					return 11;
				case "Otro":
					return 12;
				default:
					return 12;
			}

		}

		void ContinuarClicked(object sender, System.EventArgs e)
		{
			if (!Valid())
				return;

			var localuser = PropertiesManager.GetUserInfo();

			var infoIdioma = new InfoIdioma()
			{
				Nombre = InfoIdioma.Nombre,
				id_datos_usuario = localuser.data.datos_usuario.id_datos_usuario,
				id_idioma_adicional = getIdioma(InfoIdioma.Nombre),
				lectura = int.Parse(_lectura.Text),
				escritura = int.Parse(_escritura.Text),
				conversacion = int.Parse(_conversacion.Text),
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
				DisplayAlert("Guanajoven", "Ingresa un porcentaje de lectura", "Aceptar");
				return false;
			}
			else
			{
				var x = int.Parse(_lectura.Text);
				if (x < 0 || x > 100)
				{
					DisplayAlert("Guanajoven", "Ingresa un porcentaje de lectura correcto", "Aceptar");
					return false;
				}
			}

			if (string.IsNullOrEmpty(_escritura.Text))
			{
				DisplayAlert("Guanajoven", "Ingresa un porcentaje de escritura", "Aceptar");
				return false;
			}
			else
			{
				var x = int.Parse(_escritura.Text);
				if (x < 0 || x > 100)
				{
					DisplayAlert("Guanajoven", "Ingresa un porcentaje de escritura correcto", "Aceptar");
					return false;
				}
			}

			if (string.IsNullOrEmpty(_conversacion.Text))
			{
				DisplayAlert("Guanajoven", "Ingresa un porcentaje de redacción", "Aceptar");
				return false;
			}
			else
			{
				var x = int.Parse(_conversacion.Text);
				if (x < 0 || x > 100)
				{
					DisplayAlert("Guanajoven", "Ingresa un porcentaje de redacción correcto", "Aceptar");
					return false;
				}
			}




			return true;
		}
	}
}
