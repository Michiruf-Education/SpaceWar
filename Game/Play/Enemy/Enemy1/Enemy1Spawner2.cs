using SpaceWar.Game.Play.Enemy.General;

namespace SpaceWar.Game.Play.Enemy.Enemy1 {

	public class Enemy1Spawner2 : AbstractSpawner {

		public override int MinEnemyCount => 60;
		public override int MaxEnemyCount => 60;
		public override int PointsRequiredForSpawning => 6;
		public override int PointsForKilling => 5;
		public override float SpawnInterval => 0.02f;

		protected override AbstractEnemy CreateEnemyInstance() {
			return new Enemy1(PointsForKilling);
		}
	}

}
