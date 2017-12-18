using SpaceWar.Game.Play.Enemy.General;

namespace SpaceWar.Game.Play.Enemy.Enemy2 {

	public class Enemy2Spawner0 : AbstractSpawner {

		public override int MinEnemyCount => 10;
		public override int MaxEnemyCount => 10;
		public override int PointsRequiredForSpawning => 5;
		public override int PointsForKilling => 5;
		public override float SpawnDelay => 1f;
		public override float SpawnInterval => 2f;

		protected override AbstractEnemy CreateEnemyInstance() {
			return new Enemy2(this);
		}
	}

}
