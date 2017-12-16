using System;
using Framework.Input;
using Framework.Object;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using Zenseless.ShaderDebugging;

namespace Framework {

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

		public void CreatePrimitiveWindow(VierportAnchor anchor, string title = null) {
			Window = new GameWindow {Title = title};
			SetupInputHandler();
			LoadLayoutAndRegisterSaveHook();
			InitializeResizeHandler(anchor);
			RegisterWindowSceneIndirections();
		}

		public void ShowScene(Scene newScene) {
			// Delegate destroying the old scene
			ActiveScene?.OnDestroy();

			ActiveScene = newScene;
			ActiveScene.OnStart();
		}

		public void Run() {
			Time.SetStartTimeIfNotSetYet();
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
				ActiveScene?.OnDestroy();
				Window.SaveLayout();
			};
		}

		void InitializeResizeHandler(VierportAnchor anchor) {
			Window.Resize += (sender, args) => {
				GL.Viewport(0, 0, Window.Width, Window.Height);
				GL.LoadIdentity();
				switch (anchor) {
					case VierportAnchor.Horizontal:
						var aspectH = Window.Width / (float) Window.Height;
						GL.Scale(1, aspectH, 1);
						break;
					case VierportAnchor.Vertical:
						var aspectV = Window.Height / (float) Window.Width;
						GL.Scale(aspectV, 1, 1);
						break;
					default:
						throw new ArgumentException("Invalid " + typeof(VierportAnchor).Name);
				}
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
