using System;
using System.IO;
using Newtonsoft.Json;
using Realms;

namespace Guanajoven
{




	public class Documento: RealmObject 
	{
		public int? id_documento { get; set; }
		public int id_convocatoria { get; set; }
		public string titulo { get; set; }
		public string ruta_documento { get; set; }
		public int id_formato { get; set; }
		public string created_at { get; set; }
		public string updated_at { get; set; }
		public string deleted_at { get; set; }
		public Formato formato { get; set; }
		public string Icono { get; set; }


	}

}


/*
import android.os.Parcel;
import android.os.Parcelable;

import io.realm.RealmObject;
import io.realm.annotations.PrimaryKey;
*/


//@PrimaryKey


