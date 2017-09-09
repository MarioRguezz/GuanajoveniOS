using System;

using Xamarin.Forms;

namespace Guanajoven
{
	public class OAuthSettingsFacebook
	{
		public static string ClientId = "1547755481949405";
		public static string Scope = "email+public_profile";
		public static string RedirectUrl = "https://www.facebook.com/connect/login_success.html";
		public static string AuthorizeUrl = "https://m.facebook.com/dialog/oauth/";
	}

	public class OAuthSettingsGoogle
	{
		//public static string ClientId = "371600553371-ksut82ar9d3ljdp7rhvt82ktgcd8v22m.apps.googleusercontent.com";
	
public static string ClientId = "706757253499-kpo06qfv90ehr4tfqu766u6tsm6dr5s9.apps.googleusercontent.com";
	
		public static string ClientSecret = null;
		public static string AuthorizeUrl = "https://accounts.google.com/o/oauth2/auth";
		public static string AccessTokenUrl = "https://www.googleapis.com/oauth2/v4/token";
		public static string RedirectUrl = "com.08bits.guanajoven:/oauth2Callback";
		public static string Scope = "https://www.googleapis.com/auth/userinfo.profile https://www.googleapis.com/auth/userinfo.email https://www.googleapis.com/auth/plus.login";

	}
}