using System;
using System.Collections.Generic;

namespace Guanajoven
{
	public class AuthResponse
	{
		public string FB_ID { get; set; }
		public string Name { get; set; }
		public string LastName { get; set; }
		public Dictionary<string, string> Properties { get; set; }
		public string Email { get; set; }
	}
}
