using System;
using System.Collections.Generic;
using System.IO;
using Xamarin.Forms;
using ZXing;

namespace Guanajoven
{

	public interface IBarcodeService
	{
		Stream ConvertImageStream(string text, int width = 500, int height = 500);
	}

	public partial class GuanajovenCodePage : ContentPage
	{
		public GuanajovenCodePage()
		{
			InitializeComponent();

			var stream = DependencyService.Get<IBarcodeService>().ConvertImageStream("http://www.google.com.mx");
			_qrImage.Source = ImageSource.FromStream(() => { return stream; });

		}

	}
}
