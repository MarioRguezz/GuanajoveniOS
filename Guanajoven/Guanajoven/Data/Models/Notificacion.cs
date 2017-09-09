using System;
using System.IO;
using Newtonsoft.Json;
using Realms;

namespace Guanajoven
{

	public class Notificacion : RealmObject
	{ 
		 
		public string titulo { get; set; }
		public string mensaje { get; set; }
		public string fecha_emision { get; set; }
		public string url { get; set; }

	}




}



/*import io.realm.RealmObject;
import io.realm.annotations.PrimaryKey;
	@PrimaryKey*/

