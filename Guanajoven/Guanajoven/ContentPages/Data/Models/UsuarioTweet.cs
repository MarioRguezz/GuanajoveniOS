using System;
using System.IO;
using Newtonsoft.Json;

namespace Guanajoven
{

	public class UsuarioTweet
	{
		public string idStr { get; set; }
		public string name { get; set; }
		public string screenName { get; set; }
		public string profileImageUrl { get; set; }
	}
}

/*
@SerializedName("id_str")

	@SerializedName("name")

	@SerializedName("screen_name")

	@SerializedName("profile_image_url")*/