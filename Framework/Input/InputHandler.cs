using System;
using System.Collections.Generic;
using System.Linq;
using OpenTK.Input;
using OpenTK.Platform;
using SpaceWar.Framework.Input.GamePadHandlers;

namespace SpaceWar.Framework.Input {

	public static class InputHandler {

		private static readonly Dictionary<Key, object> KEYBOARD_MAP = new Dictionary<Key, object>();
		private static readonly Dictionary<MouseButton, object> MOUSE_MAP = new Dictionary<MouseButton, object>();
		private static readonly Dictionary<Buttons, object> CONTROLLER_MAP = new Dictionary<Buttons, object>();

		private static readonly List<object> ACTIVE_ACTIONS = new List<object>();

		public static void Init() {
			KEYBOARD_MAP.Clear();
			MOUSE_MAP.Clear();
			CONTROLLER_MAP.Clear();

			ACTIVE_ACTIONS.Clear();
		}

		public static void RegisterInputs(Dictionary<Enum, object> inputs) {
			foreach (var input in inputs) {
				switch (input.Key) {
					case Key key:
						KEYBOARD_MAP.Add(key, input.Value);
						break;
					case Buttons button:
						CONTROLLER_MAP.Add(button, input.Value);
						break;
					case MouseButton mouseButton:
						MOUSE_MAP.Add(mouseButton, input.Value);
						break;
				}
			}
		}

		public static void RegisterWindow(IGameWindow gameWindow) {
			gameWindow.KeyDown += (sender, args) => HandleKeyEvent(KEYBOARD_MAP, args.Key, true);
			gameWindow.KeyUp += (sender, args) => HandleKeyEvent(KEYBOARD_MAP, args.Key, false);
			
			gameWindow.MouseDown += (sender, args) => HandleKeyEvent(MOUSE_MAP, args.Button, true);
			gameWindow.MouseUp += (sender, args) => HandleKeyEvent(MOUSE_MAP, args.Button, true);

			var gamePadHandler = new GamePadHandler();
			gamePadHandler.OnButtonDown += (sender, args) => HandleKeyEvent(CONTROLLER_MAP, args.Button, true);
			gamePadHandler.OnButtonUp += (sender, args) => HandleKeyEvent(CONTROLLER_MAP, args.Button, false);
			gameWindow.UpdateFrame += (sender, args) => gamePadHandler.Update();
		}

		static void HandleKeyEvent<T>(IReadOnlyDictionary<T, object> map, T control, bool press) {
			if (press) {
				if (!map.TryGetValue(control, out var key)) {
					return;
				}
				if (ACTIVE_ACTIONS.Contains(key)) {
					return;
				}
				ACTIVE_ACTIONS.Add(key);
			} else {
				if (!map.TryGetValue(control, out var key)) {
					return;
				}
				ACTIVE_ACTIONS.Remove(key);
			}
		}

		public static bool KeyDown(object action) {
			return ACTIVE_ACTIONS.Contains(action);
		}

		[Obsolete]
		static void HandleControllerEvent(int index) {
			var pressedButtons = GamePadButtonsHelper.GetPressedButtons(index);

			GamePadButtonsHelper.GetPreviousPressedButtons().Except(pressedButtons).ToList().ForEach(removedButton => {
				if (CONTROLLER_MAP.TryGetValue(removedButton, out var controllerAction)) {
					ACTIVE_ACTIONS.Remove(controllerAction);
				}
			});

			GamePadButtonsHelper.GetPressedButtons(index).ForEach(button => {
				if (CONTROLLER_MAP.TryGetValue(button, out var controllerAction)) {
					ACTIVE_ACTIONS.Add(controllerAction);
					GamePadButtonsHelper.SetPreviousPressedButton(button);
				}
			});
		}

		// TODO Integrate this anyhow to be event based after?
		// -> Maybe in Update()?
		// -> Maybe in KeyDown(action, int controllerIndex = -1)?
		// -> Create a cache?
		// -> Extend for any axis or whatever!
		[Obsolete]
		public static bool GamePadPressed(int index, object action) {
			return GamePadButtonsHelper.GetPressedButtons(index).Select(button => {
				CONTROLLER_MAP.TryGetValue(button, out var controllerAction);
				return action.Equals(controllerAction);
			}).FirstOrDefault();


			// Iterative try
			//foreach (var button in GamePadButtonsReverse.GetButtons(index)) {
			//	CONTROLLER_MAP.TryGetValue(button, out var controllerAction);
			//	if (action.Equals(controllerAction)) {
			//		return true;
			//	}
			//}
			//return false;


			// String try (useless because we can wrap that also like above)
			//private static readonly Regex REVERSE_REGEX_DATA = new Regex(@"Buttons: ([A-Za-z]+)");
			//var gamePadState = GamePad.GetState(index);
			//if (FrameworkDebugMode.IsEnabled) {
			//	Console.WriteLine(gamePadState);
			//}
			//var match = REVERSE_REGEX_DATA.Match(gamePadState.ToString());
			//if (!match.Success) {
			//	return false;
			//}
			//var buttonsString = match.Groups[1].Value;
			//var button = GamePadButtonsReverse.ResolveButton(buttonsString);
			//if (button == 0) {
			//	return false;
			//}
			//CONTROLLER_MAP.TryGetValue(button, out var controllerAction);
			//return action.Equals(controllerAction);


			// Reflection try (useless, because ToString might be better anyway)
			//Buttons key = Buttons.A;
			//var gamePadState2 = GamePad.GetState(index);
			//var methodInfo = typeof(GamePadButtons).GetMethod("GetButton", new[] {typeof(Buttons)});
			//var buttonState = (ButtonState) methodInfo.Invoke(gamePadState2, new object[] {key});
			//return buttonState == ButtonState.Pressed;
		}
	}

}
