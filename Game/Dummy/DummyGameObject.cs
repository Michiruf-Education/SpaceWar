using SpaceWar.Framework;

namespace SpaceWar.Game {

	public class DummyGameObject : GameObject {

		public DummyGameObject() {
			AddComponent(new DummyUpdateComponent());
			AddComponent(new DummyRenderComponent());
		}
	}

}
