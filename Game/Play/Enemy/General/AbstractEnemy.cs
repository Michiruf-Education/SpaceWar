using Framework;
using Framework.Utilities;

namespace SpaceWar.Game.Play.Enemy.General {

	public abstract class AbstractEnemy : GameObject {

		public int PointsForKilling { get; }
		public bool IsAlive { get; private set; } = true;
		public bool IsSpawned { get; private set; }

		protected readonly float spawnDelay;
		protected readonly LimitedRateTimer spawnDelayTimer = new LimitedRateTimer();

		protected AbstractEnemy(float spawnDelay, int pointsForKilling) {
			PointsForKilling = pointsForKilling;
			this.spawnDelay = spawnDelay;
			AddComponent(new EnemyCollisionController());
		}

		private bool firstTime = true;
		public override void Update() {
			base.Update();
			// TODO Better timers...
			if (!IsSpawned) {
				spawnDelayTimer.DoOnlyEvery(spawnDelay, () => {
					if (firstTime) {
						firstTime = false;
						return;
					}
					IsSpawned = true;
					OnSpawned();
				});
			}
		}

		public virtual void OnSpawned() {
		}

		public override void OnDestroy() {
			IsAlive = false;
			base.OnDestroy();
		}
	}

}
