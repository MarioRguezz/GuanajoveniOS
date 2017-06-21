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
		public int Lectura { get; set; }
		public int Escritura { get; set; }
		public int Redaccion { get; set; }
	}
}
