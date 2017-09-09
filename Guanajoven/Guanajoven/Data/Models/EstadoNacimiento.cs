using System;
using System.IO;
using Newtonsoft.Json;

namespace Guanajoven
{

	public class EstadoNacimiento
	{
		public int? id_estado { get; set; }
		public string nombre { get; set; }
		public string abreviatura { get; set; }
		public object created_at { get; set; }
		public object updated_at { get; set; }
		public object deleted_at { get; set; }
	}

}




