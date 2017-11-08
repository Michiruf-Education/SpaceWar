using System;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using OpenTK.Platform;
using SpaceWar.Framework.Camera;
using SpaceWar.Framework.Input;
using SpaceWar.Framework.Object;
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

		public void RegisterInputProvider(InputProvider inputProvider) {
			var inputs = inputProvider.LoadInputs();
			InputHandler.Init();
			InputHandler.RegisterInputs(inputs);
		}

		public void CreatePrimitiveWindow() {
			Window = new GameWindow();
			SetupInputHandler();
			LoadLayoutAndRegisterSaveHook();
			InitializeResizeHandler();
			RegisterWindowSceneIndirections();
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

		void SetupInputHandler() {
			InputHandler.RegisterWindow(Window);
		}

		void LoadLayoutAndRegisterSaveHook() {
			// TODO Inform Daniel Scherzer that this is able to do with IGameWindow instead of GameWindow
			// -> The LoadLayout extension
			Window.LoadLayout();
			Window.Closing += (sender, args) => {
				ActiveScene?.Lifecycle?.onDestroy?.Invoke();
				Window.SaveLayout();
			};
		}

		void InitializeResizeHandler() {
			Window.Resize += (sender, args) => {
				GL.Viewport(0, 0, Window.Width, Window.Height);
				var aspect = Window.Width / (float) Window.Height;
				GL.LoadIdentity();
				GL.Scale(1, aspect, 1);
			};
		}

		void RegisterWindowSceneIndirections() {
			Window.UpdateFrame += (e1, e2) => {
				Time.DeltaTime = (float) e2.Time;
				ActiveScene?.Update();
			};
			Window.RenderFrame += (e1, e2) => {
				// Clear last frames drawings
				GL.Clear(ClearBufferMask.ColorBufferBit);
				// Render current frame
				ActiveScene?.Render();
				// Swapping buffers shows the rendered stuff
				// Without we will not see any affects by our GL drawings
				Window.SwapBuffers();
			};
		}
	}

}
