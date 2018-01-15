using System.Drawing;
using Framework.Collision.Collider;
using Framework.Render;
using SpaceWar.Game.Play.Enemy.General;
using SpaceWar.Resources;

namespace SpaceWar.Game.Play.Enemy.Enemy3 {

	public class Enemy3 : AbstractEnemy {

		// Logic constants
		public const float ENEMY_SPEED = 1.2f;
		public const float ENEMY_REST_DURATION = 0.5f;
		public const float ENEMY_CHASE_DURATION = 0.8f;

		// Visual constants
		public const float ENEMY_SIZE = 0.05f;

		// Components for spawn changes
		private readonly RenderTextureComponent visual;

		public Enemy3(AbstractSpawner spawner) : base(spawner) {
			AddComponent(new Enemy3MovementController(ENEMY_SPEED, ENEMY_REST_DURATION, ENEMY_CHASE_DURATION));
			AddComponent(visual = new RenderTextureComponent(Resource.Enemy3, ENEMY_SIZE, ENEMY_SIZE)
				.SetColorFilter(Color.FromArgb(150, Color.Red)));
			AddComponent(new CircleCollider(ENEMY_SIZE / 2.8f));
		}

		public override void OnSpawned() {
			base.OnSpawned();
			visual.SetColorFilter(Color.Red);
		}
	}

}
