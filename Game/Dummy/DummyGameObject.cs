using System.Drawing;
using SpaceWar.Framework;
using SpaceWar.Framework.Render;
using Zenseless.Geometry;

namespace SpaceWar.Game {

	public class DummyGameObject : GameObject {

		public DummyGameObject() {
			AddComponent(new DummyController());
			AddComponent(new RenderTextureComponent(Resources.background, 0.2f, 0.2f));
			AddComponent(new RenderLineComponent(new PointF(-0.2f, -0.2f), new PointF(0.2f, 0.2f), Color.White, 2f));
			AddComponent(new RenderLineComponent(new PointF(0.2f, -0.2f), new PointF(0f, 0f), Color.GreenYellow, 2f));
		}
	}

}
