using SpaceWar.Game.Play.Enemy.General;

namespace SpaceWar.Game.Play.Enemy.Enemy1 {

	public class Enemy1Spawner1 : AbstractSpawner {

		public override int MinEnemyCount => 1;
		public override int MaxEnemyCount => 20;
		public override int PointsRequiredForSpawning => 1;
		public override int PointsForKilling => 1;
		public override float SpawnDelay => 1f;
		public override float SpawnInterval => EnemiesThisWave < 10 ? 2f : 1f;

		protected override AbstractEnemy CreateEnemyInstance() {
			return new Enemy1(this);
		}
	}

}
