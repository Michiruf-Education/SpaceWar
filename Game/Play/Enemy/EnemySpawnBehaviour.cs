using System;
using Framework;
using Framework.Utilities;
using OpenTK;
using PlayerT = SpaceWar.Game.Play.Player.Player;

namespace SpaceWar.Game.Play.Enemy {

	public class EnemySpawnBehaviour : GameObject {

		private readonly LimitedRateTimer spawnTimer = new LimitedRateTimer();

		private bool started;
		private PlayerT player;

		public override void Update() {
			base.Update();
			spawnTimer.DoOnlyEvery(Enemy.ENEMY_SPAWN_INTERVAL, SpawnEnemies);
		}

		void SpawnEnemies() {
			if (!started) {
				started = true;
				return;
			}

			// TODO
			if (player == null)
				player = Scene.Current.GetGameObject<PlayerT>();

			var enemyCount = SpawnEnemyCountFunction(player.Attributes.Points);
			var random = new Random();

			for (var i = 0; i < enemyCount; i++) {
				var enemy1 = new Enemy();
				enemy1.Transform.WorldPosition = new Vector2(
					1f - (float) random.NextDouble() / 4f - 0.05f,
					0.5f - (float) random.NextDouble() / 4f - 0.05f
				);
				Scene.Current.Spawn(enemy1);

				var enemy2 = new Enemy();
				enemy2.Transform.WorldPosition = new Vector2(
					1f - (float) random.NextDouble() / 4f - 0.05f,
					-0.5f + (float) random.NextDouble() / 4f + 0.05f
				);
				Scene.Current.Spawn(enemy2);

				var enemy3 = new Enemy();
				enemy3.Transform.WorldPosition = new Vector2(
					-1f + (float) random.NextDouble() / 4f + 0.05f,
					-0.5f + (float) random.NextDouble() / 4f + 0.05f
				);
				Scene.Current.Spawn(enemy3);

				var enemy4 = new Enemy();
				enemy4.Transform.WorldPosition = new Vector2(
					-1f + (float) random.NextDouble() / 4f + 0.05f,
					0.5f - (float) random.NextDouble() / 4f - 0.05f
				);
				Scene.Current.Spawn(enemy4);
			}
		}

		static int SpawnEnemyCountFunction(int p) {
			if (p > 500) {
				return 10;
			}
			if (p > 300) {
				return 8;
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
