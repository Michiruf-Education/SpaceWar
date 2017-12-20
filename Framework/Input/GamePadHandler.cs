using System;
using System.Collections.Generic;
using System.Linq;
using Framework.Input.GamePad;
using OpenTK.Input;

namespace Framework.Input {

	public class GamePadHandler {

		public event EventHandler<GamePadButtonEventArgs> OnButtonDown;
		public event EventHandler<GamePadButtonEventArgs> OnButtonUp;

		private readonly List<Buttons> previousUpdateButtons = new List<Buttons>();
		private readonly List<Buttons> previousUpdateDPadButtons = new List<Buttons>();

		public void Update() {
			var currentButtons = GamePadButtonsHelper.GetPressedButtons(0);
			previousUpdateButtons.Except(currentButtons).ToList().ForEach(button =>
				OnButtonUp?.Invoke(0, new GamePadButtonEventArgs(button)));
			currentButtons.Except(previousUpdateButtons).ToList().ForEach(button =>
				OnButtonDown?.Invoke(0, new GamePadButtonEventArgs(button)));
			previousUpdateButtons.Clear();
			previousUpdateButtons.AddRange(currentButtons);
			
			var currentDPadButtons = GamePadButtonsHelper.GetPressedDPadButtons(0);
			previousUpdateDPadButtons.Except(currentDPadButtons).ToList().ForEach(button =>
				OnButtonUp?.Invoke(0, new GamePadButtonEventArgs(button)));
			currentDPadButtons.Except(previousUpdateDPadButtons).ToList().ForEach(button =>
				OnButtonDown?.Invoke(0, new GamePadButtonEventArgs(button)));
			previousUpdateDPadButtons.Clear();
			previousUpdateDPadButtons.AddRange(currentDPadButtons);
		}
	}

}
