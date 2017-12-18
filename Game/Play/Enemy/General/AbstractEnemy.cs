using Framework;
using Framework.Utilities;

namespace SpaceWar.Game.Play.Enemy.General {

	public abstract class AbstractEnemy : GameObject {

		public int PointsForKilling { get; }
		public bool IsAlive { get; private set; } = true;
		public bool IsSpawned { get; private set; }

		protected readonly float spawnDelay;
		protected readonly MyTimer spawnDelayTimer = new MyTimer();

		protected AbstractEnemy(float spawnDelay, int pointsForKilling) {
			PointsForKilling = pointsForKilling;
			this.spawnDelay = spawnDelay;
			AddComponent(new EnemyCollisionController());
		}

		public override void OnStart() {
			base.OnStart();
			spawnDelayTimer.DoOnce(spawnDelay, () => {
				IsSpawned = true;
				OnSpawned();
			});
		}

		public virtual void OnSpawned() {
		}

		public override void OnDestroy() {
			IsAlive = false;
			base.OnDestroy();
		}
	}

}
