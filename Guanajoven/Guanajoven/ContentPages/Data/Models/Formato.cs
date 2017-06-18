using System;
using System.IO;
using Newtonsoft.Json;

namespace Guanajoven
{

	public class Formato
	{
		public int? idFormato { get; set; }
		public string nombre { get; set; }
		public string createdAt { get; set; }
		public string updatedAt { get; set; }
		public string deletedAt { get; set; }
	}
}


/*
import android.os.Parcel;
import android.os.Parcelable;

import io.realm.RealmObject;
import io.realm.annotations.PrimaryKey;
PrimaryKey*/
