using System;
using System.Collections.Generic;
using System.Linq;
using Framework;
using Framework.Object;
using Framework.Utilities;
using OpenTK;

namespace SpaceWar.Game.Play.Enemy.General {

	public abstract class AbstractSpawner : Component, UpdateComponent {

		public static readonly Random POSITION_GENERATOR = new Random();

		// Abstract spawner configuration
		public abstract int MinEnemyCount { get; }
		public abstract int MaxEnemyCount { get; }
		public abstract int PointsRequiredForSpawning { get; }
		public abstract int PointsForKilling { get; }
		public abstract float SpawnDelay { get; }
		public abstract float SpawnInterval { get; }

		// Current spawn wave properties
		public int EnemiesThisWave { get; private set; }
		public int EnemiesToSpawn { get; private set; }
		public float ApproximatedWaveSpawnTime => (EnemiesThisWave + 1) * SpawnInterval;
		public float RemainingSpawnTime => (EnemiesToSpawn + 1) * SpawnInterval;

		public bool IsWaveSpawnFinished => EnemiesToSpawn == 0;
		// TODO public bool IsWaveFinished => ...

		private readonly MyTimer spawnTimer = new MyTimer();

		public override void OnDestroy() {
			spawnTimer.Cancel();
			base.OnDestroy();
		}

		public void Update() {
			spawnTimer.DoEvery(SpawnInterval, MaySpawnEnemy, MyTimer.When.End, true);
		}

		public void StartWave(int enemyCount) {
			EnemiesThisWave = enemyCount;
			EnemiesToSpawn = enemyCount;
		}

		protected abstract AbstractEnemy CreateEnemyInstance();

		protected virtual void SetEnemyPosition(AbstractEnemy enemy) {
			enemy.Transform.WorldPosition = new Vector2(
				POSITION_GENERATOR.NextFloat(-PlayScene.FIELD_WIDTH / 2, PlayScene.FIELD_WIDTH / 2),
				POSITION_GENERATOR.NextFloat(-PlayScene.FIELD_HEIGHT / 2, PlayScene.FIELD_HEIGHT / 2));
		}

		private void MaySpawnEnemy() {
			if (EnemiesToSpawn <= 0) {
				return;
			}
			EnemiesToSpawn--;

			var enemy = CreateEnemyInstance();
			SetEnemyPosition(enemy);
			Scene.Current.Spawn(enemy);
		}
	}

}
