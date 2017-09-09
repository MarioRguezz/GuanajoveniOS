using System;
using System.IO;
using Newtonsoft.Json;
using Realms;

namespace Guanajoven
{



	public class Formato: RealmObject
	{
		public int? id_formato { get; set; }
		public string nombre { get; set; }
		public string created_at { get; set; }
		public string updated_at { get; set; }
		public string deleted_at { get; set; }
	}

}


/*
import android.os.Parcel;
import android.os.Parcelable;

import io.realm.RealmObject;
import io.realm.annotations.PrimaryKey;
PrimaryKey*/
