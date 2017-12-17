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

		// Spawner configuration
		public abstract int MinEnemyCount { get; }
		public abstract int MaxEnemyCount { get; }
		public abstract int PointsRequiredForSpawning { get; }
		public abstract int PointsForKilling { get; }
		public abstract float SpawnInterval { get; }

		// Current spawn properties
		public int EnemiesThisWave { get; private set; }
		public int EnemiesToSpawn { get; private set; }
		public float ApproximatedWaveSpawnTime => (EnemiesThisWave + 1) * SpawnInterval;
		public float RemainingSpawnTime => (EnemiesToSpawn + 1) * SpawnInterval;
		public List<AbstractEnemy> SpawnedEnemies { get; } = new List<AbstractEnemy>();

		public bool IsWaveFinished => EnemiesToSpawn == 0 &&
		                              SpawnedEnemies.Aggregate(0, (i, enemy) => enemy.IsAlive ? i + 1 : i) == 0;

		private readonly LimitedRateTimer spawnTimer = new LimitedRateTimer();

		// Behaviour for parameter lookup
		private EnemySpawnBehaviour Behaviour => GameObject as EnemySpawnBehaviour;

		public void StartWave(int enemyCount) {
			SpawnedEnemies.Clear();
			EnemiesThisWave = enemyCount;
			EnemiesToSpawn = enemyCount;
		}

		public void Update() {
			spawnTimer.DoOnlyEvery(SpawnInterval, SpawnEnemy);
		}

		protected abstract AbstractEnemy CreateEnemyInstance();

		protected virtual void SetEnemyPosition(AbstractEnemy enemy) {
			enemy.Transform.WorldPosition = new Vector2(
				POSITION_GENERATOR.NextFloat(-Behaviour.FieldWidth / 2, Behaviour.FieldWidth / 2),
				POSITION_GENERATOR.NextFloat(-Behaviour.FieldHeight / 2, Behaviour.FieldHeight / 2));
		}

		private void SpawnEnemy() {
			if (EnemiesToSpawn <= 0) {
				return;
			}
			EnemiesToSpawn--;

			var enemy = CreateEnemyInstance();
			SetEnemyPosition(enemy);
			SpawnedEnemies.Add(enemy);
			Scene.Current.Spawn(enemy);
		}
	}

}
