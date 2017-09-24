using System;
using System.Collections.Generic;
using System.IO;
using Guanajoven;
using Newtonsoft.Json;
using Realms;
namespace Guanajoven
{



	public class Promotion : RealmObject
	{
		public int id_promocion { get; set; }
		public int id_empresa { get; set; }
		public string titulo { get; set; }
		public string descripcion { get; set; }
		public string bases { get; set; }
		public string codigo_promocion { get; set; }
		public string url_promocion { get; set; }
		public string fecha_inicio { get; set; }
		public string fecha_fin { get; set; }
		public string created_at { get; set; }
		public string updated_at { get; set; }
		public string deleted_at { get; set; }
	}

	public class Empresa : RealmObject
	{
		public int id_empresa { get; set; }
		public string empresa { get; set; }
		public string logo { get; set; }
		public string nombre_comercial { get; set; }
		public string razon_social { get; set; }
		public string convenio { get; set; }
		public string estatus { get; set; }
		public int prioridad { get; set; }
		public string url_empresa { get; set; }
		public string created_at { get; set; }
		public string updated_at { get; set; }
		public string deleted_at { get; set; }
		public IList<Promotion> promociones { get; }
	}

	public class EmpresaResponse
	{
		public bool success { get; set; }
		public int status { get; set; }
		public List<object> errors { get; set; }
		public List<Empresa> data { get; set; }
	}



public class PromotionService
{
	public string id_promocion { get; set; }
	public string token { get; set; }
	}
}








