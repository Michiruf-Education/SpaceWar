using Framework;

namespace SpaceWar.Game.Player {

	public class Player : GameObject {

		public Player() {
			AddComponent(new PlayerFollowingCameraController());
			AddComponent(new PlayerController());
		}
	}

}
