using SpaceWar.Framework;

namespace SpaceWar.Game {

	public class DummyScene : Scene {

		public DummyScene() {
			Spawn(new CameraGameObject());
			Spawn(new DummyGameObject());
			Spawn(new StaticGameObject());
		}
	}

}
