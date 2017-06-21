using System;
using System.Collections.Generic;

namespace Guanajoven
{
	public class HelperIdioma
	{
		public static Dictionary<string,InfoIdioma> InfioIdiomas = new Dictionary<string, InfoIdioma>();
	}

	public class InfoIdioma
	{
		public string Nombre { get; set; }
		public bool Lectura { get; set; }
		public bool Escritura { get; set; }
		public bool Redaccion { get; set; }
	}
}
