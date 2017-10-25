using SpaceWar.Framework;

namespace SpaceWar.Game {

	public class DummyGameObject : GameObject {

		public DummyGameObject() {
			Components.Add(new DummyComponent());
		}
	}

}
