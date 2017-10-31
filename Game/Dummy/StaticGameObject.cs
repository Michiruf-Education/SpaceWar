using System.Drawing;
using SpaceWar.Framework;
using SpaceWar.Framework.Render;

namespace SpaceWar.Game {

	public class StaticGameObject : GameObject {

		public StaticGameObject() {
			AddComponent(new RenderLineComponent(new Point(1, 1), new Point(-1, 1), Color.BlueViolet, 10f));
		}
	}

}
