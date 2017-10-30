using System;
using System.Collections.Generic;
using OpenTK;
using OpenTK.Input;

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
				if (input.Key is Key key) {
					RegisterInput(key, input.Value);
				} else if (input.Key is Buttons button) {
					RegisterInput(button, input.Value);
				} else if (input.Key is MouseButton mouseButton) {
					RegisterInput(mouseButton, input.Value);
				}
			}
		}

		static void RegisterInput(Key key, object action) {
			KEYBOARD_MAP.Add(key, action);
		}

		static void RegisterInput(Buttons button, object action) {
			CONTROLLER_MAP.Add(button, action);
		}

		static void RegisterInput(MouseButton button, object action) {
			MOUSE_MAP.Add(button, action);
		}

		public static void RegisterWindow(GameWindow gameWindow) {
			gameWindow.KeyDown += (sender, args) => {
				if (KEYBOARD_MAP.TryGetValue(args.Key, out var key)) {
					ACTIVE_ACTIONS.Add(key);
				}
			};
			gameWindow.KeyUp += (sender, args) => {
				if (KEYBOARD_MAP.TryGetValue(args.Key, out var key)) {
					ACTIVE_ACTIONS.Remove(key);
				}
			};
		}

		public static bool KeyDown(object action) {
			return ACTIVE_ACTIONS.Contains(action);
		}
	}

}
