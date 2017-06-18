using System;
using System.IO;
using Newtonsoft.Json;

namespace Guanajoven
{

	public class Publicidad
	{
		public int? idPublicidad { get; set; }
		public string titulo { get; set; }
		public string descripcion { get; set; }
		public string rutaImagen { get; set; }
		public string url;
		public string fechaInicio;
		public string fechaFin;
		public string createdAt;
		public string updatedAt;
		public string deletedAt;
	}
}


/*
import io.realm.RealmObject;
import io.realm.annotations.PrimaryKey; @PrimaryKey*/
