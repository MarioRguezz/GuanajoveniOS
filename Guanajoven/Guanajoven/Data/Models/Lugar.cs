using System;
using System.IO;
using Newtonsoft.Json;

namespace Guanajoven
{

	public class Lugar
	{
		public int? id_lugar { get; set; }
		public string nombre { get; set; }
		public string tipo_instalacion { get; set; }
		public string direccion { get; set; }
		public string colonia { get; set; }
		public string municipio { get; set; }
		public int cp { get; set; }
		public string administrador { get; set; }
		public string telefono { get; set; }
		public string email { get; set; }
		public float latitud { get; set; }
		public float longitud { get; set; }
	}
}




