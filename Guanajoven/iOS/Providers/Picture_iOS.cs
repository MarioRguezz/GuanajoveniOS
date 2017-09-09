using System;
using Xamarin.Forms;
using UIKit;
using Guanajoven.iOS;
using BigTed;
using Foundation;

[assembly: Xamarin.Forms.Dependency(typeof(Picture_iOS))]
namespace Guanajoven.iOS
{
	public class Picture_iOS : IPicture
	{
		public void SavePictureToDisk(string filename, byte[] imageData)
		{
			var chartImage = new UIImage(NSData.FromArray(imageData));
			chartImage.SaveToPhotosAlbum((image, error) =>
			{
				//you can retrieve the saved UI Image as well if needed using
				//var i = image as UIImage;
				if (error != null)
				{
					Console.WriteLine(error.ToString());
				}
			});
		}
	}
}