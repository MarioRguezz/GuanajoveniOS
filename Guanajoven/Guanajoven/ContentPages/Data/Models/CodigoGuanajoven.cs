using System;
using System.IO;
using Newtonsoft.Json;

namespace Guanajoven
{

	public class CodigoGuanajoven
	{
		public int? idCodigoGuanajoven { get; set; }
		public string token { get; set; }
		public string fechaExpiracion { get; set; }
		public string fechaLimite { get; set; }
	}
}
