using SpaceWar.Framework;

namespace SpaceWar.Game {

	public class DummyScene : Scene {

		public DummyScene() {
			Spawn(new DummyGameObject());
		}
	}

}
