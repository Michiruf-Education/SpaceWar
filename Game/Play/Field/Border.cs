using System.Drawing;
using Framework;
using Framework.Collision.Collider;
using Framework.Render;
using OpenTK;

namespace SpaceWar.Game.Play.Field {

	public class Border : GameObject {

		private readonly float x;
		private readonly float y;

		public Border(float x, float y, float width, float height) {
			this.x = x;
			this.y = y;
			AddComponent(new RenderBoxComponent(width, height).Fill(Color.White));
			AddComponent(new BoxCollider(width, height));
		}

		public override void OnStart() {
			base.OnStart();
			Transform.WorldPosition = new Vector2(x, y);
		}
	}

}
