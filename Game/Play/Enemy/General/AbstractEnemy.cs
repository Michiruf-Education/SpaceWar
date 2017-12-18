using Framework;
using Framework.Utilities;

namespace SpaceWar.Game.Play.Enemy.General {

	public abstract class AbstractEnemy : GameObject {

		public bool IsAlive { get; private set; } = true;
		public bool IsSpawned { get; private set; }
		public int PointsForKilling => spawner.PointsForKilling;

		protected readonly AbstractSpawner spawner;
		protected readonly MyTimer spawnDelayTimer = new MyTimer();

		protected AbstractEnemy(AbstractSpawner spawner) {
			this.spawner = spawner;
			AddComponent(new EnemyCollisionController());
		}

		public override void OnStart() {
			base.OnStart();
			spawnDelayTimer.DoOnce(spawner.SpawnDelay, () => {
				IsSpawned = true;
				OnSpawned();
			});
		}

		public override void OnDestroy() {
			IsAlive = false;
			base.OnDestroy();
		}

		public virtual void OnSpawned() {
		}
	}

}
