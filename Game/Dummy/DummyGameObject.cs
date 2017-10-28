using System.Drawing;
using SpaceWar.Framework;
using SpaceWar.Framework.Render;
using Zenseless.Geometry;

namespace SpaceWar.Game {

	public class DummyGameObject : GameObject {

		public DummyGameObject() {
			AddComponent(new DummyUpdateComponent());
			AddComponent(new DummyRenderComponent(Resources.PLAY_BACKGROUND, new Box2D(0, 0, 5f, 5f)));
			AddComponent(new RenderLineComponent(new Point(-1, -1), new Point(0, 1), Color.White, 10f));
		}
	}

}
