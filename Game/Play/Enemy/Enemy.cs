using System.Drawing;
using Framework;
using Framework.Collision;
using Framework.Render;

namespace SpaceWar.Game.Play.Enemy {

	public class Enemy : GameObject {

		// Logic constants
		public const float ENEMY_SPAWN_INTERVAL = 10f;
		public const float ENEMY_SPEED = 0.35f;

		// Visual constants
		public const float ENEMY_SIZE = 0.035f;

		public Enemy() {
			AddComponent(new EnemyMovementController());
			AddComponent(new EnemyCollisionController());
			AddComponent(new RenderBoxComponent(ENEMY_SIZE, ENEMY_SIZE).Fill(Color.DarkOrange));
			AddComponent(new BoxCollider(ENEMY_SIZE, ENEMY_SIZE));
		}
	}

}
