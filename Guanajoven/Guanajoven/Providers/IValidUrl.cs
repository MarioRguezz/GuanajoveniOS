using System;
using System.Globalization;
using Xamarin.Forms;

namespace Guanajoven
{
	public interface IValidUrl
	{
		bool Valid(string url);
	}
}