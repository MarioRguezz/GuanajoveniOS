using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Plugin.Connectivity;
using Xamarin.Forms;

namespace Guanajoven
{
	public partial class PromotionPage : BasePage
	{
		HomeDrawerPage RootPage;
		public static bool Changed = false;
		public static List<Promotion> _promociones = null;
		ObservableCollection<Promotion> _itemsList = new ObservableCollection<Promotion>();
		static string timeString = DateTime.Now.ToString("yyyy-MM-dd HH:MM:ss");
		static DateTime time = DateTime.Parse(timeString);
		static DateTime dateToSend = time;
		string empresa_imagen = null;
		public PromotionPage(Empresa empresa)
		{
			InitializeComponent();
			//NavigationPage.SetHasNavigationBar(this, false);

			_image.Source = empresa.logo;
			empresa_imagen = empresa.logo;
			_itemsList.Clear();
			_promociones = empresa.promociones.OrderBy(x => x.fecha_inicio).ToList();
			foreach (var item in _promociones)
			{
				_itemsList.Add(item);
			}


			Device.BeginInvokeOnMainThread(() =>
					{
						if (ListView.ItemsSource != _itemsList)
							ListView.ItemsSource = _itemsList;
					});


			ListView.ItemsSource = _itemsList;
		}


		async void TapItem(object sender, System.EventArgs e)
		{
			var stack = (StackLayout)sender;
			await stack.ScaleTo(0.9, 100, Easing.SinIn);
			await stack.ScaleTo(1, 100, Easing.SinIn);
			if (stack.BindingContext is Promotion)
			{
				var promotion = stack.BindingContext as Promotion;

				await Navigation.PushAsync(new DetailPromotionPage(promotion, empresa_imagen));
			}
		}

		async void CloseClicked(object sender, System.EventArgs e)
		{
			var image = sender as Image;
			image.Opacity = 0.6;
			image.FadeTo(1);
			RootPage.IsPresented = true;
			//await Navigation.PopAsync();
		}





	}
}
