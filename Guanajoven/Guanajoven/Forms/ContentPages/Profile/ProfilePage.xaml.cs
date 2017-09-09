using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Plugin.Connectivity;
using Xamarin.Forms;

namespace Guanajoven
{
	public partial class ProfilePage : BasePage
	{
		string url = null;
		async void SetIdiomas(object sender, System.EventArgs e)
		{
			await Navigation.PushModalAsync(new NavigationPage(new PickIdiomas()));
		}

		HomeDrawerPage RootPage;
		public ProfilePage(HomeDrawerPage _rootPage)
		{
			InitializeComponent();
			RootPage = _rootPage;

			NavigationPage.SetHasNavigationBar(this, false);
			Background.BackgroundColor = Color.FromHex("#b7C7E1F5");


			MessagingCenter.Subscribe<CarouselPageIdiomas>(this, "finished_picking", (sender) =>
			{
				SetStackIdiomas();
			});


			ImageSourceChanged = () =>
	{
		if (LastView is FFImageLoading.Forms.CachedImage)
			(LastView as FFImageLoading.Forms.CachedImage).Source = Source;

		_imageView.Source = Source;
	};

		}

		void SetStackIdiomas()
		{
			_idiomasStack.Children.Clear();

			if (HelperIdioma.InfioIdiomas.Count > 0)
			{
				foreach (var keyPair in HelperIdioma.InfioIdiomas)
				{
					_idiomasStack.Children.Add(new IdiomaItemView(keyPair.Value));
				}
			}
		}

		bool firstTime = true;

		protected override void OnAppearing()
		{

			if (firstTime)
			{
				pickerSetStudies();
				pickerSetBeneficiario();
				pickerSetJob();
				pickerSetPopulation();
				pickerSetHandicap();
				pickerSetAwards();
				pickerSetSocial();
				getProfile();
			}


			firstTime = false;
		}


		#region Events

		async void CloseClicked(object sender, System.EventArgs e)
		{
			var image = sender as Image;
			image.Opacity = 0.6;
			image.FadeTo(1);
			//await Navigation.PopAsync();
			//MessagingCenter.Send<ProfilePage>(this, "show_home");
			RootPage.IsPresented = true;

		}




		async void ChangePicture(object sender, EventArgs e)
		{
			TakePictureActionSheet(_imageView);
		}



		async void getProfile()
		{

			ShowProgress("Validando");
			TokenPOJO token = new TokenPOJO();
			var localuser = PropertiesManager.GetUserInfo();
			token.api_token = localuser.data.api_token;
			CheckConnection();
			var response = await ClientGuanajoven.getProfile(token.api_token);

			if (ValidateResponse(response))
			{
				var dataProfile = JsonConvert.DeserializeObject<DataUserResponse>(response);
				SetInfo(dataProfile);
				await Task.Delay(600);
			}
			HideProgress();
		}

		void SetInfo(DataUserResponse profile)
		{
			_imageView.Source = profile.data.ruta_imagen;
			url = profile.data.ruta_imagen;
			NivelEstudios(profile);
			Beneficiario(profile);
			Working(profile);
			Etnia(profile);
			Able(profile);
			Trophy(profile);
			Social(profile);
			Idioms(profile);
		}

		void NivelEstudios(DataUserResponse profile)
		{
			if (profile.data.id_nivel_estudios == null)
			{
			}
			else
			{
				pickernivelestudios.SelectedIndex = (int)profile.data.id_nivel_estudios;
			}
		}

		void Beneficiario(DataUserResponse profile)
		{
			if (profile.data.id_programa_beneficiario == null || profile.data.id_programa_beneficiario == 0)
			{
				pickerbeneficiario.SelectedIndex = 2;
			}
			else
			{
				pickerbeneficiario.SelectedIndex = 1;
				pickerEstado.SelectedIndex = (int)profile.data.id_programa_beneficiario;
			}
		}

		void Working(DataUserResponse profile)
		{
			if (profile.data.trabaja == null || profile.data.trabaja == 0)
			{
				pickerjob.SelectedIndex = 2;
			}
			else
			{
				pickerjob.SelectedIndex = (int)profile.data.trabaja;
			}
		}

		void Etnia(DataUserResponse profile)
		{

			if (profile.data.id_pueblo_indigena == null)
			{
				pickerpopulation.SelectedIndex = 2;
			}
			else
			{
				pickerpopulation.SelectedIndex = 1;
				pickerEtnia.SelectedIndex = (int)profile.data.id_pueblo_indigena;
			}
		}

		void Able(DataUserResponse profile)
		{

			if (profile.data.id_capacidad_diferente == null)
			{
				pickerhandicap.SelectedIndex = 2;
			}
			else
			{
				pickerhandicap.SelectedIndex = 1;
				pickerPhysic.SelectedIndex = (int)profile.data.id_capacidad_diferente;
			}
		}
		void Trophy(DataUserResponse profile)
		{

			if (profile.data.premios == null || profile.data.premios == "")
			{
				pickerawards.SelectedIndex = 2;
			}
			else
			{
				pickerawards.SelectedIndex = 1;
				inputMention.Text = profile.data.premios + "";
			}
		}

		void Social(DataUserResponse profile)
		{

			if (profile.data.apoyo_proyectos_sociales == null || profile.data.apoyo_proyectos_sociales == 2)
			{
				pickersocial.SelectedIndex = 2;
			}
			else
			{
				pickersocial.SelectedIndex = 1;
				inputSocial.Text = profile.data.proyectos_sociales + "";
			}
		}


		void Idioms(DataUserResponse profile)
		{

			if (profile.data.idiomas == null)
			{
				return;
			}

			if (profile.data.idiomas.Count == 0)
			{
				return;
			}
			if (HelperIdioma.InfioIdiomas.Count > 0)
			{
				HelperIdioma.InfioIdiomas.Clear();
			}
			foreach (var idioma in profile.data.idiomas)
			{
				var infoIdioma = new InfoIdioma()
				{
					Nombre = getIdioma((int)idioma.id_idioma_adicional),
					id_datos_usuario = idioma.id_datos_usuario,
					id_idioma_adicional = idioma.id_idioma_adicional,
					lectura = idioma.lectura,
					escritura = idioma.escritura,
					conversacion = idioma.conversacion
				};
				HelperIdioma.InfioIdiomas.Add(getIdioma((int)idioma.id_idioma_adicional), infoIdioma);
			}

			if (HelperIdioma.InfioIdiomas.Count > 0)
			{
				foreach (var keyPair in HelperIdioma.InfioIdiomas)
				{
					_idiomasStack.Children.Add(new IdiomaItemView(keyPair.Value));
				}
			}
		}

		string getIdioma(int name)
		{
			switch (name)
			{
				case 1:
					return "Alemán";
				case 2:
					return "Árabe";
				case 3:
					return "Chino";
				case 4:
					return "Coreano";
				case 5:
					return "Francés";
				case 6:
					return "Inglés";
				case 7:
					return "Italiano";
				case 8:
					return "Japonés";
				case 9:
					return "Polaco";
				case 10:
					return "Portugués";
				case 11:
					return "Ruso";
				case 12:
					return "Otro";
				default:
					return "Otro";

			}
		}



		async void ModifyClicked(object sender, System.EventArgs e)
		{
			var answer = await DisplayAlert("Guanajoven", "¿Deseas modificar tu perfil?", "Sí", "No");
			if (!answer)
			{
			}
			else
			{
				DatosUsuario user = new DatosUsuario();
				var localuser = PropertiesManager.GetUserInfo();
				user.api_token = localuser.data.api_token;
				if (pickernivelestudios.SelectedIndex != 0)
				{
					user.id_nivel_estudios = pickernivelestudios.SelectedIndex;
				}

				if (pickersocial.SelectedIndex != 0 || pickersocial.SelectedIndex != 2)
				{
					user.proyectos_sociales = inputSocial.Text;
					user.apoyo_proyectos_sociales = 1;
				}

				if (pickerawards.SelectedIndex != 0 || pickerawards.SelectedIndex != 2)
				{
					user.premios = inputMention.Text;
				}
				if (pickerhandicap.SelectedIndex != 0 || pickerhandicap.SelectedIndex != 2)
				{
					user.id_capacidad_diferente = pickerPhysic.SelectedIndex;
				}

				if (pickerpopulation.SelectedIndex != 0 || pickerpopulation.SelectedIndex != 2)
				{
					user.id_pueblo_indigena = pickerEtnia.SelectedIndex;
				}

				if (pickerjob.SelectedIndex != 0 || pickerjob.SelectedIndex != 2)
				{
					user.trabaja = pickerjob.SelectedIndex;
				}

				if (pickerbeneficiario.SelectedIndex != 0 || pickerbeneficiario.SelectedIndex != 2)
				{
					user.id_programa_beneficiario = pickerEstado.SelectedIndex;
				}

				if (HelperIdioma.InfioIdiomas.Count > 0)
				{
					user.idiomas = new List<IdiomaAdicional>();
					foreach (var idioma in HelperIdioma.InfioIdiomas)
					{
						var IdiomaAdicional = new IdiomaAdicional()
						{
							id_datos_usuario = (int)idioma.Value.id_datos_usuario,
							id_idioma_adicional = idioma.Value.id_idioma_adicional,
							lectura = idioma.Value.lectura,
							escritura = idioma.Value.escritura,
							conversacion = idioma.Value.conversacion
						};
						user.idiomas.Add(IdiomaAdicional);
					}
				}


				try
				{
					user.ruta_imagen = "data:image/jpeg;base64," + Convert.ToBase64String(bytes);
				}
				catch (Exception ex)
				{
					HttpClient client = new HttpClient();
					client.MaxResponseContentBufferSize = 256000;
					Stream stream = await client.GetStreamAsync(url);
					var xy = stream;
					var y = GetImageStreamAsBytes(xy);
					var imageurl = "data:image/jpeg;base64," + Convert.ToBase64String(y);
					user.ruta_imagen = imageurl;
				}

				CheckConnection();
				ShowProgress("Validando");

				var response = await ClientGuanajoven.updateProfile(user);
				var updateAct = await ClientGuanajoven.getProfile(user.api_token);
				var updated = JsonConvert.DeserializeObject<DataUserResponse>(updateAct);
				var pivot = PropertiesManager.GetUserInfo();
				pivot.data.datos_usuario.ruta_imagen = updated.data.ruta_imagen;
				PropertiesManager.SaveUserInfo(pivot);
				MessagingCenter.Send<ProfilePage>(this, "Hi");

				if (ValidateResponseRegister(response))
				{
					await Task.Delay(600);
					await DisplayAlert("Guanajoven", "Datos Guardados con exito", "Aceptar");
				}

				HideProgress();
			}
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


		bool ValidateResponseRegister(string response)
		{
			if (ClientGuanajoven.IsError(response))
			{
				DisplayAlert("Error", "No pudimos consultar tu información", "Aceptar");
				return false;
			}
			else
			{
				return true;
			}
		}


		#endregion



		#region Pickers
		public void pickerSetStudies()
		{
			pickernivelestudios.Items.Add("Seleccione una opción");
			pickernivelestudios.Items.Add("Primaria");
			pickernivelestudios.Items.Add("Secundaria");
			pickernivelestudios.Items.Add("Preparatoria");
			pickernivelestudios.Items.Add("TSU");
			pickernivelestudios.Items.Add("Universidad");
			pickernivelestudios.Items.Add("Maestría");
			pickernivelestudios.Items.Add("Doctorado");
			pickernivelestudios.Items.Add("Otro");
			pickernivelestudios.SelectedIndex = 0;
			pickernivelestudios.SelectedIndexChanged += (sender, e) =>
					  {
						  if (pickernivelestudios.SelectedIndex == 0)
							  pickernivelestudios.SelectedIndex = 1;
					  };

		}

		public void pickerSetBeneficiario()
		{
			pickerbeneficiario.Items.Add("Seleccione una opción");
			pickerbeneficiario.Items.Add("Sí");
			pickerbeneficiario.Items.Add("No");
			pickerbeneficiario.SelectedIndex = 0;
			pickerbeneficiario.SelectedIndexChanged += (sender, e) =>
			{
				if (pickerbeneficiario.SelectedIndex == 0)
				{
					pickerbeneficiario.SelectedIndex = 1;
				}
				if (pickerbeneficiario.SelectedIndex == 1)
				{
					pickerSetEstado();
				}
				else
				{
					pickerEstado.IsVisible = false;
					labelType.IsVisible = false;
					pickerEstado.SelectedIndex = 0;
				}
			};
		}

		public void pickerSetEstado()
		{
			pickerEstado.IsVisible = true;
			labelType.IsVisible = true;
			pickerEstado.Items.Add("Seleccione una opción");
			pickerEstado.Items.Add("Municipal");
			pickerEstado.Items.Add("Estatal");
			pickerEstado.Items.Add("Federal");
			pickerEstado.Items.Add("Internacional");
			pickerEstado.SelectedIndex = 0;
			pickerEstado.SelectedIndexChanged += (sender, e) =>
			{
				if (pickerEstado.SelectedIndex == 0)
				{
					pickerEstado.SelectedIndex = 1;
				}

			};
		}

		public void pickerSetJob()
		{
			pickerjob.Items.Add("Seleccione una opción");
			pickerjob.Items.Add("Sí");
			pickerjob.Items.Add("No");
			pickerjob.SelectedIndex = 0;
			pickerjob.SelectedIndexChanged += (sender, e) =>
			{
				if (pickerjob.SelectedIndex == 0)
					pickerjob.SelectedIndex = 1;
			};
		}


		public void pickerSetPopulation()
		{
			pickerpopulation.Items.Add("Seleccione una opción");
			pickerpopulation.Items.Add("Sí");
			pickerpopulation.Items.Add("No");
			pickerpopulation.SelectedIndex = 0;
			pickerpopulation.SelectedIndexChanged += (sender, e) =>
			{
				if (pickerpopulation.SelectedIndex == 0)
				{
					pickerpopulation.SelectedIndex = 1;
				}
				if (pickerpopulation.SelectedIndex == 1)
				{
					pickerSetEtnia();
				}
				else
				{
					pickerEtnia.IsVisible = false;
					labelPopulation.IsVisible = false;
					pickerEtnia.SelectedIndex = 0;
				}
			};

		}


		public void pickerSetEtnia()
		{

			pickerEtnia.IsVisible = true;
			labelPopulation.IsVisible = true;
			pickerEtnia.Items.Add("Seleccione una opción");
			pickerEtnia.Items.Add("Otomí");
			pickerEtnia.Items.Add(" Chichimeca-Jonaz");
			pickerEtnia.Items.Add("Náhuatl");
			pickerEtnia.Items.Add("Mazahua");
			pickerEtnia.Items.Add("Otra");
			pickerEtnia.SelectedIndex = 0;
		}

		public void pickerSetHandicap()
		{
			pickerhandicap.Items.Add("Selecciona una opción");
			pickerhandicap.Items.Add("Sí");
			pickerhandicap.Items.Add("No");
			pickerhandicap.SelectedIndex = 0;
			pickerhandicap.SelectedIndexChanged += (sender, e) =>
			 {
				 if (pickerhandicap.SelectedIndex == 0)
				 {
					 pickerhandicap.SelectedIndex = 1;
				 }
				 if (pickerhandicap.SelectedIndex == 1)
				 {
					 pickerSetPhysics();
				 }
				 else
				 {
					 pickerPhysic.IsVisible = false;
					 labelPhysic.IsVisible = false;
					 pickerPhysic.SelectedIndex = 0;
				 }
			 };
		}


		public void pickerSetPhysics()
		{
			pickerPhysic.IsVisible = true;
			labelPhysic.IsVisible = true;
			pickerPhysic.Items.Add("Selecciona una opción");
			pickerPhysic.Items.Add("Física");
			pickerPhysic.Items.Add("Sensorial");
			pickerPhysic.Items.Add("Auditiva Visual");
			pickerPhysic.Items.Add("Psíquica");
			pickerPhysic.Items.Add("Intelectual");
			pickerPhysic.Items.Add("Mental");
			pickerPhysic.SelectedIndex = 0;
		}





		public void pickerSetAwards()
		{
			pickerawards.Items.Add("Seleccione una opción");
			pickerawards.Items.Add("Sí");
			pickerawards.Items.Add("No");
			pickerawards.SelectedIndex = 0;
			pickerawards.SelectedIndexChanged += (sender, e) =>
			{
				if (pickerawards.SelectedIndex == 0)
				{
					pickerawards.SelectedIndex = 1;
				}
				if (pickerawards.SelectedIndex == 1)
				{
					inputMention.IsVisible = true;
					labelMention.IsVisible = true;
				}
				else
				{
					inputMention.IsVisible = false;
					labelMention.IsVisible = false;
					inputMention.Text = "";
				}
			};
		}


		public void pickerSetSocial()
		{
			pickersocial.Items.Add("Seleccione una opción");
			pickersocial.Items.Add("Sí");
			pickersocial.Items.Add("No");
			pickersocial.SelectedIndex = 0;
			pickersocial.SelectedIndexChanged += (sender, e) =>
			 {
				 if (pickersocial.SelectedIndex == 0)
				 {
					 pickersocial.SelectedIndex = 1;
				 }
				 if (pickersocial.SelectedIndex == 1)
				 {
					 socialSet();
				 }
				 else
				 {
					 labelSocial.IsVisible = false;
					 inputSocial.IsVisible = false;
				 }
			 };
		}

		public void socialSet()
		{
			labelSocial.IsVisible = true;
			inputSocial.IsVisible = true;

		}

		#endregion

		bool ValidateResponse(string response)
		{
			if (ClientGuanajoven.IsError(response))
			{
				DisplayAlert("Error", "Hubo un problema al iniciar sesión", "Aceptar");
				return false;
			}
			else
			{
				return true;
			}
		}


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