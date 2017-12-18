using Framework.Debug;
using Framework.Object;

namespace SpaceWar.Game {

	internal static class SpaceWar {

		static void Main() {
			// Configure debug mode
			FrameworkDebug.Enabled = true;
			FrameworkDebug.DrawColliders = false;
			FrameworkDebug.PrintCollisionDetection = true;

			// Initialize the game
			var game = new Framework.Game();
			game.RegisterInputProvider(new Keymap());
			game.CreatePrimitiveWindow(VierportAnchor.Horizontal, "SpaceWar");
//			game.ShowScene(new Menu.MenuScene());
			game.ShowScene(new Play.PlayScene());
			game.Run();
		}
	}

}
