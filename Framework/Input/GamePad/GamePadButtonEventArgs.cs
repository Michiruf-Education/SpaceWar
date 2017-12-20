using System;
using OpenTK.Input;

namespace Framework.Input.GamePad {

	public class GamePadButtonEventArgs : EventArgs {

		public Buttons Button { get; }

		public GamePadButtonEventArgs(Buttons button) {
			Button = button;
		}
	}

}
