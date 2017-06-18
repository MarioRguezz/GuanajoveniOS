using System;
using System.IO;
using Newtonsoft.Json;

namespace Guanajoven
{

	public class DatosUsuario
	{
		public int? idDatosUsuario { get; set; }
		public string nombre { get; set; }
		public string apellidoPaterno { get; set; }
		public string apellidoMaterno { get; set; }
		//private Genero genero;
		private string fechaNacimiento;
		//private Estado estadoNacimiento;
		//private Estado estado;
		public int codigoPostal;
		public string telefono;
		public string curp;
		//public Municipio municipio;
		public string rutaImagen;
		//public NivelEstudios nivelEstudios;
		//public PuebloIndigena puebloIndigena;
		//public CapacidadDiferente capacidadDiferente;
		public string premios;
		public string proyectosSociales;
		public Boolean apoyoProyectosSociales;

	}
}

