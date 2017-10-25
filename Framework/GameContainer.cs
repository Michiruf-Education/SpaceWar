using System;

namespace SpaceWar.Framework {

	public class GameContainer {

		// TODO Static indirections for the game class!
		// TODO May be for the active scene class?
		
		private static Game game;
		
		public static void SetGame(Game game) {
			GameContainer.game = game;
		}

		private void Validate() {
			if (game == null) {
				throw new InvalidOperationException("GameContainer does not container a game instance!");
			}
		}
	}

}
