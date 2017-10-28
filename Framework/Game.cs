using System;
using OpenTK;
using OpenTK.Platform;
using Zenseless.ShaderDebugging;

namespace SpaceWar.Framework {

	public class Game {

		public GameWindow Window { get; private set; }
		public Scene ActiveScene { get; private set; }

		public Game() {
			GameContainer.SetGame(this);
		}

		public void CreatePrimitiveWindow() {
			Window = new GameWindow();
			LoadLayoutAndRegisterSaveHook(Window);
			RegisterWindowSceneIndirections(Window);
		}

		public void ShowScene(Scene newScene) {
			ActiveScene = newScene;
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

			gameWindow.UpdateFrame += (e1, e2) => ActiveScene?.Update();
			gameWindow.RenderFrame += (e1, e2) => {
				ActiveScene?.Render();
				// Swapping buffers shows the rendered stuff
				Window.SwapBuffers();
			};
		}
	}

}
