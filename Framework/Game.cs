using System;
using OpenTK;
using OpenTK.Platform;

namespace SpaceWar.Framework {

	public class Game {

		public GameWindow Window { get; private set; }
		public Scene ActiveScene { get; private set; }

		public Game() {
			GameContainer.SetGame(this);
		}

		public void CreatePrimitiveWindow() {
			Window = new GameWindow();
			RegisterWindowSceneIndirections(Window);
		}

		public void ShowScene(Scene newScene) {
			ActiveScene = newScene;
		}

		public void Run() {
			Window.Run();
		}

		void RegisterWindowSceneIndirections(IGameWindow gameWindow) {
			if (gameWindow == null) {
				throw new ArgumentNullException(nameof(gameWindow));
			}

			gameWindow.UpdateFrame += (e1, e2) => { ActiveScene?.Update(); };
			gameWindow.RenderFrame += (e1, e2) => { ActiveScene?.Render(); };
		}
	}

}
