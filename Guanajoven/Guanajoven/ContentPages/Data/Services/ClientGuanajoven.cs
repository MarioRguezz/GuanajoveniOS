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
				id_google = null,  //before and working was ""
				id_facebook = null
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
     * Método para actualizar el token Guanajoven del código QR
     * @param apiToken
     * @return
	@POST("usuarios/actualizar-token-guanajoven")

	Call<Response<String>> actualizarTokenGuanajoven(
			@Query("api_token") String apiToken
    );
     */

		public static async Task<string> getToken(string  api_token)
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

		#endregion
	}
}

