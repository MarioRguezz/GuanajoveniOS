using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

namespace Guanajoven
{

	public class Mensaje
	{
		public string data { get; set; }
		public bool success { get; set; }
		public int status { get; set; }
		public string api_token { get; set; }
		public string mensaje { get; set; }
		public string page { get; set; }
		public List<object> errors { get; set; }
	}

}





