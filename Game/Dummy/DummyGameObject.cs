using System.Drawing;
using SpaceWar.Framework;
using SpaceWar.Framework.Render;
using Zenseless.Geometry;

namespace SpaceWar.Game {

	public class DummyGameObject : GameObject {

		public DummyGameObject() {
			AddComponent(new DummyController());
			AddComponent(new RenderTextureComponent(Resources.PLAY_BACKGROUND, 1f, 1f));
			AddComponent(new RenderLineComponent(new Point(-1, -1), new Point(1, 1), Color.White, 10f));
			AddComponent(new RenderLineComponent(new Point(1, -1), new Point(0, 0), Color.GreenYellow, 10f));
		}
	}

}
