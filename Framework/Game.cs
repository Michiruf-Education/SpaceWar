using System;
using OpenTK;
using OpenTK.Platform;
using Zenseless.ShaderDebugging;

namespace SpaceWar.Framework {

	public class Game {

		public static Game Instance { get; private set; }
		public static Scene ActiveScene => Instance.ActiveSceneInstance;

		public GameWindow Window { get; private set; }
		public Scene ActiveSceneInstance { get; private set; }

		public Game() {
			Instance = this;
		}

		public void CreatePrimitiveWindow() {
			Window = new GameWindow();
			LoadLayoutAndRegisterSaveHook(Window);
			RegisterWindowSceneIndirections(Window);
		}

		public void ShowScene(Scene newScene) {
			ActiveSceneInstance = newScene;
		}

		public void Run() {
			Window.Run();
		}

		static void LoadLayoutAndRegisterSaveHook(GameWindow gameWindow) {
			// TODO Inform Daniel Scherzer that this is able to do with IGameWindow instead of GameWindow
			gameWindow.LoadLayout();
			gameWindow.Closing += (sender, args) => gameWindow.SaveLayout();
		}

		void RegisterWindowSceneIndirections(IGameWindow gameWindow) {
			if (gameWindow == null) {
				throw new ArgumentNullException(nameof(gameWindow));
			}

			gameWindow.UpdateFrame += (e1, e2) => ActiveSceneInstance?.Update();
			gameWindow.RenderFrame += (e1, e2) => {
				ActiveSceneInstance?.Render();
				// Swapping buffers shows the rendered stuff
				// Without we will not see any affects by our GL drawings
				Window.SwapBuffers();
			};
		}
	}

}
