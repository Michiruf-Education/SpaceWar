using System.Drawing;
using Framework.Collision.Collider;
using Framework.Render;
using SpaceWar.Game.Play.Enemy.General;

namespace SpaceWar.Game.Play.Enemy.Enemy2 {

	public class Enemy2 : AbstractEnemy {

		// Logic constants
		public const float SPAWN_DELAY = 1f;
		public const float ENEMY_SPEED = 0.35f;

		// Visual constants
		public const float ENEMY_SIZE = 0.05f;

		// Components for spawn changes
		private readonly RenderBoxComponent visual;

		public Enemy2(int pointsForKilling) : base(SPAWN_DELAY, pointsForKilling) {
			AddComponent(new EnemyLinearFollowNearestPlayerMovementController(ENEMY_SPEED));
			AddComponent(new EnemyNoOverlapCollisionController());
			AddComponent(visual = new RenderBoxComponent(ENEMY_SIZE, ENEMY_SIZE).Fill(Color.FromArgb(150, Color.Aqua)));
			AddComponent(new BoxCollider(ENEMY_SIZE, ENEMY_SIZE));
		}

		public override void OnSpawned() {
			base.OnSpawned();
			visual.Fill(Color.Aqua);
		}
	}

}
