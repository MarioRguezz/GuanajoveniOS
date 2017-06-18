using System;
using System.IO;
using Newtonsoft.Json;

namespace Guanajoven
{

	public class Usuario
	{
		public int? id { get; set; }
		public string email { get; set; }
		public int admin { get; set; }
		public string apiToken { get; set; }
		public DatosUsuario datosUsuario { get; set; }
		public CodigoGuanajoven codigoGuanajoven { get; set; } 
		public string idGoogle { get; set; }
		public string idFacebook { get; set; }
	}
}
