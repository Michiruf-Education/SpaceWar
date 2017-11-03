using System;
using System.Collections.Generic;
using OpenTK.Input;

namespace SpaceWar.Framework.Input.GamePadHandlers {

	public static class GamePadButtonsHelper {

		[Obsolete]
		private static readonly List<Buttons> PREVIOUS_BUTTONS = new List<Buttons>();

		[Obsolete]
		public static List<Buttons> GetAllButtons() {
			var r = new List<Buttons>();
			r.Add(Buttons.A);
			r.Add(Buttons.B);
			r.Add(Buttons.X);
			r.Add(Buttons.Y);
			r.Add(Buttons.Back);
			r.Add(Buttons.Start);
			r.Add(Buttons.BigButton);
			r.Add(Buttons.LeftShoulder);
			r.Add(Buttons.RightShoulder);
			r.Add(Buttons.LeftStick);
			r.Add(Buttons.RightStick);
			return r;
		}

		[Obsolete]
		internal static void SetPreviousPressedButton(Buttons button) {
			PREVIOUS_BUTTONS.Add(button);
		}

		[Obsolete]
		internal static List<Buttons> GetPreviousPressedButtons() {
			return PREVIOUS_BUTTONS;
		}

		public static List<Buttons> GetPressedButtons(int controllerIndex) {
			return GetPressedButtons(GamePad.GetState(controllerIndex));
		}

		public static List<Buttons> GetPressedButtons(GamePadState state) {
			var r = new List<Buttons>();
			if (state.Buttons.A == ButtonState.Pressed) r.Add(Buttons.A);
			if (state.Buttons.B == ButtonState.Pressed) r.Add(Buttons.B);
			if (state.Buttons.X == ButtonState.Pressed) r.Add(Buttons.X);
			if (state.Buttons.Y == ButtonState.Pressed) r.Add(Buttons.Y);
			if (state.Buttons.Back == ButtonState.Pressed) r.Add(Buttons.Back);
			if (state.Buttons.Start == ButtonState.Pressed) r.Add(Buttons.Start);
			if (state.Buttons.BigButton == ButtonState.Pressed) r.Add(Buttons.BigButton);
			if (state.Buttons.LeftShoulder == ButtonState.Pressed) r.Add(Buttons.LeftShoulder);
			if (state.Buttons.RightShoulder == ButtonState.Pressed) r.Add(Buttons.RightShoulder);
			if (state.Buttons.LeftStick == ButtonState.Pressed) r.Add(Buttons.LeftStick);
			if (state.Buttons.RightStick == ButtonState.Pressed) r.Add(Buttons.RightStick);
			return r;
		}
	}

}
