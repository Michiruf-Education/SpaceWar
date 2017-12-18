using SpaceWar.Game.Play.Enemy.Calculator;
using SpaceWar.Game.Play.Enemy.General;

namespace SpaceWar.Game.Play.Enemy.Enemy1 {

	public class Enemy1Spawner2 : AbstractSpawner {

		public override int MinEnemyCount => 60;
		public override int MaxEnemyCount => 60;
		public override int PointsRequiredForSpawning => 6;
		public override int PointsForKilling => 1;
		public override float SpawnDelay => 1f;
		public override float SpawnInterval => 0.05f;

		protected override AbstractEnemy CreateEnemyInstance() {
			return new Enemy1(this);
		}

		protected override void SetEnemyPosition(AbstractEnemy enemy) {
			enemy.Transform.WorldPosition = EdgeSpawnPositionCalculator.RandomEdgePosition(
				PlayScene.FIELD_WIDTH, PlayScene.FIELD_HEIGHT);
		}
	}

}
