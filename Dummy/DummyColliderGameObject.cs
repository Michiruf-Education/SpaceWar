using Framework;
using Framework.Collision;
using Zenseless.Geometry;

namespace SpaceWar.Game {

	public class DummyColliderGameObject : GameObject {

		public DummyColliderGameObject() {
			AddComponent(new UnrotateableBoxCollider(new Box2D(-0.1f, -0.1f, 0.2f, 0.2f)));
		}
	}

}
