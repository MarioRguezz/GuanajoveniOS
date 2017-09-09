using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Plugin.Connectivity;
using Xamarin.Forms;
using System.Net;

namespace Guanajoven
{
	public partial class SignUpPage : BasePage
	{
		Boolean first = true;
		string aux = "";
		string facebook = null;
		string google = null;
		string imageurl = null;
		string url = null;
		public SignUpPage()
		{
			InitializeComponent();
			NavigationPage.SetHasNavigationBar(this, false);
			//Background.BackgroundColor = Color.FromHex("#b7C7E1F5");
			Background.BackgroundColor = Color.FromHex("#CCFFFFFF");

			ImageSourceChanged = () =>
		{
			if (LastView is FFImageLoading.Forms.CachedImage)
				(LastView as FFImageLoading.Forms.CachedImage).Source = Source;

			_imageView.Source = Source;
		};
		}

		// Social 0 Facebook 1 Google
		public SignUpPage(string id, int social, string email, string picture)
		{
			InitializeComponent();
			NavigationPage.SetHasNavigationBar(this, false);
			Background.BackgroundColor = Color.FromHex("#b7C7E1F5");
			_imageView.Source = picture;
			url = picture;
			_email.Text = email;
			_password.Text = "_";
			_confirmpassword.Text = "_";
			_boxpassword.IsVisible = false;
			_boxconfirmpassword.IsVisible = false;
			_confirmpasswordtext.IsVisible = false;
			_passwordtext.IsVisible = false;
			_password.IsVisible = false;
			_confirmpassword.IsVisible = false;
			if (social == 0)
			{
				facebook = id;
			}
			else
			{
				google = id;
			}
			ImageSourceChanged = () =>
					{
						if (LastView is FFImageLoading.Forms.CachedImage)
							(LastView as FFImageLoading.Forms.CachedImage).Source = Source;

						_imageView.Source = Source;
					};
		}


		protected override void OnAppearing()
		{
			if (first)
			{
				pickerSetGenre();
				pickerSetState();
				first = false;
			}
		}


		#region Events

		async void CloseClicked(object sender, System.EventArgs e)
		{
			var image = sender as Image;
			image.Opacity = 0.6;
			await image.FadeTo(1);
			await Navigation.PopAsync();
		}


		public byte[] GetImageStreamAsBytes(Stream input)
		{
			var buffer = new byte[16 * 1024];
			using (MemoryStream ms = new MemoryStream())
			{
				int read;
				while ((read = input.Read(buffer, 0, buffer.Length)) > 0)
				{
					ms.Write(buffer, 0, read);
				}
				return ms.ToArray();
			}
		}

		async void SignUpClicked(object sender, System.EventArgs e)
		{
			if (isNull(_email.Text))
			{
				await DisplayAlert("Error", "Falta correo", "Aceptar");
				return;
			}
			if (isNull(_password.Text))
			{
				await DisplayAlert("Error", "Falta contraseña", "Aceptar");
				return;
			}
			if (isNull(_confirmpassword.Text))
			{
				await DisplayAlert("Error", "Falta confirmar contraseña", "Aceptar");
				return;
			}
			if (isNull(_curp.Text))
			{
				await DisplayAlert("Error", "Falta CURP", "Aceptar");
				return;
			}
			if (isNull(_apellidpat.Text))
			{
				await DisplayAlert("Error", "Falta Apellido paterno", "Aceptar");
				return;
			}
			if (isNull(_apellidmat.Text))
			{
				await DisplayAlert("Error", "Falta Apellido materno", "Aceptar");
				return;
			}
			if (isNull(_nombre.Text))
			{
				await DisplayAlert("Error", "Falta nombre", "Aceptar");
				return;
			}
			if (isNull(fecha_nac.Text))
			{
				await DisplayAlert("Error", "Falta fecha de nacimiento", "Aceptar");
				return;
			}
			if (isNull(_cp.Text))
			{
				await DisplayAlert("Error", "Falta código postal", "Aceptar");
				return;
			}
			if (pickergenre.SelectedIndex == 0)
			{
				await DisplayAlert("Error", "Seleccione un género", "Aceptar");
				return;
			}
			if (pickerstate.SelectedIndex == 0)
			{
				await DisplayAlert("Error", "Seleccione un estado", "Aceptar");
				return;
			}
			try
			{
				if (facebook != null || google != null)
				{
					HttpClient client = new HttpClient();
					client.MaxResponseContentBufferSize = 256000;
					Stream stream = await client.GetStreamAsync(url);
					var xy = stream;
					var y = GetImageStreamAsBytes(xy);
					imageurl = "data:image/jpeg;base64," + Convert.ToBase64String(y);
					aux = imageurl;
				}
				else
				{
					aux = "data:image/jpeg;base64," + Convert.ToBase64String(bytes);
				}
			}
			catch (Exception ex)
			{
				await DisplayAlert("Error", "Falta agregar una imagen", "Aceptar");
				return;
			}
			if (_confirmpassword.Text == _password.Text)
			{
				var email = _email.Text;
				var password = _password.Text;
				var confirmarPassword = _confirmpassword.Text;
				var curp = _curp.Text;
				var apellidoPaterno = _apellidpat.Text;
				var apellidoMaterno = _apellidmat.Text;
				var nombre = _nombre.Text;
				var genero = getGenre(pickergenre);
				var fechaNacimiento = fecha_nac.Text;
				var codigoPostal = _cp.Text;
				var estadoNacimiento = abrevEstado(pickerstate);
				var rutaImagen = aux;
				UsuarioPOJO user = new UsuarioPOJO();
				user.email = email;
				user.password = password;
				user.confirmar_password = confirmarPassword;
				user.curp = curp;
				user.apellido_paterno = apellidoPaterno;
				user.apellido_materno = apellidoMaterno;
				user.nombre = nombre;
				user.genero = genero + "";
				user.fecha_nacimiento = fechaNacimiento;
				user.codigo_postal = codigoPostal;
				user.estado_nacimiento = estadoNacimiento;
				user.ruta_imagen = rutaImagen;
				user.id_google = google;
				user.id_facebook = facebook;
				if (ValidateUI())
				{
					CheckConnection();
					ShowProgress("Validando");
					var response = await ClientGuanajoven.registerUser(user);

					if (ValidateResponseRegister(response))
					{
						var newuser = JsonConvert.DeserializeObject<ResponseUsuario>(response);
						//getposition 
						var posicion = await ClientGuanajoven.getPosition(newuser.data.api_token);
						newuser.data.posicion = ClientGuanajoven.Data(posicion);
						await Task.Delay(600);
						ShowProgress(IProgressType.LogedIn);
						PropertiesManager.SaveUserInfo(newuser);
						HideProgress();
						await Navigation.PushModalAsync(new HomeDrawerPage());
					}

					HideProgress();
				}
			}
			else
			{
				await DisplayAlert("Error", "Verifique que sus contraseñas coincidan", "Aceptar");
			}
			//await Navigation.PushModalAsync(new SignUpPage());
			//
		}



		bool isNull(string x)
		{
			if (string.IsNullOrEmpty(x))
			{
				return true;
			}
			return false;
		}



		bool ValidateUI()
		{
			if (string.IsNullOrEmpty(_email.Text) || !Regex.IsMatch(_email.Text, @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z", RegexOptions.IgnoreCase))
			{
				DisplayAlert("Error", "Correo no valido", "Aceptar");

				return false;
			}
			return true;
		}



		private async void DynamicEditor_TextChanged(object sender, TextChangedEventArgs e)
		{
			if (String.IsNullOrEmpty(e.NewTextValue) == false)
				_curp.Text = e.NewTextValue.ToUpper();
			if (e.NewTextValue.Length == 18)
			{
				CheckConnection();
				ShowProgress("Validando");
				var response = await ClientGuanajoven.getCurp(_curp.Text);

				if (ValidateResponse(response))
				{
					var ResponseCurp = JsonConvert.DeserializeObject<ResponseCurp>(response);
					_nombre.Text = ResponseCurp.data.nombres;
					fecha_nac.Text = ResponseCurp.data.fechNac;
					_apellidpat.Text = ResponseCurp.data.PrimerApellido;
					_apellidmat.Text = ResponseCurp.data.SegundoApellido;
					selectGenre(ResponseCurp.data.sexo);
					selectEstado(ResponseCurp.data.cveEntidadNac);
					await Task.Delay(600);
					HideProgress();
					if (ResponseCurp.data.nombres == null || ResponseCurp.data.nombres == "")
					{
						DisplayAlert("Error", "No se encontró su CURP en la base de datos", "Aceptar");
					}
					//await Navigation.PushModalAsync(new RootPage());
				}

				HideProgress();
			}
		}

		void selectEstado(string estado)
		{



			switch (estado)
			{
				case "AS":
					pickerstate.SelectedIndex = 1;
					break;
				case "BC":
					pickerstate.SelectedIndex = 2;
					break;
				case "BS":
					pickerstate.SelectedIndex = 3;
					break;
				case "CC":
					pickerstate.SelectedIndex = 4;
					break;
				case "CS":
					pickerstate.SelectedIndex = 5;
					break;
				case "CH":
					pickerstate.SelectedIndex = 6;
					break;
				case "CL":
					pickerstate.SelectedIndex = 7;
					break;
				case "CM":
					pickerstate.SelectedIndex = 8;
					break;
				case "DF":
					pickerstate.SelectedIndex = 9;
					break;
				case "DG":
					pickerstate.SelectedIndex = 10;
					break;
				case "MC":
					pickerstate.SelectedIndex = 11;
					break;
				case "GT":
					pickerstate.SelectedIndex = 12;
					break;
				case "GR":
					pickerstate.SelectedIndex = 13;
					break;
				case "HG":
					pickerstate.SelectedIndex = 14;
					break;
				case "JC":
					pickerstate.SelectedIndex = 15;
					break;
				case "MN":
					pickerstate.SelectedIndex = 16;
					break;
				case "MS":
					pickerstate.SelectedIndex = 17;
					break;
				case "NT":
					pickerstate.SelectedIndex = 18;
					break;
				case "NL":
					pickerstate.SelectedIndex = 19;
					break;
				case "OC":
					pickerstate.SelectedIndex = 20;
					break;
				case "PL":
					pickerstate.SelectedIndex = 21;
					break;
				case "QT":
					pickerstate.SelectedIndex = 22;
					break;
				case "QR":
					pickerstate.SelectedIndex = 23;
					break;
				case "SP":
					pickerstate.SelectedIndex = 24;
					break;
				case "SL":
					pickerstate.SelectedIndex = 25;
					break;
				case "SR":
					pickerstate.SelectedIndex = 26;
					break;
				case "TC":
					pickerstate.SelectedIndex = 27;
					break;
				case "TS":
					pickerstate.SelectedIndex = 28;
					break;
				case "TL":
					pickerstate.SelectedIndex = 29;
					break;
				case "VZ":
					pickerstate.SelectedIndex = 30;
					break;
				case "YN":
					pickerstate.SelectedIndex = 31;
					break;
				case "ZS":
					pickerstate.SelectedIndex = 32;
					break;
				case "NE":
					pickerstate.SelectedIndex = 33;
					break;
				default:
					pickerstate.SelectedIndex = 0;
					break;
			}
		}

		void selectGenre(string genero)
		{
			if (genero == "H")
			{
				pickergenre.SelectedIndex = 1;
			}
			else
			{
				pickergenre.SelectedIndex = 2;
			}
		}


		private async void CPTextChanged(object sender, TextChangedEventArgs e)
		{
			if (String.IsNullOrEmpty(e.NewTextValue) == false)
				if (e.NewTextValue.Contains("."))
				{
					_cp.Text = e.OldTextValue;
				}
				else
				{
				}
			if (e.NewTextValue.Length > 5)
			{
				_cp.Text = e.OldTextValue;
			}

		}

		/*async void bolsaTrabajo(object sender, TextChangedEventArgs e)
		{
			Device.OpenUri(new Uri("https://consultas.curp.gob.mx/CurpSP/gobmx/inicio.jsp"));

		}*/

		async void bolsaTrabajo(object sender, System.EventArgs e)
		{
			Device.OpenUri(new Uri("https://consultas.curp.gob.mx/CurpSP/gobmx/inicio.jsp"));
		}

		bool ValidateResponse(string response)
		{

			if (ClientGuanajoven.IsError(response))
			{
				DisplayAlert("Error", "No se encontró su CURP en la base de datos", "Aceptar");
				return false;
			}
			else
			{
				return true;
			}
		}



		bool ValidateResponseRegister(string response)
		{
			if (ClientGuanajoven.IsError(response))
			{
				DisplayAlert("Error", "Hubo un problema al registrarse", "Aceptar");
				return false;
			}
			else
			{
				return true;
			}
		}



		async void ChangePicture(object sender, EventArgs e)


		{
			TakePictureActionSheet(_imageView);
		}
		#endregion



		#region Pickers
		public void pickerSetGenre()
		{
			pickergenre.Items.Add("Seleccione una opción");
			pickergenre.Items.Add("Masculino");
			pickergenre.Items.Add("Femenino");
			pickergenre.SelectedIndex = 0;
			pickergenre.SelectedIndexChanged += (sender, e) =>
			{
				if (pickergenre.SelectedIndex == 0)
					pickergenre.SelectedIndex = 1;
			};
		}

		public void pickerSetState()
		{
			pickerstate.Items.Add("Seleccione una opción");
			pickerstate.Items.Add("Aguascalientes");
			pickerstate.Items.Add("Baja California");
			pickerstate.Items.Add("Baja California Sur");
			pickerstate.Items.Add("Campeche");
			pickerstate.Items.Add("Chiapas");
			pickerstate.Items.Add("Chihuahua");
			pickerstate.Items.Add("Coahuila");
			pickerstate.Items.Add("Colima");
			pickerstate.Items.Add("Distrito Federal");
			pickerstate.Items.Add("Durango");
			pickerstate.Items.Add("Estado de México");
			pickerstate.Items.Add("Guanajuato");
			pickerstate.Items.Add("Guerrero");
			pickerstate.Items.Add("Hidalgo");
			pickerstate.Items.Add("Jalisco");
			pickerstate.Items.Add("Michoacán");
			pickerstate.Items.Add("Morelos");
			pickerstate.Items.Add("Nayarit");
			pickerstate.Items.Add("Nuevo León");
			pickerstate.Items.Add("Oaxaca");
			pickerstate.Items.Add("Puebla");
			pickerstate.Items.Add("Querétaro");
			pickerstate.Items.Add("Quintana Roo");
			pickerstate.Items.Add("San Luis Potosí");
			pickerstate.Items.Add("Sinaloa");
			pickerstate.Items.Add("Sonora");
			pickerstate.Items.Add("Tabasco");
			pickerstate.Items.Add("Tamaulipas");
			pickerstate.Items.Add("Tlaxcala");
			pickerstate.Items.Add("Veracruz");
			pickerstate.Items.Add("Yucatán");
			pickerstate.Items.Add("Zacatecas");
			pickerstate.Items.Add("Nacido en el extranjero");
			pickerstate.SelectedIndex = 0;
			pickerstate.SelectedIndexChanged += (sender, e) =>
						{
							if (pickerstate.SelectedIndex == 0)
								pickerstate.SelectedIndex = 1;
						};
		}



		public string getGenre(Picker estadoNacimiento)
		{
			switch (estadoNacimiento.SelectedIndex)
			{
				case 1:
					return "H";

				case 2:
					return "M";

				default:
					return "";
			}
		}



		public string abrevEstado(Picker estadoNacimiento)
		{
			switch (estadoNacimiento.SelectedIndex)
			{
				case 1:
					return "AS";

				case 2:
					return "BC";

				case 3:
					return "BS";

				case 4:
					return "CC";
				case 5:
					return "CS";

				case 6:
					return "CH";

				case 7:
					return "CL";

				case 8:
					return "CM";

				case 9:
					return "DF";

				case 10:
					return "DG";

				case 11:
					return "MC";

				case 12:
					return "GT";

				case 13:
					return "GR";

				case 14:
					return "HG";

				case 15:
					return "JC";

				case 16:
					return "MN";

				case 17:
					return "MS";

				case 18:
					return "NT";

				case 19:
					return "NL";
					;
				case 20:
					return "OC";

				case 21:
					return "PL";

				case 22:
					return "QT";

				case 23:
					return "QR";

				case 24:
					return "SP";

				case 25:
					return "SL";

				case 26:
					return "SR";

				case 27:
					return "TC";

				case 28:
					return "TS";

				case 29:
					return "TL";

				case 30:
					return "VZ";

				case 31:
					return "YN";

				case 32:
					return "ZS";

				case 33:
					return "NE";

				default:
					return "";

			}
		}

		#endregion

		#region  Connection
		async void CheckConnection()
		{
			try
			{
				//var res = await CrossConnectivity.Current.IsReachable("http://www.google.com") ? "Reachable" : "Not reachable";
				var res = await CrossConnectivity.Current.IsReachable("http://www.google.com") ? true : false;

				if (res)
				{
					System.Diagnostics.Debug.WriteLine("conexion");
				}
				else
				{
					//await DisplayAlert("Error", "Verifique su conexión a internet", "Aceptar");
					return;
				}
			}
			catch (Exception ex)
			{
			}
		}
		#endregion
	}
}
