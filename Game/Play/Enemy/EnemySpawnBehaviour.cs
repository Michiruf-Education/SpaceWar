using Framework;
using Framework.Utilities;

namespace SpaceWar.Game.Play.Enemy {

	public class EnemySpawnBehaviour : GameObject {

		private bool started;
		private readonly DelayTimer spawnStartTimer = new DelayTimer();
		private readonly LimitedRateTimer spawnTimer = new LimitedRateTimer();

		public override void Update() {
			base.Update();
			spawnStartTimer.DoOnlyOnce(Enemy.ENEMY_SPAWN_INTERVAL, () => started = true);
			spawnTimer.DoOnlyEvery(Enemy.ENEMY_SPAWN_INTERVAL, SpawnEnemies);
		}

		void SpawnEnemies() {
			if (!started) {
				return;
			}

			Scene.Current.Spawn(new Enemy());
		}
	}

}
