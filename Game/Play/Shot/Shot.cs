using System.Drawing;
using Framework;
using Framework.Render;

namespace SpaceWar.Game.Play.Shot {

	public class Shot : GameObject {
		
		public const float SHOT_SPEED = 0.1f;

		public Shot(float direction) {
			AddComponent(new ShotMovementController(direction));
			AddComponent(new ShotCollisionController());
			AddComponent(new RenderBoxComponent(0.01f, 0.01f).Fill(Color.Brown));
		}
	}

}
