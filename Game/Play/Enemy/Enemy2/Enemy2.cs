using System.Drawing;
using Framework.Collision.Collider;
using Framework.Render;
using SpaceWar.Game.Play.Enemy.General;
using SpaceWar.Resources;

namespace SpaceWar.Game.Play.Enemy.Enemy2 {

	public class Enemy2 : AbstractEnemy {

		// Logic constants
		public const float ENEMY_SPEED = 0.35f;

		// Visual constants
		public const float ENEMY_SIZE = 0.042f;

		// Components for spawn changes
		private readonly RenderTextureComponent visual;

		public Enemy2(AbstractSpawner spawner) : base(spawner) {
			AddComponent(new EnemyLinearFollowNearestPlayerMovementController(ENEMY_SPEED));
			AddComponent(new EnemyNoOverlapCollisionController());
			AddComponent(visual = new RenderTextureComponent(Resource.Enemy2, ENEMY_SIZE, ENEMY_SIZE).SetColorFilter(Color.FromArgb(150, Color.Aqua)));
			AddComponent(new CircleCollider(ENEMY_SIZE / 3f));
		}

		public override void OnSpawned() {
			base.OnSpawned();
			visual.SetColorFilter(Color.Aqua);
		}
	}

}
