using System;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Guanajoven
{
	public interface IScreenshotManager
	{
		Task<byte[]> CaptureAsync();
	}
}

