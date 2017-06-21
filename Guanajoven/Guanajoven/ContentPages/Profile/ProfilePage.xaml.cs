using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace Guanajoven
{
	public partial class ProfilePage : BasePage
	{
		HomeDrawerPage RootPage;
		public ProfilePage(HomeDrawerPage _rootPage)
		{
			InitializeComponent();
			RootPage = _rootPage;

			NavigationPage.SetHasNavigationBar(this, false);
			Background.BackgroundColor = Color.FromHex("#b7C7E1F5");
		}


		protected override void OnAppearing()
		{

			pickerSetStudies();
			pickerSetBeneficiario();
			pickerSetJob();
			pickerSetPopulation();
			pickerSetHandicap();
			pickerSetAwards();
			pickerSetSocial();
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

		async void SignUpClicked(object sender, System.EventArgs e)
		{
			//await Navigation.PushModalAsync(new SignUpPage());
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
	}
}