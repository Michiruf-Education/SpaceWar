using System;
using OpenTK.Input;

namespace SpaceWar.Framework.Input.GamePad {

	public class GamePadButtonEventArgs : EventArgs {

		public Buttons Button { get; }

		public GamePadButtonEventArgs(Buttons button) {
			Button = button;
		}
	}

}
