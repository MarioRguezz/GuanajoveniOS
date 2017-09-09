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
		public int? id_idioma_adicional { get; set;}
		public int? id_datos_usuario  {get; set;}
		public int lectura { get; set; }
		public int escritura { get; set; }
		public int conversacion { get; set; }
	}
}
