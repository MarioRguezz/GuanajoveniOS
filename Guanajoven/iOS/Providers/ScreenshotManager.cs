using System;
using Xamarin.Forms;
using UIKit;
using Guanajoven.iOS;
using BigTed;
using System.IO;

[assembly: Dependency(typeof(ScreenshotManager))]
namespace Guanajoven.iOS
{
	public class ScreenshotManager : IScreenshotManager
	{

		public async System.Threading.Tasks.Task<byte[]> CaptureAsync()
		{
			var view = UIApplication.SharedApplication.KeyWindow.RootViewController.View;

			UIGraphics.BeginImageContext(view.Frame.Size);
			view.DrawViewHierarchy(view.Frame, true);
			var image = UIGraphics.GetImageFromCurrentImageContext();
			UIGraphics.EndImageContext();

			using (var imageData = image.AsPNG())
			{
				var bytes = new byte[imageData.Length];
				System.Runtime.InteropServices.Marshal.Copy(imageData.Bytes, bytes, 0, Convert.ToInt32(imageData.Length));
				return bytes;
			}

		}

	}
}