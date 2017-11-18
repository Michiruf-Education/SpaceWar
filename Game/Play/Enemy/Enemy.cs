using System.Drawing;
using Framework;
using Framework.Collision;
using Framework.Render;
using Zenseless.Geometry;

namespace SpaceWar.Game.Play.Enemy {

	public class Enemy : GameObject {

		// Logic constants
		public const float ENEMY_SPAWN_INTERVAL = 10f;
		public const float ENEMY_SPEED = 0.4f;

		// Visual constants
		public const float ENEMY_SIZE = 0.035f;

		public Enemy() {
			AddComponent(new EnemyMovementController());
			AddComponent(new RenderBoxComponent(ENEMY_SIZE, ENEMY_SIZE).Fill(Color.DarkOrange));
			AddComponent(new UnrotateableBoxCollider(new Box2D(-ENEMY_SIZE / 2, -ENEMY_SIZE / 2,
				ENEMY_SIZE, ENEMY_SIZE)));
		}
	}

}
