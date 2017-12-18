using SpaceWar.Game.Play.Enemy.Calculator;
using SpaceWar.Game.Play.Enemy.General;

namespace SpaceWar.Game.Play.Enemy.Enemy2 {

	public class Enemy2Spawner2 : AbstractSpawner {

		public override int MinEnemyCount => 30;
		public override int MaxEnemyCount => 30;
		public override int PointsRequiredForSpawning => 101;
		public override int PointsForKilling => 10;
		public override float SpawnDelay => 1f;
		public override float SpawnInterval => 0.05f;

		protected override AbstractEnemy CreateEnemyInstance() {
			return new Enemy2(this);
		}

		protected override void SetEnemyPosition(AbstractEnemy enemy) {
			enemy.Transform.WorldPosition = EdgeSpawnPositionCalculator.RandomEdgePosition(
				PlayScene.FIELD_WIDTH, PlayScene.FIELD_HEIGHT);
		}
	}

}
