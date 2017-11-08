using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Guanajoven
{

	public class ClientGuanajoven
	{

		public static HttpClient GetHttpClient()
		{
			HttpClient httpClient = new HttpClient();
			httpClient.Timeout = TimeSpan.FromSeconds(30);
			httpClient.DefaultRequestHeaders.ExpectContinue = true;
			httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
			httpClient.DefaultRequestHeaders.AcceptCharset.Add(new StringWithQualityHeaderValue("UTF-8"));
			httpClient.DefaultRequestHeaders.AcceptEncoding.Add(new StringWithQualityHeaderValue("UTF-8"));

			httpClient.BaseAddress = new Uri(Config.URL);

			return httpClient;
		}


		//Return data curp 
		public static async Task<string> getCurp(string curp)
		{
			var jsonResponse = await PostObject<Curp>(new Curp()
			{
				curp = curp
			}, WEB_METHODS.Curp);

			if (jsonResponse == null)
			{
				return null;
			}

			return jsonResponse;
		}


		//Return data position 
		public static async Task<string> getPosition(string api_token)
		{
			var jsonResponse = await PostObject<TokenPOJO>(new TokenPOJO()
			{
				api_token = api_token
			}, WEB_METHODS.Position);

			if (jsonResponse == null)
			{
				return null;
			}

			return jsonResponse;
		}



		//Return URL from notificacion
		public static async Task<string> GetURL(string titulo, string mensaje)
		{
			var jsonResponse = await PostObject<Notificacion>(new Notificacion()
			{
				titulo = titulo,
				mensaje = mensaje
			}, WEB_METHODS.SetURL);

			if (jsonResponse == null)
			{
				return null;
			}



			return jsonResponse;
		}

		public static async Task<string> setPromotion(string id_promocion, string token)
		{
			var jsonResponse = await PostObject<PromotionService>(new PromotionService()
			{
				id_promocion = id_promocion,
				token = token
			}, WEB_METHODS.RegistrarPromocion);

			if (jsonResponse == null)
			{
				return null;
			}

			return jsonResponse;
		}



		public static async Task<string> registerUser(UsuarioPOJO user)
		{
			var jsonResponse = await PostObject<UsuarioPOJO>(new UsuarioPOJO()
			{
				email = user.email,
				password = user.password,
				confirmar_password = user.confirmar_password,
				apellido_materno = user.apellido_materno,
				apellido_paterno = user.apellido_paterno,
				nombre = user.nombre,
				genero = user.genero,
				curp = user.curp,
				fecha_nacimiento = user.fecha_nacimiento,
				codigo_postal = user.codigo_postal,
				estado_nacimiento = user.estado_nacimiento,
				ruta_imagen = user.ruta_imagen,
				id_google = user.id_google,
				id_facebook = user.id_facebook
			}, WEB_METHODS.Register);
			if (jsonResponse == null)
			{
				return null;
			}
			return jsonResponse;
		}


		public static async Task<string> signin(string email, string password)
		{
			var jsonResponse = await PostObject<UsuarioPOJO>(new UsuarioPOJO()
			{
				email = email,
				password = password
			}, WEB_METHODS.Login);
			if (jsonResponse == null)
			{
				return null;
			}
			return jsonResponse;


		}


		public static async Task<string> EventNotification(string api_token, string id_evento, string latitud, string longitud)
		{
			var jsonResponse = await PostObject<PuntajePOJO>(new PuntajePOJO()
			{
				api_token = api_token,
				id_evento = id_evento,
				latitud = latitud,
				longitud = longitud
			}, WEB_METHODS.NotificacionEvento);
			if (jsonResponse == null)
			{
				return null;
			}
			return jsonResponse;
		}





		//id_usuario
		public static async Task<string> InteresaEvento(string token, string id_evento)
		{
			var jsonResponse = await PostObject<UsuarioEvento>(new UsuarioEvento()
			{
				id_evento = id_evento,
				api_token = token
			}, WEB_METHODS.InteresaEvento);
			if (jsonResponse == null)
			{
				return null;
			}
			return jsonResponse;
		}




		/*
		 * Params api_token & mensaje "cannot be null"
		 * return simple structure
		 * */
		public static async Task<string> EnviarChat(Mensaje mensaje)
		{
			var jsonResponse = await PostObject<Mensaje>(new Mensaje()
			{
				api_token = mensaje.api_token,
				mensaje = mensaje.mensaje
			}, WEB_METHODS.EnviarChat);
			if (jsonResponse == null)
			{
				return null;
			}


			return jsonResponse;
		}




		/*
		 * Params api_token & mensaje "cannot be null"
		 * return responseChat with paginator and messages list
		 * */
		public static async Task<List<ChatModel>> MensajeChat(Mensaje mensaje)
		{
			var jsonResponse = await PostObject<Mensaje>(new Mensaje()
			{
				api_token = mensaje.api_token,
				page = mensaje.page
			}, WEB_METHODS.MensajeChat);
			if (jsonResponse == null)
			{
				return null;
			}


			var x = JsonConvert.DeserializeObject<ResponseChat>(jsonResponse);

			return x.data.data;
		}


		public static async Task<string> recoverpass(string email)
		{
			var jsonResponse = await PostObject<UsuarioPOJO>(new UsuarioPOJO()
			{
				email = email
			}, WEB_METHODS.ForgotPass);
			if (jsonResponse == null)
			{
				return null;
			}
			return jsonResponse;
		}

		/**
		* Verifica si el correo ya está registrado en la BD
		* @param email
		* @return

@POST("usuarios/verificarcorreo")
Call<Response<Boolean>> verificarCorreo(
			@Query("email") String email
); */

		public static async Task<string> verifyemail(string email)
		{
			var jsonResponse = await PostObject<UsuarioPOJO>(new UsuarioPOJO()
			{
				email = email
			}, WEB_METHODS.VerifyEmail);
			if (jsonResponse == null)
			{
				return null;
			}
			return jsonResponse;
		}

		/**
     * Funcionalidad para logueo utilizando cuenta de Google.
     * @param email
     * @param idGoogle
     * @return
	@POST("usuarios/logingoogle")

	Call<Response<Usuario>> loginGoogle(
			@Query("email") String email,
			@Query("id_google") String idGoogle
    );
*/
		public static async Task<string> loginGoogle(string email, string id_google)
		{
			var jsonResponse = await PostObject<UsuarioPOJO>(new UsuarioPOJO()
			{
				email = email,
				id_google = id_google
			}, WEB_METHODS.Google);
			if (jsonResponse == null)
			{
				return null;
			}
			return jsonResponse;
		}


		/**
		 * Funcionalidad para logueo utilizando cuenta de Facebook
		 * @param email
		 * @param idFacebook
		 * @return
		@POST("usuarios/loginfacebook")

		Call<Response<Usuario>> loginFacebook(
				@Query("email") String email,
				@Query("id_facebook") String idFacebook
		); */

		public static async Task<string> loginFacebook(string email, string id_facebook)
		{
			var jsonResponse = await PostObject<UsuarioPOJO>(new UsuarioPOJO()
			{
				email = email,
				id_facebook = id_facebook
			}, WEB_METHODS.Facebook);
			if (jsonResponse == null)
			{
				return null;
			}
			return jsonResponse;
		}

		/**
		 * Método para actualizar el id de firebase 
		 * @param firebase
		 * @return
		 */

		public static async Task<string> getIdFirebase(FirebaseObject firebase)
		{
			var jsonResponse = await PostObject<FirebaseObject>(new FirebaseObject()
			{
				id_usuario = firebase.id_usuario,
				device_token = firebase.device_token,
				os = firebase.os,
			}, WEB_METHODS.EnviarToken);
			if (jsonResponse == null)
			{
				return null;
			}
			return jsonResponse;
		}

		/**
		* Método para remover el id de firebase 
		* @param firebase
		* @return
		*/

		public static async Task<string> CancelarFirebase(FirebaseObject firebase)
		{
			var jsonResponse = await PostObject<FirebaseObject>(new FirebaseObject()
			{
				id_usuario = firebase.id_usuario,
			}, WEB_METHODS.CancelarToken);
			if (jsonResponse == null)
			{
				return null;
			}
			return jsonResponse;
		}






		/**
		 * Método para actualizar el token Guanajoven del código QR
		 * @param apiToken
		 * @return
		@POST("usuarios/actualizar-token-guanajoven")

		Call<Response<String>> actualizarTokenGuanajoven(
				@Query("api_token") String apiToken
		);
		 */

		public static async Task<string> getToken(string api_token)
		{
			var jsonResponse = await PostObject<TokenPOJO>(new TokenPOJO()
			{
				api_token = api_token
			}, WEB_METHODS.TokenGuanajoven);
			if (jsonResponse == null)
			{
				return null;
			}
			return jsonResponse;
		}


		/*
		* Eventos.
		* @param actual date 
		* Return list of events
			*/


		public static async Task<List<Evento>> getEvents(string time)
		{
			var jsonResponse = await GetObject<ResponseEvent>(WEB_METHODS.Evento, false, where: "timestamp=" + time);

			if (jsonResponse == null || jsonResponse.data == null)
			{
				return null;
			}
			return jsonResponse.data;
		}



		public static async Task<bool> sendEmailID(string api_token)
		{
			var jsonResponse = await GetObject<GuanajovenResponse>(WEB_METHODS.SendEmailID, false, where: "api_token=" + api_token);

			if (jsonResponse == null || jsonResponse.data == false)
			{
				return false;
			}
			return jsonResponse.data;
		}





		/*
		* Eventos.
		* @param actual date 
		* Return list of events	*/

		public static async Task<List<Convocatoria>> getCalls(string time)
		{
			var jsonResponse = await GetObject<ConvocatoriaResponse>(WEB_METHODS.Convocatoria, false, where: "timestamp=" + time);

			if (jsonResponse == null || jsonResponse.data == null)
			{
				return null;
			}
			return jsonResponse.data;
		}


		/*
            @GET("regiones")
	Call<Response<ArrayList<Region>>> get(
			@Query("timestamp") String timestamp
    );	*/

		public static async Task<List<Region>> getRegion(string time)
		{
			var jsonResponse = await GetObject<RegionResponse>(WEB_METHODS.Regiones, false, where: "timestamp=" + time);

			if (jsonResponse == null || jsonResponse.data == null)
			{
				return null;
			}
			return jsonResponse.data;
		}


		/*Get Publicidad
		 * */
		public static async Task<List<Publicidad>> getAdvertising(string time)
		{
			var jsonResponse = await GetObject<PublicidadResponse>(WEB_METHODS.Publicidad, false, where: "timestamp=" + time);

			if (jsonResponse == null || jsonResponse.data == null)
			{
				return null;
			}
			return jsonResponse.data;
		}


		/*
			Promociones	*/

		public static async Task<List<Empresa>> getPromotions(string time)
		{
			var jsonResponse = await GetObject<EmpresaResponse>(WEB_METHODS.Promotions, false, where: "timestamp=" + time);

			if (jsonResponse == null || jsonResponse.data == null)
			{
				return null;
			}
			return jsonResponse.data;
		}




		public static async Task<string> getProfile(string api_token)
		{
			var jsonResponse = await PostObject<TokenPOJO>(new TokenPOJO()
			{
				api_token = api_token
			}, WEB_METHODS.ProgileInfo);
			if (jsonResponse == null)
			{
				return null;
			}
			return jsonResponse;
		}

		public static async Task<string> updateProfile(DatosUsuario user)
		{
			var jsonResponse = await PostObject<DatosUsuario>(new DatosUsuario()
			{
				api_token = user.api_token,
				id_nivel_estudios = user.id_nivel_estudios,
				apoyo_proyectos_sociales = user.apoyo_proyectos_sociales,
				premios = user.premios,
				id_capacidad_diferente = user.id_capacidad_diferente,
				id_pueblo_indigena = user.id_pueblo_indigena,
				trabaja = user.trabaja,
				id_programa_beneficiario = user.id_programa_beneficiario,
				idiomas = user.idiomas,
				ruta_imagen = user.ruta_imagen
			}, WEB_METHODS.ProfileUpdate);
			if (jsonResponse == null)
			{
				return null;
			}
			return jsonResponse;
		}


		/*Call<Response<Boolean>> enviarCorreo(
		@Query("id_usuario") int idUsuario,
		@Query("id_convocatoria") int idConvocatoria
        ? id_usuario = 156 & id_convocatoria=30
		    );*/

		public static async Task<string> sendEmail(string token, string idconvocatoria)
		{
			var jsonResponse = await PostObject<EmailPOJO>(new EmailPOJO()
			{
				api_token = token,
				id_convocatoria = idconvocatoria
			}, WEB_METHODS.EmailCall);
			if (jsonResponse == null)
			{
				return null;
			}
			return jsonResponse;
		}

		/*
     * Funciona
     * @param usr_lat
     * @param user_long
		*/
		/*public static async Task<string> getMensajeros(string usr_lat, string user_long)
		{
			var jsonResponse = await PostObject<User>(new User()
			{
				email = mail,
				password = pass,
			}, WEB_METHODS.GetMensajeros);

			if (jsonResponse == null)
			{
				return null;
			}

			return jsonResponse 
		} */



		/*
	 * Progreso del servicio actual.
	 * @param request_id
	 * @param token
	 * @param id
			*/

		/*
		public static async Task<List<Datum>> progreso(int request_id, string token, int id)
		{
			var jsonResponse = await GetObject<ResponseGetCursos>(WEB_METHODS.Cursos, false, where: "request_id=" + request_id + "&token=" + token  + "&id=" + id );

			if (jsonResponse == null || jsonResponse.data == null)
			{
				return null;
			}
			return jsonResponse.data;
		}
		*/





		public static async Task<string> PostObject<T>(object item, WEB_METHODS method, bool igonoreIfnull = false)
		{
			try
			{
				var client = GetHttpClient();
				string json = null;

				if (igonoreIfnull)
				{

					json = JsonConvert.SerializeObject((T)item,
							Newtonsoft.Json.Formatting.None,
							new JsonSerializerSettings
							{
								NullValueHandling = NullValueHandling.Ignore
							});
				}
				else
				{
					json = JsonConvert.SerializeObject((T)item);
				}

				HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");
				client.BaseAddress = new Uri(Config.URL);
				var response = await client.PostAsync(Config.GetURLForMethod(method), content);
				var resultString = await response.Content.ReadAsStringAsync();
				if (response.IsSuccessStatusCode)
				{

					//var result = JsonConvert.DeserializeObject<K>(resultString); 
					return resultString;
				}
				return null;
			}
			catch (Exception ex)
			{
				return null;
			}
		}
		public static async Task<T> GetObject<T>(WEB_METHODS method, bool default_url = true, string where = null, bool igonoreIfnull = false)
		{
			try
			{
				var client = GetHttpClient();

				if (default_url)
					client.BaseAddress = new Uri(Config.URL);

				var response = await client.GetAsync(Config.GetURLForMethod(method) + where);
				if (response.IsSuccessStatusCode)
				{
					var resultString = await response.Content.ReadAsStringAsync();
					if (igonoreIfnull)
					{
						return JsonConvert.DeserializeObject<T>(resultString,
								new JsonSerializerSettings
								{
									NullValueHandling = NullValueHandling.Ignore
								});
					}
					else
					{
						return JsonConvert.DeserializeObject<T>(resultString);
					}
				}
				return default(T);
			}
			catch (Exception ex)
			{
				return default(T);
			}
		}



		#region PARSING
		public static bool IsError(string json)
		{
			if (string.IsNullOrEmpty(json))
				return true;

			if (!json.Contains("success"))
				return false;
			try
			{
				var resp = JsonConvert.DeserializeObject<Response>(json);
				if (resp.success)
					return false;
				else return true;
			}
			catch (Exception ex)
			{
				return true;
			}

			return false;
		}





		public static string IsErrorType(string json)
		{
			if (string.IsNullOrEmpty(json))
				return "";
			try
			{
				var resp = JsonConvert.DeserializeObject<ResponseLogin>(json);
				return resp.errors[0];
			}
			catch (Exception ex)
			{
				return "";

			}
		}


		public static string Data(string json)
		{
			if (string.IsNullOrEmpty(json))
				return "";

			if (!json.Contains("data"))
				return "";
			try
			{
				var resp = JsonConvert.DeserializeObject<Posicion>(json);
				return resp.data;
			}
			catch (Exception ex)
			{
				return "";
			}

		}

		public static WEB_ERROR GetWebError(string json)
		{
			if (json == null)
				return WEB_ERROR.ServerError;

			try
			{
				var resp = JsonConvert.DeserializeObject<Response>(json);
				if (resp.success)
				{
					return WEB_ERROR.NoError;
				}
				else
				{
					if (resp.error.Contains("email.exists"))
						return WEB_ERROR.EmailExists;
					else
						return WEB_ERROR.Error;

				}
			}
			catch (Exception ex)
			{
				return WEB_ERROR.ParseError;
			}
		}
		public static bool IsErrorFalse(string json)
		{

			if (json == null || !json.Contains("false"))
			{
				return false;
			}
			else
			{
				return true;
			}
		}

		public static bool IsGood(string jsonResponse)
		{
			return (GetWebError(jsonResponse) == WEB_ERROR.NoError);
		}

		public static string GetMessageForError(WEB_ERROR error)
		{
			string message = "Error";
			switch (error)
			{
				case WEB_ERROR.EmailExists:
					message = "El correo ya existe";
					break;
				case WEB_ERROR.ServerError:
					message = "Hubo un error en la conexión";
					break;
			}
			return message;
		}

		public class Response
		{
			public bool success { get; set; }
			public List<string> error { get; set; }
		}

		public class ResponseLogin
		{
			public bool success { get; set; }
			public List<string> errors { get; set; }
		}

		public class Posicion
		{
			public string data { get; set; }
		}

		#endregion
	}
}

