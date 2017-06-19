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

		CheckBoxGroup _checks;

		string Idioma;

		public SetInfoIdioma(string idioma, bool last = false)
		{
			InitializeComponent();
			Idioma = idioma;
			_nombre.Text = idioma;
			SetIdiomas();

			if (last)
				_continuar.Text = "Finalizar";
		}

		void SetIdiomas()
		{
			var list = new List<string>()
			{
				"Lectura",
				"Escritura",
				"Redacción",
			};

			_checks = new CheckBoxGroup(list.ToArray());
			_scrollView.Content = _checks;
		}

		void ContinuarClicked(object sender, System.EventArgs e)
		{
			var res = _checks.Values;

			//if (res.Count > 0)
			//{
			var infoIdioma = new InfoIdioma()
			{
				Nombre = Idioma,
				Lectura = res.Contains("Lectura"),
				Escritura = res.Contains("Escritura"),
				Redaccion = res.Contains("Redacción"),
			};

			if (HelperIdioma.InfioIdiomas.ContainsKey(Idioma))
			{
				HelperIdioma.InfioIdiomas.Remove(Idioma);
			}

			HelperIdioma.InfioIdiomas.Add(Idioma, infoIdioma);

			if (NextPage != null)
			{
				NextPage(this);
			}
			//}



		}
	}
}
