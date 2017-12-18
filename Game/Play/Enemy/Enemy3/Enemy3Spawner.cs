using SpaceWar.Game.Play.Enemy.General;

namespace SpaceWar.Game.Play.Enemy.Enemy3 {

	public class Enemy3Spawner : AbstractSpawner {

		public override int MinEnemyCount => 1;
		public override int MaxEnemyCount => 4;
		public override int PointsRequiredForSpawning => 501;
		public override int PointsForKilling => 20;
		public override float SpawnDelay => 1f;
		public override float SpawnInterval => 5f;

		protected override AbstractEnemy CreateEnemyInstance() {
			return new Enemy3(this);
		}
	}

}
