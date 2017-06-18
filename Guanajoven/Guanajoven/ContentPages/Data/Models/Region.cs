using System;
using System.IO;
using Newtonsoft.Json;

namespace Guanajoven
{

	public class Region
	{
		public int? idRegion { get; set; }
		public string nombre { get; set; }
		public string direccion { get; set; }
		public string responsable { get; set; }
		public string descripcion { get; set; }
		public double latitud { get; set; }
		public double longitud { get; set; }
		public string createdAt { get; set; }
		public string updatedAt { get; set; }
		public string deletedAt { get; set; }
	}
}


/*
import io.realm.RealmList;
import io.realm.RealmObject;
import io.realm.annotations.PrimaryKey;
@PrimaryKey*/
