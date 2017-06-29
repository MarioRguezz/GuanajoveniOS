using System;
namespace Guanajoven
{
	public enum WEB_METHODS
	{
		Curp,
		Register,
		Login,
		ForgotPass,
		VerifyEmail,
		Google,
		Facebook,
		TokenGuanajoven,
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
		public static string URL = "http://localhost/GuanajovenWeb/public/api/";

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
				case WEB_METHODS.Register:
					return "usuarios/registrar";
				case WEB_METHODS.Login:
					return "usuarios/login";
				case WEB_METHODS.ForgotPass:
					return "password/email";
				case WEB_METHODS.VerifyEmail:
					return "usuarios/verificarcorreo";
				case WEB_METHODS.Google:
					return "usuarios/logingoogle";
				case WEB_METHODS.Facebook:
					return "usuarios/loginfacebook";
				case WEB_METHODS.TokenGuanajoven:
					return "usuarios/actualizar-token-guanajoven";
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



