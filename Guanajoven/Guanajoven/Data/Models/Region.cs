using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using Realms;

namespace Guanajoven
{
	public class Region: RealmObject
	{
		public int? id_region { get; set; }
		public string nombre { get; set; }
		public string direccion { get; set; }
		public string responsable { get; set; }
		public string descripcion { get; set; }
		public double latitud { get; set; }
		public double longitud { get; set; }
		public string created_at { get; set; }
		public string updated_at { get; set; }
		public string deleted_at { get; set; }
	}

	public class RegionResponse
	{
		public int status { get; set; }
		public bool success { get; set; }
		public List<object> errors { get; set; }
		public List<Region> data { get; set; }
	}
}


/*
import io.realm.RealmList;
import io.realm.RealmObject;
import io.realm.annotations.PrimaryKey;
@PrimaryKey*/
