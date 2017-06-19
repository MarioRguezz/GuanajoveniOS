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
			_scrollView.Content = _checks;
		}


		async void ContinuarClicked(object sender, System.EventArgs e)
		{
			var res = _checks.Values;

			if (res.Count > 0)
			{
				await Navigation.PushAsync(new CarouselPageIdiomas(res));
			}

		}

	}
}
