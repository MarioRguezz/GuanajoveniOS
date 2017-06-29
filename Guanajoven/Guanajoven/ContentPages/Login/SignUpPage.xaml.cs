using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Xamarin.Forms;

namespace Guanajoven
{
	public partial class SignUpPage : BasePage
	{
		Boolean first = true;
		string aux = "";
		public SignUpPage()
		{
			InitializeComponent();
			NavigationPage.SetHasNavigationBar(this, false);
			Background.BackgroundColor = Color.FromHex("#b7C7E1F5");

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
			image.FadeTo(1);
			await Navigation.PopAsync();
		}

		async void SignUpClicked(object sender, System.EventArgs e)
		{
			if (isNull(_email.Text))
			{
				DisplayAlert("Error", "Falta correo", "Aceptar");
				return;
			}
			if (isNull(_password.Text))
			{
				DisplayAlert("Error", "Falta contraseña", "Aceptar");
				return;
			}
			if (isNull(_confirmpassword.Text))
			{
				DisplayAlert("Error", "Falta confirmar contraseña", "Aceptar");
				return;
			}
			if (isNull(_curp.Text))
			{
				DisplayAlert("Error", "Falta CURP", "Aceptar");
				return;
			}
			if (isNull(_apellidpat.Text))
			{
				DisplayAlert("Error", "Falta Apellido paterno", "Aceptar");
				return;
			}
			if (isNull(_apellidmat.Text))
			{
				DisplayAlert("Error", "Falta Apellido materno", "Aceptar");
				return;
			}
			if (isNull(_nombre.Text))
			{
				DisplayAlert("Error", "Falta nombre", "Aceptar");
				return;
			}
			if (isNull(fecha_nac.Text))
			{
				DisplayAlert("Error", "Falta fecha de nacimiento", "Aceptar");
				return;
			}
			if (isNull(_cp.Text))
			{
				DisplayAlert("Error", "Falta código postal", "Aceptar");
				return;
			}
			if (pickergenre.SelectedIndex == 0)
			{
				DisplayAlert("Error", "Seleccione un género", "Aceptar");
				return;
			}
			if (pickerstate.SelectedIndex == 0)
			{
				DisplayAlert("Error", "Seleccione un estado", "Aceptar");
				return;
			}
			try
			{
				aux = "data:image/jpeg;base64," + Convert.ToBase64String(bytes);
			}
			catch (Exception ex)
			{
				DisplayAlert("Error", "Falta agregar una imagen", "Aceptar");
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
				user.id_google = null;
				user.id_facebook = null;
				if (ValidateUI())
				{
					ShowProgress("Validando");
					var response = await ClientGuanajoven.registerUser(user);

					if (ValidateResponseRegister(response))
					{
						var newuser = JsonConvert.DeserializeObject<ResponseUsuario>(response);
						PropertiesManager.SaveUserInfo(newuser);
						ShowProgress(IProgressType.LogedIn);
						await Task.Delay(600);
						HideProgress();
						await Navigation.PushModalAsync(new HomeDrawerPage());
					}

					HideProgress();
				}
			}
			else
			{
				DisplayAlert("Error", "Verifique que sus contraseñas coincidan", "Aceptar");
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
				ShowProgress("Validando");
				var response = await ClientGuanajoven.getCurp(_curp.Text);

				if (ValidateResponse(response))
				{
					var ResponseCurp = JsonConvert.DeserializeObject<ResponseCurp>(response);
					_nombre.Text = ResponseCurp.data.nombres;
					fecha_nac.Text = ResponseCurp.data.fechNac;
					_apellidpat.Text = ResponseCurp.data.PrimerApellido;
					_apellidmat.Text = ResponseCurp.data.SegundoApellido;
					await Task.Delay(600);
					HideProgress();
					if (ResponseCurp.data.nombres == null || ResponseCurp.data.nombres == "")
					{
						DisplayAlert("Error", "Verifique su Curp", "Aceptar");
					}
					//await Navigation.PushModalAsync(new RootPage());
				}

				HideProgress();
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



	bool ValidateResponse(string response)
	{
		if (ClientGuanajoven.IsError(response))
		{
			DisplayAlert("Error", "Verifique su Curp", "Aceptar");
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
				break;
			case 2:
				return "M";
				break;
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
				break;
			case 2:
				return "BC";
				break;
			case 3:
				return "BS";
				break;
			case 4:
				return "CC";
				break;
			case 5:
				return "CS";
				break;
			case 6:
				return "CH";
				break;
			case 7:
				return "CL";
				break;
			case 8:
				return "CM";
				break;
			case 9:
				return "DF";
				break;
			case 10:
				return "DG";
				break;
			case 11:
				return "MC";
				break;
			case 12:
				return "GT";
				break;
			case 13:
				return "GR";
				break;
			case 14:
				return "HG";
				break;
			case 15:
				return "JC";
				break;
			case 16:
				return "MC";
				break;
			case 17:
				return "MS";
				break;
			case 18:
				return "NT";
				break;
			case 19:
				return "NL";
				break;
			case 20:
				return "OC";
				break;
			case 21:
				return "PL";
				break;
			case 22:
				return "QT";
				break;
			case 23:
				return "QR";
				break;
			case 24:
				return "SP";
				break;
			case 25:
				return "SL";
				break;
			case 26:
				return "SR";
				break;
			case 27:
				return "TC";
				break;
			case 28:
				return "TS";
				break;
			case 29:
				return "TL";
				break;
			case 30:
				return "VZ";
				break;
			case 31:
				return "YN";
				break;
			case 32:
				return "ZS";
				break;
			case 33:
				return "NE";
				break;
			default:
				return "";

		}
	}

	#endregion
}
}
