using SpaceWar.Framework.Debug;

namespace SpaceWar.Game {

	internal static class SpaceWar {

		static void Main(string[] args) {
			FrameworkDebugMode.IsEnabled = true;

			var game = new Framework.Game();
			game.RegisterInputProvider(new Keymap());
			game.CreatePrimitiveWindow();
			game.ShowScene(new DummyScene());
			game.Run();
		}
	}

}
