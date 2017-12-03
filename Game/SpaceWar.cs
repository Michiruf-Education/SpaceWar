using Framework.Debug;
using SpaceWar.Game.Play;

namespace SpaceWar.Game {

	internal static class SpaceWar {

		static void Main(string[] args) {
			FrameworkDebugMode.IsEnabled = true;

			var game = new Framework.Game();
			game.RegisterInputProvider(new Keymap());
			game.CreatePrimitiveWindow("SpaceWar");
			game.ShowScene(new PlayScene());
			game.Run();
		}
	}

}
