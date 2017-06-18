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

