using SpaceWar.Game.Play.Enemy.General;

namespace SpaceWar.Game.Play.Enemy.Enemy2 {

	public class Enemy2Spawner1 : AbstractSpawner {

		public override int MinEnemyCount => 4;
		public override int MaxEnemyCount => 15;
		public override int PointsRequiredForSpawning => 21;
		public override int PointsForKilling => 5;
		public override float SpawnInterval => 2f;

		protected override AbstractEnemy CreateEnemyInstance() {
			return new Enemy2(PointsForKilling);
		}
	}

}
