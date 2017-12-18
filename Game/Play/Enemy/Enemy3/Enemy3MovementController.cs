using Framework;
using Framework.Object;
using SpaceWar.Game.Play.Enemy.General;

namespace SpaceWar.Game.Play.Enemy.Enemy3 {

	public class Enemy3MovementController : Component, UpdateComponent {

		private readonly float speed;

		private AbstractEnemy enemy;

		public Enemy3MovementController(float speed) {
			this.speed = speed;
		}

		public override void OnStart() {
			base.OnStart();
			enemy = GameObject as AbstractEnemy;
		}

		public void Update() {
			throw new System.NotImplementedException();
		}
	}

}
