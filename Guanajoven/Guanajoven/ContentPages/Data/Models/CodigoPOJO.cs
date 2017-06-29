using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

namespace Guanajoven
{

	public class CodigoPOJO
	{
		public string data { get; set; }
		public bool success { get; set; }
		public int status { get; set; }
		public List<object> errors { get; set; }
	}

}


