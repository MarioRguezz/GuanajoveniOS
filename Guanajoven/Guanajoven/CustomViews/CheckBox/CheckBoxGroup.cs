using System;
using Xamarin.Forms;
using System.Collections.Generic;

namespace Guanajoven
{
	public class CheckBoxGroup : StackLayout
	{
		List<Checkbox> optionRatingList;
		string [] optionTextArray;
		public Label Label;

		public List<string> Values {
			get { return (List<string>)GetValue (RatingValuesProperty); }
			set {
				SetValue (RatingValuesProperty, value);
			}
		}

		public static readonly BindableProperty RatingValuesProperty =
			BindableProperty.Create<CheckBoxGroup, List<string>> (
				ratingView => ratingView.Values,
				new List<string> ()
			);


		static string [] defalutValue = new string [] { "" };

		public CheckBoxGroup (string [] options = null)
		{
			optionRatingList = new List<Checkbox> ();
			//optionTextArray = new string []
			//{
			//	"Ventas",
			//	"Imagen",
			//	"Estrategias",
			//	"Otro"
			//};

			if (options != null) {
				optionTextArray = options;
			} else {
				optionTextArray = new string [] { };
			}

			Label = new Label ();

			/* Initial setup of RatingValues */
			Values = new List<string> ();

			Orientation = StackOrientation.Horizontal;

			/* Set up view */
			Spacing = 10;


			var stackChecks = new StackLayout () {
				Orientation = StackOrientation.Vertical,
				Spacing = 10,
				HorizontalOptions = LayoutOptions.FillAndExpand,
			};



			/* Add subviews: StackLayouts(horizontal) with checkbox and text */
			for (int i = 1; i <= optionTextArray.Length; i++) {
				var option = CreateOption (i);
				var optionStack = CreateOptionStack (option, optionTextArray [i - 1]);

				var vertStack = new StackLayout {
					Orientation = StackOrientation.Vertical,
					Spacing = 0,
				};

				vertStack.Children.Add (optionStack);
				if (i < optionTextArray.Length)
					vertStack.Children.Add (new BoxView () {
						HeightRequest = 1,
						HorizontalOptions = LayoutOptions.FillAndExpand,
						BackgroundColor = Color.FromHex ("CFD0D0"),
					});

				stackChecks.Children.Add (vertStack);
				optionRatingList.Add (option);
			}

			Children.Add (Label);
			Children.Add (stackChecks);
		}
 

		Checkbox CreateOption (int optionId)
		{
			var option = new Checkbox (optionId) {
				HorizontalOptions = LayoutOptions.Start,
				VerticalOptions = LayoutOptions.CenterAndExpand
			};

			return option;
		}

		StackLayout CreateOptionStack (Checkbox option, string optionText, bool last = false)
		{
			var optionStack = new StackLayout {
				Orientation = StackOrientation.Horizontal,
				HorizontalOptions = LayoutOptions.FillAndExpand,
				Spacing = 10,
				Padding = 5,
			};
			optionStack.Children.Add (option);
			optionStack.Children.Add (CreateOptionLabel (optionText));
			optionStack.GestureRecognizers.Add (CreateOptionTappedRecognizer ());

			return optionStack;
		}

		Label CreateOptionLabel (string optionText)
		{
			var optionLabel = new Label {
				Text = optionText,
				TextColor = Color.FromHex ("424244"),
				HorizontalOptions = LayoutOptions.FillAndExpand,
				VerticalTextAlignment = TextAlignment.Center,
				HorizontalTextAlignment = TextAlignment.Start,
			};

			return optionLabel;
		}

		TapGestureRecognizer CreateOptionTappedRecognizer ()
		{
			var optionTapEventHandler = new EventHandler (delegate (object sender, EventArgs e) {
				var optionStackTapped = (StackLayout)sender;
				var optionTapped = (Checkbox)optionStackTapped.Children [0];
				UpdateRating (optionTapped.GetOptionId ());
			});

			var optionTap = new TapGestureRecognizer ();
			optionTap.NumberOfTapsRequired = 1;
			optionTap.Tapped += optionTapEventHandler;

			return optionTap;
		}

		void UpdateRating (int optionId)
		{
			Checkbox checkBox = optionRatingList [optionId - 1];

			if (checkBox.IsChecked ()) {
				checkBox.TurnCheckboxOff ();
				Values.Remove (optionTextArray [optionId - 1]);
			} else {
				checkBox.TurnCheckboxOn ();
				Values.Add (optionTextArray [optionId - 1]);
			}
		}
	}
}
