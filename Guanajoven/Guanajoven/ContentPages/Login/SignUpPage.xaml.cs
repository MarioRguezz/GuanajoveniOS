using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Xamarin.Forms;

namespace Guanajoven
{
	public partial class SignUpPage : BasePage
	{
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

			pickerSetGenre();
			pickerSetState();
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
			//await Navigation.PushModalAsync(new SignUpPage());
			//
		}


		/*	public void Curp()
			{
				_curp.Completed += (sender, e) =>
				{

				};
			}*/


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


					var foto =   "data:image/jpeg;base64," +  Convert.ToBase64String(bytes);

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

		#endregion
	}
}
