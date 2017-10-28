using System;

namespace SpaceWar.Framework {

	public class GameContainer {

		// TODO Static indirections for the game class!
		// TODO May be for the active scene class?

		private static Game GAME;

		public static void SetGame(Game game) {
			GAME = game;
		}

		private static void Validate() {
			if (GAME == null) {
				throw new InvalidOperationException("GameContainer does not container a game instance!");
			}
		}
	}

}
