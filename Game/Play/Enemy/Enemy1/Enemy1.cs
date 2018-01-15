using System.Drawing;
using System.Windows.Forms;
using Framework.Collision.Collider;
using Framework.Render;
using SpaceWar.Game.Play.Enemy.General;
using SpaceWar.Resources;

namespace SpaceWar.Game.Play.Enemy.Enemy1 {

	public class Enemy1 : AbstractEnemy {

		// Logic constants
		public const float ENEMY_SPEED = 0.2f;

		// Visual constants
		public const float ENEMY_SIZE = 0.035f;

		// Components for spawn changes
		private readonly RenderTextureComponent visual;

		public Enemy1(AbstractSpawner spawner) : base(spawner) {
			AddComponent(new EnemyLinearFollowNearestPlayerMovementController(ENEMY_SPEED));
			AddComponent(new EnemyNoOverlapCollisionController());
			AddComponent(visual = new RenderTextureComponent(Resource.Enemy1, ENEMY_SIZE, ENEMY_SIZE)
				.SetColorFilter(Color.FromArgb(150, Color.DarkOrange)));
			AddComponent(new BoxCollider(ENEMY_SIZE, ENEMY_SIZE));
		}

		public override void OnSpawned() {
			base.OnSpawned();
			visual.SetColorFilter(Color.DarkOrange);
		}
	}

}
