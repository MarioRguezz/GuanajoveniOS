using System;
using System.IO;
using Newtonsoft.Json;

namespace Guanajoven
{

	public class UsuarioPOJO
	{
		public string email { get; set; }
		public string password { get; set; }
		public string confirmar_password { get; set; }
		public string curp { get; set; }
		public string apellido_paterno { get; set; }
		public string apellido_materno { get; set; }
		public string nombre { get; set; }
		public string genero { get; set; }
		public string fecha_nacimiento { get; set; }
		public string codigo_postal { get; set; }
		public string estado_nacimiento { get; set; }
		public string ruta_imagen { get; set; }
		public string id_google { get; set; }
		public string id_facebook { get; set; }
	}
}
