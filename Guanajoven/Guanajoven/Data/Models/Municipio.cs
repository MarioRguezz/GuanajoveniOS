using System;
using System.IO;
using Newtonsoft.Json;

namespace Guanajoven
{

	public class Municipio
	{
		public int? id_municipio { get; set; }
		public string nombre { get; set; }
		public int id_region { get; set; }
		public string created_at { get; set; }
		public string updated_at { get; set; }
		public object deleted_at { get; set; }
	}
}
