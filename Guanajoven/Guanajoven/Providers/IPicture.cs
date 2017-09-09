using System;

using Xamarin.Forms;

namespace Guanajoven
{
	public interface IPicture
	{
		void SavePictureToDisk(string filename, byte[] imageData);
	}
}





