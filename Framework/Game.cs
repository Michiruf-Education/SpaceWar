using System;
using OpenTK;
using OpenTK.Platform;
using Zenseless.ShaderDebugging;

namespace SpaceWar.Framework {

	public class Game {

		public static Game Instance { get; private set; }

		public GameWindow Window { get; private set; }
		public Scene ActiveScene { get; private set; }

		public Game() {
			if (Instance != null) {
				// For now we throw a exception, but later we may implement a destroy call to tear 
				// down the current game and start over
				// Eventual use-case: graphic changes that need a restart of the game
				throw new Exception("Only one game may exist!");
			}
			Instance = this;
		}

		public void CreatePrimitiveWindow() {
			Window = new GameWindow();
			LoadLayoutAndRegisterSaveHook(Window);
			RegisterWindowSceneIndirections(Window);
		}

		public void ShowScene(Scene newScene) {
			// Delegate destroying the old scene
			ActiveScene?.Lifecycle?.onDestroy?.Invoke();

			ActiveScene = newScene;
			ActiveScene?.Lifecycle?.onCreate?.Invoke();
		}

		public void Run() {
			Window.Run();
		}

		void LoadLayoutAndRegisterSaveHook(GameWindow gameWindow) {
			// TODO Inform Daniel Scherzer that this is able to do with IGameWindow instead of GameWindow
			gameWindow.LoadLayout();
			gameWindow.Closing += (sender, args) => {
				ActiveScene?.Lifecycle?.onDestroy?.Invoke();
				gameWindow.SaveLayout();
			};
		}

		void RegisterWindowSceneIndirections(IGameWindow gameWindow) {
			if (gameWindow == null) {
				throw new ArgumentNullException(nameof(gameWindow));
			}

			gameWindow.UpdateFrame += (e1, e2) => ActiveScene?.Update();
			gameWindow.RenderFrame += (e1, e2) => {
				ActiveScene?.Render();
				// Swapping buffers shows the rendered stuff
				// Without we will not see any affects by our GL drawings
				Window.SwapBuffers();
			};
		}
	}

}
