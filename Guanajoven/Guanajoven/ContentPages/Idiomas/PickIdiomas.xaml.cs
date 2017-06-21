using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace Guanajoven
{
	public partial class PickIdiomas : ContentPage
	{
		CheckBoxGroup _checks;

		public PickIdiomas()
		{
			InitializeComponent();

			SetIdiomas();

			NavigationPage.SetHasNavigationBar(this, false);
		}

		void SetIdiomas()
		{
			var list = new List<string>()
			{
				"Inglés",
				"Español",
				"Francés",
				"Italiano",
				"Aleman",
				"Chino",
				"Totonaca",
			};

			_checks = new CheckBoxGroup(list.ToArray());

			_checks.RemoveValue += (sender, value) =>
			{
				if (HelperIdioma.InfioIdiomas.ContainsKey(value))
					HelperIdioma.InfioIdiomas.Remove(value);
			};

			_scrollView.Content = _checks;

			foreach (var item in HelperIdioma.InfioIdiomas)
			{
				var index = list.IndexOf(item.Key);
				_checks.UpdateRating(index + 1);
			}
		}


		async void ContinuarClicked(object sender, System.EventArgs e)
		{
			var res = _checks.Values;

			if (res.Count > 0)
			{
				await Navigation.PushAsync(new CarouselPageIdiomas(res));
			}
			else {
				//
				await DisplayAlert("", "Elige al menos un idioma", "Ok");
			}

		}

	}
}
