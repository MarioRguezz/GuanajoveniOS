using System;
using System.IO;
using Newtonsoft.Json;

namespace Guanajoven
{

	public class CodigoGuanajoven
	{
		public int? id_codigo_guanajoven { get; set; }
		public int id_usuario { get; set; }
		public string token { get; set; }
		public string fecha_expiracion { get; set; }
		public string fecha_limite { get; set; }
		public string created_at { get; set; }
		public string updated_at { get; set; }
		public object deleted_at { get; set; }
	}

}

