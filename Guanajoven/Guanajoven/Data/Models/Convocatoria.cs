using System;
using System.IO;
using Newtonsoft.Json;
using System.Collections.Generic;
using Realms;

namespace Guanajoven
{

	public class Convocatoria : RealmObject
	{
		public int? id_convocatoria { get; set; }
		public string titulo { get; set; }
		public string descripcion { get; set; }
		public string ruta_imagen { get; set; }
		public string fecha_inicio { get; set; }
		public string fecha_cierre { get; set; }
		public int estatus { get; set; }
		public string created_at { get; set; }
		public string updated_at { get; set; }
		public string deleted_at { get; set; }
		public IList<Documento> documentos { get; }
		public string estatusText { get; set; }
		public string fecha { get; set; }
		//private RealmList<Documento> documentos;
	}

	public class ConvocatoriaResponse
	{
		public bool success { get; set; }
		public int status { get; set; }
		public List<object> errors { get; set; }
		public List<Convocatoria> data { get; set; }
	}







}


//import io.realm.RealmList;
//import io.realm.RealmObject;
//import io.realm.annotations.PrimaryKey;

/*
public RealmList<Documento> getDocumentos() { return documentos; }
public void setDocumentos(RealmList<Documento> documentos) { this.documentos = documentos; }
*/
