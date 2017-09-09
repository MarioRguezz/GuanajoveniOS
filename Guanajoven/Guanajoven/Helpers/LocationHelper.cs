using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

using Newtonsoft.Json;
using Plugin.Geolocator;
using Plugin.Geolocator.Abstractions;
using Xamarin.Forms;

namespace Guanajoven
{
	public class LocationHelper
	{

		public Position CurrentPosition = null;
		public IGeolocator Geolocator;

		static LocationHelper _instance;
		public static LocationHelper Instance
		{
			get
			{
				if (_instance == null)
				{
					_instance = new LocationHelper();
				}

				return _instance;
			}
		}

		public LocationHelper()
		{
			SetupGeoLocator();
		}

		async void SetupGeoLocator()
		{
			if (Geolocator != null)
				return;
			Geolocator = CrossGeolocator.Current;
			Geolocator.DesiredAccuracy = 100;

			Geolocator.PositionChanged += (sender, e) =>
			{
				CurrentPosition = e.Position;
				System.Diagnostics.Debug.WriteLine("PositionChanged {0} {1}", e.Position.Latitude, e.Position.Longitude);
			};

			//Geolocator.
			await Geolocator.StartListeningAsync(TimeSpan.FromSeconds(10),1);
		}


	}
}