namespace SpaceWar.Game.Scene {

	public class PlayScene : Framework.Scene {

		public PlayScene() {
			Spawn(new Player.Player());
		}
	}

}
