using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Xamarin.Forms;

namespace Guanajoven
{
	public partial class ChatPage : BasePage
	{

		HomeDrawerPage RootPage;
		ObservableCollection<ChatModel> _items;
		public static List<ChatModel> _chat = null;
		int page = 1;

		public ChatPage(HomeDrawerPage _rootPage)
		{
			InitializeComponent();
			RootPage = _rootPage;


			IntiViews();

			NavigationPage.SetHasNavigationBar(this, false);
		}

		void IntiViews()
		{
			_listView.ItemTemplate = new DataTemplate(typeof(ChatViewCell));
			_items = new ObservableCollection<ChatModel>()
			{
				/*new ChatModel() { envia_usuario = 0, created_at = "xxx:00:xx",mensaje = "compu"},
				new ChatModel() { envia_usuario = 1, created_at = "xxx:11xx",mensaje = "usu"},
				new ChatModel() { envia_usuario = 0, created_at = "xxx:00:xx",mensaje = "compu"},
				new ChatModel() { envia_usuario = 1, created_at = "xxx:11xx",mensaje = "usu"},
				new ChatModel() { envia_usuario = 0, created_at = "xxx:00:xx",mensaje = "compu"},
				new ChatModel() { envia_usuario = 1, created_at = "xxx:11xx",mensaje = "usu"},
				new ChatModel() { envia_usuario = 0, created_at = "xxx:00:xx",mensaje = "compu"},
				new ChatModel() { envia_usuario = 1, created_at = "xxx:11xx",mensaje = "usu"},*/
			}; ;
			_listView.ItemsSource = _items;
			_listView.ItemSelected += (sender, e) =>
			{
				if (e.SelectedItem == null)
					return;
				_listView.SelectedItem = null;
			};

			_listView.Refreshing += (sender, e) =>
		   {
			   GetChatMessages();


		   };

		}

		async void GetChatMessages()
		{
			_listView.IsRefreshing = true;

			var user = PropertiesManager.GetUserInfo();
			var me = new Mensaje();
			me.api_token = user.data.api_token;
			me.page = page + "";

			var mensajes = await ClientGuanajoven.MensajeChat(me);
			_listView.IsRefreshing = false;
			if (mensajes != null && mensajes.Count > 0)
			{
				mensajes.Reverse();

				int n = 0;
				foreach (var mensaje in mensajes)
				{
					_items.Insert(n, mensaje);

					if (page != 1)
						await System.Threading.Tasks.Task.Delay(100);

					n++;
				}

				page++;

				if (page == 2)
				{
					await System.Threading.Tasks.Task.Delay(300);
					ScrollToLast();
				}
			}
		}

		protected override void OnAppearing()
		{
			base.OnAppearing();

			GetChatMessages();

			//ScrollToLast();
			App.CurrentApp.IsInChat = true;
			App.CurrentApp.MensajeRecibido += CurrentApp_MensajeRecibido;
		}

		protected override void OnDisappearing()
		{
			base.OnDisappearing();
			App.CurrentApp.IsInChat = false;
			App.CurrentApp.MensajeRecibido -= CurrentApp_MensajeRecibido;

		}

		void CurrentApp_MensajeRecibido(object sender, Guanajoven.ChatModel e)
		{
			_items.Add(e);
			ScrollToLast(true);
		}



	async void EnviarMensaje(object sender, System.EventArgs e)
	{

		if (!string.IsNullOrEmpty(_entryMensaje.Text))
		{
			var now = DateTime.Now;

			var user = PropertiesManager.GetUserInfo();
			var me = new Mensaje();
			me.api_token = user.data.api_token;
			me.mensaje = _entryMensaje.Text;
			ShowProgress("Enviando");
			var message = await ClientGuanajoven.EnviarChat(me);
			HideProgress();

			var isTrue = ClientGuanajoven.Data(message);
			if (isTrue == "true")
			{
				_items.Add(new ChatModel()
				{
					mensaje = _entryMensaje.Text,
					envia_usuario = 1,
					created_at = now.ToString("u").Substring(0, now.ToString("u").Length - 1),
				});
				ScrollToLast(false);
				_entryMensaje.Text = "";
			}



		}


	}


	async void CloseClicked(object sender, System.EventArgs e)

	{
		var image = sender as Image;
		image.Opacity = 0.6;
		image.FadeTo(1);
		//RootPage.IsPresented = true;
		await Navigation.PushAsync(new AdvertisingPage());
		//await Navigation.PopAsync()		
	}

	void ScrollToLast(bool animated = true)
	{
		if (_items != null)
		{
			_listView.ScrollTo(_items.Last(), ScrollToPosition.End, animated);
		}

	}
}
}
