using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace Guanajoven
{
	public partial class ChatViewCell : ViewCell
	{
		public ChatViewCell()
		{
			InitializeComponent();
		}

		protected override void OnBindingContextChanged()
		{
			base.OnBindingContextChanged();
			if (BindingContext != null)
			{
				var mensaje = BindingContext as ChatModel;
				if (mensaje != null)
				{
					_mensaje.Text = mensaje.mensaje;
					_hora.Text = mensaje.created_at;


					if (mensaje.envia_usuario == 0)
					{
						_mensaje.HorizontalTextAlignment = TextAlignment.Start;
						_hora.HorizontalTextAlignment = TextAlignment.Start;
						_frame.HorizontalOptions = LayoutOptions.Start;
						_hora.FontAttributes = FontAttributes.Italic | FontAttributes.Bold;
						_mensaje.FontAttributes = FontAttributes.Italic;
						_frame.BackgroundColor = Color.FromHex("#B23F62");
					}
					else if (mensaje.envia_usuario == 1)
					{
						_mensaje.HorizontalTextAlignment = TextAlignment.End;
						_hora.HorizontalTextAlignment = TextAlignment.End;
						_frame.HorizontalOptions = LayoutOptions.End;
						_frame.BackgroundColor = Color.FromHex("#003464");
					}
				}
			}
		}
	}
}
