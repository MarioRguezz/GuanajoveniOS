using System;
using System.IO;
using Newtonsoft.Json;

namespace Guanajoven
{

	public class Curp
	{
		public string statusOper { get; set; }
		public string message { get; set; }
		public string nombres { get; set; }
		public string sexo { get; set; }
		public string fechNac { get; set; }
		public string nacionalidad { get; set; }
		public string anioReg { get; set; }
		public string cveEntidadNac { get; set; }
		public string cveMunicipioReg { get; set; }
		public string statusCurp { get; set; }
		public string CurpRenapo { get; set; }
		public string PrimerApellido { get; set; }
public string SegundoApellido { get; set; }
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