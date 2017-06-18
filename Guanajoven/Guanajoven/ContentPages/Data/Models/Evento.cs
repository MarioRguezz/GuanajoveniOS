using System;
using System.IO;
using Newtonsoft.Json;

namespace Guanajoven
{

	public class Evento
	{
		public int? idEvento { get; set; }
		public string titulo { get; set; }
		public string descripcion { get; set; }
		public string fechaInicio { get; set; }
		public string fechaFin { get; set; }
		public int idTipoEvento;
		public string direccion { get; set; }
		public Double latitud { get; set; }
		public Double longitud { get; set; }
		public string createdAt { get; set; }
		public string updatedAt { get; set; }
		public string deletedAt { get; set; }
	}
}


/*
import io.realm.RealmObject;
import io.realm.annotations.PrimaryKey;
public class Evento extends RealmObject
{
	@PrimaryKey*/
