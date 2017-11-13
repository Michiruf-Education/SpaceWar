using Framework.Debug;
using SpaceWar.Game.Scene;

namespace SpaceWar.Game {

	internal static class SpaceWar {

		static void Main(string[] args) {
			FrameworkDebugMode.IsEnabled = true;

			var game = new Framework.Game();
			game.RegisterInputProvider(new Keymap());
			game.CreatePrimitiveWindow();
			//game.ShowScene(new DummyScene());
			game.ShowScene(new PlayScene());
			game.Run();
		}
	}

}
