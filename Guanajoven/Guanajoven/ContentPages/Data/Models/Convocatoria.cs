using System;
using System.IO;
using Newtonsoft.Json;

namespace Guanajoven
{

	public class Convocatoria
	{
		public int? idConvocatoria { get; set; }
		public string titulo { get; set; }
		public string descripcion { get; set; }
		public string rutaImagen { get; set; }
		public string fechaInicio { get; set; }
		public string fechaCierre { get; set; }
		public int estatus { get; set; }
		//private RealmList<Documento> documentos;
		private string createdAt;
		private string updatedAt;
		private string deletedAt;
	}
}


//import io.realm.RealmList;
//import io.realm.RealmObject;
//import io.realm.annotations.PrimaryKey;

/*
public RealmList<Documento> getDocumentos() { return documentos; }
public void setDocumentos(RealmList<Documento> documentos) { this.documentos = documentos; }
*/
