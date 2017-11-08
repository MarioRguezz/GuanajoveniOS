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
		Evento,
		Convocatoria,
		Request,
		Regiones,
		CurrentRequest,
		Position,
		ProgileInfo,
		ProfileUpdate,
		EmailCall,
		Promotions,
		EnviarToken,
		CancelarToken,
		Publicidad,
		EnviarChat,
		MensajeChat,
		NotificacionEvento,
		InteresaEvento,
		SetURL,
		SendEmailID,
		RegistrarPromocion


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
		//public static string URL = "http://172.20.10.3/GuanajovenWeb/public/api/";
		//public static string URL = "http://192.168.0.97/GuanajovenWeb/public/api/";
		//public static string URL = "http://localhost/GuanajovenWeb/public/api/";
		//public static string URL = "http://200.23.39.11/GuanajovenWeb/public/api/";
		public static string URL = "http://guanajovenapp.guanajuato.gob.mx/api/";




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
				case WEB_METHODS.Evento:
					return "eventos?";
				case WEB_METHODS.Convocatoria:
					return "convocatorias?";
				case WEB_METHODS.Regiones:
					return "regiones?";
				case WEB_METHODS.CurrentRequest:
					return "user/currentrequest?";
				case WEB_METHODS.Position:
					return "usuarios/posicion";
				case WEB_METHODS.ProgileInfo:
					return "profile/get";
				case WEB_METHODS.ProfileUpdate:
					return "profile/update";
				case WEB_METHODS.EmailCall:
					//return "notificaciones/convocatoria";
				   	return "convocatorias/notificacion";
				case WEB_METHODS.Promotions:
					return "promociones?";
				case WEB_METHODS.EnviarToken:
					return "notificaciones/enviartoken";
				case WEB_METHODS.CancelarToken:
					return "notificaciones/cancelartoken";
				case WEB_METHODS.Publicidad:
					return "publicidad?";
				case WEB_METHODS.EnviarChat:
					return "chat/enviar";
				case WEB_METHODS.MensajeChat:
					return "chat/mensajes";
				case WEB_METHODS.NotificacionEvento:
					return "eventos/marcar";
				case WEB_METHODS.InteresaEvento:
					//return "notificaciones/evento";
					return "eventos/notificacion";
				case WEB_METHODS.SetURL:
					return "notificaciones/notificacionurl";
				case WEB_METHODS.SendEmailID:
					return "documentos/pdf/idguanajoven?";
				case WEB_METHODS.RegistrarPromocion:
					return "promociones/registrar";




				default:
					break;
			}

			return null;
		}

	}
}




