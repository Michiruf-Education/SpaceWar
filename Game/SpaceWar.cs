namespace SpaceWar.Game {

	internal static class SpaceWar {

		static void Main(string[] args) {
			var game = new Framework.Game();
			game.CreatePrimitiveWindow();
			game.ShowScene(new DummyScene());
			game.Run();
		}
	}

}
