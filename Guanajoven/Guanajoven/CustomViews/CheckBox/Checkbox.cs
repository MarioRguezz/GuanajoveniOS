using System;
using Xamarin.Forms;

namespace Guanajoven
{
	public class Checkbox : Image
	{
		private bool isOn;
		private int optionId;

		public Checkbox (int number)
		{
			Source = "checkbox_off.png";
			WidthRequest = HeightRequest = 25;

			isOn = false;
			optionId = number;
		}

		public bool IsChecked ()
		{
			return isOn;
		}

		private void ToggleCheckbox ()
		{
			if (isOn) {
				Source = "checkbox_off.png";
			} else {
				Source = "checkbox_on.png";
			}

			isOn = !isOn;
		}
		public int GetOptionId ()
		{
			return optionId;
		}

		public void TurnCheckboxOn ()
		{
			Source = "checkbox_on.png";
			isOn = true;
		}

		public void TurnCheckboxOff ()
		{
			Source = "checkbox_off.png";
			isOn = false;
		}
	}
}