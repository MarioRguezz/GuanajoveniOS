using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

namespace Guanajoven
{

	public class DatosUsuario
	{
		public int? id_datos_usuario { get; set; }
		public int id_usuario { get; set; }
		public string nombre { get; set; }
		public string apellido_paterno { get; set; }
		public string apellido_materno { get; set; }
		public int id_genero { get; set; }
		public string fecha_nacimiento { get; set; }
		public int id_estado_nacimiento { get; set; }
		public int codigo_postal { get; set; }
		public object telefono { get; set; }
		public string curp { get; set; }
		public int id_estado { get; set; }
		public int id_municipio { get; set; }
		public string ruta_imagen { get; set; }
		public string created_at { get; set; }
		public string updated_at { get; set; }
		public object deleted_at { get; set; }
		public int? id_nivel_estudios { get; set; }
		public int? id_pueblo_indigena { get; set; }
		public int? id_capacidad_diferente { get; set; }
		public string premios { get; set; }
		public string proyectos_sociales { get; set; }
		public int? apoyo_proyectos_sociales { get; set; }
		public int? id_programa_beneficiario { get; set; }
		public int? trabaja { get; set; }
		public Municipio municipio { get; set; }
		public Estado estado { get; set; }
		public EstadoNacimiento estado_nacimiento { get; set; }
		public Genero genero { get; set; }
		public object pueblo_indigena { get; set; }
		public object programa_gobierno { get; set; }
		public object nivel_estudios { get; set; }
		public object capacidad_diferente { get; set; }
		public List<IdiomaAdicional> idiomas { get; set; }
		public string api_token { get; set; }
	}



	public class DataUserResponse
	{
		public bool success { get; set; }
		public List<object> errors { get; set; }
		public int status { get; set; }
		public DatosUsuario data { get; set; }
	}



}


