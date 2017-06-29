using System;
using System.IO;
using Newtonsoft.Json;

namespace Guanajoven
{



	public class Genero
	{
		public int? id_genero { get; set; }
		public string nombre { get; set; }
		public string abreviatura { get; set; }
		public string created_at { get; set; }
		public string updated_at { get; set; }
		public object deleted_at { get; set; }
	}

}

