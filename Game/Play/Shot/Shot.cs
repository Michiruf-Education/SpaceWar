using System.Drawing;
using Framework;
using Framework.Collision;
using Framework.Render;
using OpenTK;
using Zenseless.Geometry;

namespace SpaceWar.Game.Play.Shot {

	public class Shot : GameObject {

		// Logic constants
		public const float SHOT_SPEED = 0.5f;

		// Visual constants
		public const float SHOT_SIZE = 0.01f;

		public Shot(float direction, Vector2 position) {
			AddComponent(new ShotMovementController(direction));
			AddComponent(new ShotCollisionController());
			AddComponent(new RenderBoxComponent(0.01f, 0.01f).Fill(Color.Brown));
			AddComponent(new UnrotateableBoxCollider(new Box2D(-SHOT_SIZE / 2, -SHOT_SIZE / 2,
				SHOT_SIZE, SHOT_SIZE)));
			Transform.WorldPosition = position;
		}
	}

}
