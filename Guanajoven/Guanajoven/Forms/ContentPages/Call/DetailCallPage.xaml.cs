using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Plugin.Connectivity;
using Xamarin.Forms;

namespace Guanajoven
{
	public partial class DetailCallPage : BasePage
	{
		string id_convocatoria = null;
		public DetailCallPage(Convocatoria call)
		{
			InitializeComponent();
			_image.Source = call.ruta_imagen;
			Title = call.titulo;
			_title.Text = call.titulo;
			id_convocatoria = call.id_convocatoria + "";
			_fechainicio.Text = DateTime.Parse(call.fecha_inicio).ToString("dd/MM/yyyy");
			_fechafin.Text = DateTime.Parse(call.fecha_cierre).ToString("dd/MM/yyyy");


			_flowListView.FlowItemsSource = call.documentos.ToList();

			var x = Math.Ceiling((double)call.documentos.Count / 3);
			_flowListView.HeightRequest = x * _flowListView.HeightRequest;



			/*
					foreach (var item in call.documentos)
					{

						if (item.formato.nombre == "docx")
						{
							item.Icono = "ic_doc-web.png";
						}
						else if (item.formato.nombre == "xlsx")
						{
							item.Icono = "ic_xls-web.png";
						}
						else if (item.formato.nombre == "pdf")
						{
								item.Icono = "ic_pdf-web.png";
						}
						else
						{
								item.Icono = "ic_unknow-web.png";
						}
					}

				}*/


			_flowListView.FlowItemTapped += (sender, e) =>
			{
				var item = e.Item as Documento;
				if (item != null)
					System.Diagnostics.Debug.WriteLine("Tapped {0}", item.titulo);
				Device.OpenUri(new Uri(item.ruta_documento));

			};
		}



		async void information(object sender, System.EventArgs e)
		{
			CheckConnection();
			ShowProgress("Validando");
			var user = PropertiesManager.GetUserInfo();
			//var response = await ClientGuanajoven.sendEmail(user.data.datos_usuario.id_usuario + "", id_convocatoria);
			var response = await ClientGuanajoven.sendEmail(user.data.api_token + "", id_convocatoria);

			if (ValidateResponseRegister(response))
			{
				await Task.Delay(600);
				await DisplayAlert("Guanajoven", "Gracias por interesarte en la convocatoria, en breve te llegará un correo electrónico con más información", "Aceptar");
			}

			HideProgress();
		}

		bool ValidateResponseRegister(string response)
		{
			if (ClientGuanajoven.IsError(response))
			{
				DisplayAlert("Error", "Hubo un problema al ponerse en contacto. Intenta más tarde", "Aceptar");
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
