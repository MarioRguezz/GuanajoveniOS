using System;
using Xamarin.Forms;
using UIKit;
using Guanajoven.iOS;
using BigTed;

[assembly: Dependency(typeof(ProgressView))]
namespace Guanajoven.iOS
{
	public class ProgressView : IProgress
	{
		bool show = false;

		public void ShowProgress(string text)
		{
			if (!show)
			{
				show = true;
				BTProgressHUD.ForceiOS6LookAndFeel = true;
				BTProgressHUD.Show(text, -1, ProgressHUD.MaskType.Black);
			}
		}

		public void Dismiss()
		{
			show = false;
			BTProgressHUD.Dismiss();
		}

		public void ShowProgress(IProgressType type)
		{
			if (type == IProgressType.Done)
			{
				BTProgressHUD.ShowSuccessWithStatus("Cargando", 1000);
			}
		}
	}
}