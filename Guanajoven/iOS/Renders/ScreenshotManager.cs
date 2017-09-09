using System;

using Xamarin.Forms;

namespace Guanajoven.iOS
{
	public class ScreenshotManager : ContentPage
	{
		public ScreenshotManager()
		{
			Content = new StackLayout
			{
				Children = {
					new Label { Text = "Hello ContentPage" }
				}
			};
		}
	}
}

