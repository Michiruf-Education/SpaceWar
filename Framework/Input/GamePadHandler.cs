using System;
using System.Collections.Generic;
using System.Linq;
using OpenTK.Input;
using SpaceWar.Framework.Input.GamePadHandlers;

namespace SpaceWar.Framework.Input {

	public class GamePadHandler {

		public event EventHandler<GamePadButtonEventArgs> OnButtonDown;
		public event EventHandler<GamePadButtonEventArgs> OnButtonUp;

		private readonly List<Buttons> previousUpdateButtons = new List<Buttons>();

		public void Update() {
			var currentButtons = GamePadButtonsHelper.GetPressedButtons(0);

			previousUpdateButtons.Except(currentButtons).ToList().ForEach(button =>
				OnButtonUp?.Invoke(0, new GamePadButtonEventArgs(button)));
			currentButtons.Except(previousUpdateButtons).ToList().ForEach(button =>
				OnButtonDown?.Invoke(0, new GamePadButtonEventArgs(button)));

			previousUpdateButtons.Clear();
			previousUpdateButtons.AddRange(currentButtons);
		}
	}

}
