using System;
namespace Guanajoven
{
	public interface IProgress
	{


		void ShowProgress(string text);

		void ShowProgress(IProgressType type);

		void Dismiss();
	}
	public enum IProgressType
	{
		OK,
		Done,
		LogedIn,
	}

}

