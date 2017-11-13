using System;
using System.Collections.Generic;
using OpenTK.Input;
using OpenTK.Platform;

namespace Framework.Input {

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

		// TODO Implement playerId check to have multiple players!
		public static bool KeyDown(object action, int playerId = -1) {
			return ACTIVE_ACTIONS.Contains(action);
		}

		// TODO Implement this to have controlls not only in 8 directions! (topleft, top, topright, ...)
		public static float GetAxis() {
			return 0;
		}
	}

}
