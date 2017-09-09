using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace Guanajoven
{
	public partial class RegionPageDetail : BasePage
	{


		public static string street = null, streetNumber, colony = null, city = null, postalCode = null, state = null, country = null;

		Position START_POINT;
		Distance START_DISTANCE = Distance.FromKilometers(0.2);
		static string timeString = DateTime.Now.ToString("yyyy-MM-dd HH:MM:ss");
		DateTime now = DateTime.Parse(timeString);

		bool chooseAddress;

		public RegionPageDetail(Region region)
		{
			InitializeComponent();
			START_POINT = new Position(region.latitud, region.longitud);
			Map.MoveToRegion(MapSpan.FromCenterAndRadius(START_POINT, START_DISTANCE));
			Title = region.nombre;
			_nombre.Text = region.nombre;
			_direccion.Text = region.direccion;
			_responsable.Text = region.responsable;
			_descripcion.Text = region.descripcion;

		}

		protected override void OnAppearing()
		{
			base.OnAppearing();
		//	GetLocation();
		}




		async void GetLocation()
		{
			//Map.MoveToRegion(MapSpan.FromCenterAndRadius(START_POINT, START_DISTANCE));

		}









	}


}

