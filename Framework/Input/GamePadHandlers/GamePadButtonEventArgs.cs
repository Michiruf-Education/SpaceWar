using System;
using OpenTK.Input;

namespace SpaceWar.Framework.Input.GamePadHandlers {

	public class GamePadButtonEventArgs : EventArgs {

		public Buttons Button { get; }

		public GamePadButtonEventArgs(Buttons button) {
			Button = button;
		}
	}

}
