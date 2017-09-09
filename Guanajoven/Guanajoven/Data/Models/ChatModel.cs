using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

namespace Guanajoven
{


	public class ChatModel
	{
		public int? id_mensaje { get; set; }
		public int? id_chat { get; set; }
		public string mensaje { get; set; }
		public int? envia_usuario { get; set; }
		public int? visto { get; set; }
		public string created_at { get; set; }
		public string updated_at { get; set; }
		public object deleted_at { get; set; }
	}

	public class Paginator
	{
		public int? current_page { get; set; }
		public List<ChatModel> data { get; set; }
		public int? from { get; set; }
		public int? last_page { get; set; }
		public string next_page_url { get; set; }
		public string path { get; set; }
		public int? per_page { get; set; }
		public object prev_page_url { get; set; }
		public int? to { get; set; }
		public int? total { get; set; }
	}

	public class ResponseChat
	{
		public bool success { get; set; }
		public int? status { get; set; }
		public List<object> errors { get; set; }
		public Paginator data { get; set; }
	}



}







