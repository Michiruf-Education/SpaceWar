using System.Drawing;
using Framework;
using Framework.Collision;
using Framework.Render;
using OpenTK;
using Zenseless.Geometry;

namespace SpaceWar.Game.Play.Field {

	public class Border : GameObject {

		public Border(float x, float y, float width, float height) {
			AddComponent(new RenderBoxComponent(width, height).Fill(Color.White));
			AddComponent(new UnrotateableBoxCollider(new Box2D(-width / 2, -height / 2, width, height)));
			Transform.WorldPosition = new Vector2(x, y);
		}
	}

}
