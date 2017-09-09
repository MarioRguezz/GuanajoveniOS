using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

namespace Guanajoven
{

	public class Usuario
	{
		public int? id { get; set; }
		public string email { get; set; }
		public int admin { get; set; }
		public string api_token { get; set; }
		public string created_at { get; set; }
		public string updated_at { get; set; }
		public object deleted_at { get; set; }
		public DatosUsuario datos_usuario { get; set; }
		public CodigoGuanajoven codigo_guanajoven { get; set; }
		public int puntaje { get; set; }
		public string posicion { get; set; }
	}


	public class ResponseUsuario
	{
		public bool success { get; set; }
		public List<object> errors { get; set; }
		public int status { get; set; }
		public Usuario data { get; set; }
	}
}
