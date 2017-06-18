using System;
using Guanajoven;
using Guanajoven.iOS;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(BorderlessEntry), typeof(BorderlessEntryImpl))]
namespace Guanajoven.iOS
{
	public class BorderlessEntryImpl : EntryRenderer
	{

		protected override void OnElementPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
		{
			base.OnElementPropertyChanged(sender, e);

			if (Control != null)
			{
				Control.BorderStyle = UIKit.UITextBorderStyle.None;
				 //Control.Font = UIFont.FromName("Courier New", 13.50f);
			}
		}

	}
}

