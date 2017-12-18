using System.Drawing;
using Framework.Collision.Collider;
using Framework.Render;
using SpaceWar.Game.Play.Enemy.General;

namespace SpaceWar.Game.Play.Enemy.Enemy3 {

	public class Enemy3 : AbstractEnemy {

		// Logic constants
		public const float SPAWN_DELAY = 1f;
		public const float ENEMY_SPEED = 0.35f;

		// Visual constants
		public const float ENEMY_SIZE = 0.05f;

		// Components for spawn changes
		private readonly RenderBoxComponent visual;

		public Enemy3(int pointsForKilling) : base(SPAWN_DELAY, pointsForKilling) {
			AddComponent(new Enemy3MovementController(ENEMY_SPEED));
			AddComponent(visual = new RenderBoxComponent(ENEMY_SIZE, ENEMY_SIZE).Fill(Color.FromArgb(150, Color.Red)));
			AddComponent(new BoxCollider(ENEMY_SIZE, ENEMY_SIZE));
		}

		public override void OnSpawned() {
			base.OnSpawned();
			visual.Fill(Color.Red);
		}
	}

}
