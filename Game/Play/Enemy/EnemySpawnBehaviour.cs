using System;
using Framework;
using Framework.Utilities;
using OpenTK;
using PlayerT = SpaceWar.Game.Play.Player.Player;

namespace SpaceWar.Game.Play.Enemy {

	public class EnemySpawnBehaviour : GameObject {

		private readonly LimitedRateTimer spawnTimer = new LimitedRateTimer();

		private PlayerT player;
		private bool started;

		public override void OnStart() {
			base.OnStart();
			player = Scene.Current.GetGameObject<PlayerT>();
		}

		public override void Update() {
			base.Update();
			spawnTimer.DoOnlyEvery(Enemy.ENEMY_SPAWN_INTERVAL, SpawnEnemies);
		}

		void SpawnEnemies() {
			// Skip the first call to this method because we intially want to wait some time
			// before the enemies are spawned
			if (!started) {
				started = true;
				return;
			}

			var enemyCount = SpawnEnemyCountFunction(player.Attributes.Points);
			var random = new Random();

			for (var i = 0; i < enemyCount; i++) {
				var enemy1 = new Enemy();
				enemy1.Transform.WorldPosition = new Vector2(
					1f - (float) random.NextDouble()/ 1.5f - 0.05f,
					0.5f - (float) random.NextDouble()/ 1.5f - 0.05f
				);
				Scene.Current.Spawn(enemy1);

				var enemy2 = new Enemy();
				enemy2.Transform.WorldPosition = new Vector2(
					1f - (float) random.NextDouble()/ 1.5f - 0.05f,
					-0.5f + (float) random.NextDouble()/ 1.5f + 0.05f
				);
				Scene.Current.Spawn(enemy2);

				var enemy3 = new Enemy();
				enemy3.Transform.WorldPosition = new Vector2(
					-1f + (float) random.NextDouble()/ 1.5f + 0.05f,
					-0.5f + (float) random.NextDouble()/ 1.5f + 0.05f
				);
				Scene.Current.Spawn(enemy3);

				var enemy4 = new Enemy();
				enemy4.Transform.WorldPosition = new Vector2(
					-1f + (float) random.NextDouble()/ 1.5f + 0.05f,
					0.5f - (float) random.NextDouble()/ 1.5f - 0.05f
				);
				Scene.Current.Spawn(enemy4);
			}
		}

		static int SpawnEnemyCountFunction(int p) {
			if (p > 1000) {
				return 22;
			}
			if (p > 700) {
				return 17;
			}
			if (p > 500) {
				return 13;
			}
			if (p > 300) {
				return 9;
			}
			if (p > 100) {
				return 6;
			}
			if (p > 60) {
				return 5;
			}
			if (p > 40) {
				return 4;
			}
			if (p > 20) {
				return 3;
			}
			if (p > 10) {
				return 2;
			}
			return 1;
		}
	}

}
