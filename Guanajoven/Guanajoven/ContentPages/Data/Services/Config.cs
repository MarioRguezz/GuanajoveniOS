using System;
namespace Guanajoven
{
	public enum WEB_METHODS
	{
		Curp,
		GetMensajeros,
		Promociones,
		Destination,
		Update,
		Login,
		Registro,
		Solicitar,
		Cancelar,
		Request,
		Historial,
		CurrentRequest,
		Rate,

	}

	public enum WEB_ERROR
	{
		Error,
		NoError,
		EmailExists,
		ParseError,
		ServerError
	}

	public class Config
	{
		//	public static string URL = "http://189.211.207.173/courier/public/";
		//public static string URL = "http://10.0.7.134/GuanajovenWeb/public/api/";
		public static string URL = "http://200.23.39.11/GuanajovenWeb/public/api/";

		/*
		private static Realm realm;
		public static String LAST_UPDATE_CONVOCATORIAS = "last_update_convocatorias";
		public static String LAST_UPDATE_REGIONES = "last_update_regiones";
		public static final String LAST_UPDATE_PUBLICIDAD = "last_update_publicidad";
		public static String LAST_UPDATE_EVENTOS = "last_update_eventos";		*/



		public static string GetURLForMethod(WEB_METHODS method)
		{

			switch (method)
			{
				case WEB_METHODS.Curp:
					return "usuarios/curp";
				case WEB_METHODS.Promociones:
					return "user/apply-promo";
				case WEB_METHODS.Destination:
					return "user/setdestination";
				case WEB_METHODS.Update:
					return "user/update";
				case WEB_METHODS.Login:
					return "user/login";
				case WEB_METHODS.Registro:
					return "user/register";
				case WEB_METHODS.Solicitar:
					return "user/createrequestproviders";
				case WEB_METHODS.Cancelar:
					return "user/cancelrequest";
				case WEB_METHODS.Request:
					return "user/getrequest?";
				case WEB_METHODS.Historial:
					return "user/history?";
				case WEB_METHODS.CurrentRequest:
					return "user/currentrequest?";
				case WEB_METHODS.Rate:
					return "user/rate?";



				default:

					break;
			}

			return null;
		}

	}
}



