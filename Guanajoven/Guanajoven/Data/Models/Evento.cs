using System;
using System.Collections.Generic;
using System.IO;
using Guanajoven;
using Newtonsoft.Json;
using Realms;

namespace Guanajoven
{

	public class Evento : RealmObject
	{
		public int? id_evento { get; set; }
		public string titulo { get; set; }
		public string descripcion { get; set; }
		public string fecha_inicio { get; set; }
		public string fecha_fin { get; set; }
		public string evento_estado { get; set; }
		public int id_tipo_evento;
		public string direccion { get; set; }
		public double latitud { get; set; }
		public double longitud { get; set; }
		public string created_at { get; set; }
		public string updated_at { get; set; }
		public string deleted_at { get; set; }
		public string fecha { get; set; }
		public bool IsEventoActivo { get; set; }

	}


	public class ResponseEvent
	{
		public int status { get; set; }
		public bool success { get; set; }
		public List<object> errors { get; set; }
		public List<Evento> data { get; set; }
	}
}





/*
import io.realm.RealmObject;
import io.realm.annotations.PrimaryKey;
public class Evento extends RealmObject
{
	@PrimaryKey*/
