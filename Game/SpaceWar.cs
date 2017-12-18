using Framework;
using Framework.Debug;
using Framework.Object;
using SpaceWar.Game.Menu;
using SpaceWar.Game.Play;

namespace SpaceWar.Game {

	internal static class SpaceWar {

		static void Main() {
			// Configure debug mode
			FrameworkDebug.Enabled = false;
			FrameworkDebug.DrawColliders = false;
			FrameworkDebug.PrintCollisionDetection = true;
			GameDebug.Enabled = false;
			GameDebug.UnDieable = true;
			GameDebug.InitialPoints = 501;

			// Initialize the game
			var game = new Framework.Game();
			game.RegisterInputProvider(new Keymap());
			game.CreatePrimitiveWindow(VierportAnchor.Horizontal, "SpaceWar");
			game.ShowScene(GameDebug.Enabled ? (Scene) new PlayScene() : new MenuScene());
			game.Run();
		}
	}

}
