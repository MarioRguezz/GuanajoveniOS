using System;
using System.IO;
using Newtonsoft.Json;

namespace Guanajoven
{

	public class Tweet
	{
		public string idStr { get; set; }
		public string text { get; set; }
		public UsuarioTweet user { get; set; }
	}
}


/*
	@SerializedName("id_str")

	@SerializedName("text")

@SerializedName("user")*/