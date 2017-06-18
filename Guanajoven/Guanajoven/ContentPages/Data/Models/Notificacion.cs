using System;
using System.IO;
using Newtonsoft.Json;

namespace Guanajoven
{

	public class Notificacion
	{
		public int? idNotificacion { get; set; }
		public string titulo { get; set; }
		public string mensaje { get; set; }
		public string fechaEmision { get; set; }
		public int edadMinima { get; set; }
		public int edadMaxima { get; set; }
		public int idGenero { get; set; }
		public string url { get; set; }
		public string createdAt { get; set; }
		public string updatedAt { get; set; }
		public string deletedAt { get; set; }

	}
}



/*import io.realm.RealmObject;
import io.realm.annotations.PrimaryKey;
	@PrimaryKey*/

