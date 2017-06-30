using System;

namespace Guanajoven
{
	public interface IPushNotifications
	{
		void Register();
		void Unregister();
		string IdUnique();
	}
}