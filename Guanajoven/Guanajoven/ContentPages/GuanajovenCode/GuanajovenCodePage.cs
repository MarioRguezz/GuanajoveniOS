using System;

using Xamarin.Forms;

namespace Guanajoven
{
	public class GuanajovenCodePage : ContentPage
	{
		public GuanajovenCodePage()
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

