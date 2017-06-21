using System;
namespace Guanajoven
{


	public class PictureData
	{
		public bool is_silhouette { get; set; }
		public string url { get; set; }
	}

	public class FacebookPicture
	{
		public PictureData data { get; set; }
	}

	public class FacebookUserModel
	{
		public string id { get; set; }
		public string first_name { get; set; }
		public string last_name { get; set; }
		public string email { get; set; }
		public FacebookPicture picture { get; set; }
	}
}
