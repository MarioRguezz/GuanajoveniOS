using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using Realms;

namespace Guanajoven
{



	public class Publicidad: RealmObject
	{
		public int id_publicidad { get; set; }
		public string titulo { get; set; }
		public string descripcion { get; set; }
		public string ruta_imagen { get; set; }
		public string url { get; set; }
		public string fecha_inicio { get; set; }
		public string fecha_fin { get; set; }
		public string created_at { get; set; }
		public string updated_at { get; set; }
		public string deleted_at { get; set; }
	}

	public class PublicidadResponse
	{
		public bool success { get; set; }
		public int status { get; set; }
		public List<object> errors { get; set; }
		public List<Publicidad> data { get; set; }
	}
}
