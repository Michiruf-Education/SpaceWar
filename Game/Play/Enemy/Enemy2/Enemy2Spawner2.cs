using SpaceWar.Game.Play.Enemy.General;

namespace SpaceWar.Game.Play.Enemy.Enemy2 {

	public class Enemy2Spawner2 : AbstractSpawner {

		public override int MinEnemyCount => 40;
		public override int MaxEnemyCount => 40;
		public override int PointsRequiredForSpawning => 101;
		public override int PointsForKilling => 10;
		public override float SpawnInterval => 0.05f;

		protected override AbstractEnemy CreateEnemyInstance() {
			return new Enemy2(PointsForKilling);
		}
	}

}
