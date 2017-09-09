using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

namespace Guanajoven
{


	public class Curp
	{
		//public ExtensionData ExtensionData { get; set; }
		public string statusOper { get; set; }
		public string message { get; set; }
		public string TipoError { get; set; }
		public string CodigoError { get; set; }
		public string nombres { get; set; }
		public string sexo { get; set; }
		public string fechNac { get; set; }
		public string nacionalidad { get; set; }
		public string docProbatorio { get; set; }
		public string anioReg { get; set; }
		public string foja { get; set; }
		public string tomo { get; set; }
		public string libro { get; set; }
		public string curp { get; set; }
		public string numActa { get; set; }
		public string numEntidadReg { get; set; }
		public string cveMunicipioReg { get; set; }
		public string NumRegExtranjeros { get; set; }
		public string FolioCarta { get; set; }
		public string cveEntidadNac { get; set; }
		public string cveEntidadEmisora { get; set; }
		public string statusCurp { get; set; }
		public string CurpRenapo { get; set; }
		public string PrimerApellido { get; set; }
		public string SegundoApellido { get; set; }
	}

	public class ResponseCurp
	{
		public bool success { get; set; }
		public int status { get; set; }
		public List<object> errors { get; set; }
		public Curp data { get; set; }
	}



}


/*
	@SerializedName("statusOper")
	@SerializedName("fechNac")
	@SerializedName("anioReg")
	@SerializedName("cveEntidadNac")
	@SerializedName("cveMunicipioReg")
	@SerializedName("statusCurp")
	@SerializedName("CurpRenapo")
	@SerializedName("PrimerApellido")
	@SerializedName("SegundoApellido")
	*/
