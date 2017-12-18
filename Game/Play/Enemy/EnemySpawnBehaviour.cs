using System;
using System.Linq;
using Framework;
using Framework.Utilities;
using SpaceWar.Game.Play.Player;
using SpaceWar.Game.Play.UI;

namespace SpaceWar.Game.Play.Enemy {

	public class EnemySpawnBehaviour : GameObject {

		public const float WAVE_PAUSE_DELAY = 2f;
		private static readonly Random NON_DETERMINISTIC_RNG = new Random();

		private readonly MyTimer waveStartTimerOverlay = new MyTimer();
		private readonly MyTimer waveStartTimer = new MyTimer();

		public EnemySpawnBehaviour() {
			Spawners.Init();
		}

		public override void OnStart() {
			base.OnStart();
			Spawners.All.ForEach(AddComponent);
		}

		public override void OnDestroy() {
			waveStartTimerOverlay.Cancel();
			waveStartTimer.Cancel();
			base.OnDestroy();
		}

		public override void Update() {
			if (AllSpawnersFinishedWave()) {
				// Show the overlay to the user once
				waveStartTimerOverlay.DoEvery(WAVE_PAUSE_DELAY,
					() => Scene.Current.Spawn(new NewWaveOverlay()), MyTimer.When.Start);

				// Trigger the wave start delayed
				waveStartTimer.DoEvery(WAVE_PAUSE_DELAY, StartNewWave, MyTimer.When.End);
			}

			// We want to run the waves after starting a new one
			base.Update();
		}

		private bool AllSpawnersFinishedWave() {
			return Spawners.All.Aggregate(true, (b, spawner) => b && spawner.IsWaveFinished);
		}

		private void StartNewWave() {
			var playerPoints = PlayerHelper.GetPlayerPoints();
			if (playerPoints < 4) {
				playerPoints = 4;
			}

			// Clone the list and reverse to go top down filling everything
			var spawners = Spawners.All.ToList();
			spawners.Sort((c1, c2) =>
				c2.PointsRequiredForSpawning * c2.MinEnemyCount - c1.PointsRequiredForSpawning * c1.MinEnemyCount);

			// Spawn maximum possible enemies per spawner, beginning with the most required points
			spawners.ForEach(spawner => {
				var enemiesToSpawn = (int) Math.Floor(playerPoints / (float) spawner.PointsRequiredForSpawning);

				// Skip spawners where the minimum requirement is not met
				if (enemiesToSpawn < spawner.MinEnemyCount) {
					return;
				}

				// Take the maximum spawn count if that is exceeded
				if (enemiesToSpawn > spawner.MaxEnemyCount) {
					enemiesToSpawn = spawner.MaxEnemyCount;
				}

				// NOTE Random value added to be non-deterministic
				if (enemiesToSpawn > 10) {
					// Add/substract a random number of enemies to be not deterministic
					// so the game feels different when played multiple times
					// Do this only if the enemy count is high enough to have not too much heavy enemes
					enemiesToSpawn += NON_DETERMINISTIC_RNG.Next(0, enemiesToSpawn / 10);
				}
				spawner.StartWave(enemiesToSpawn);
				playerPoints -= enemiesToSpawn * spawner.PointsRequiredForSpawning;
			});

			// NOTE
			// A possibility to select spawners would be to add a property "OccurenceLevel" and calculate the 
			// probability for each spawner and take those, where the probability fits a random value. If this
			// constellation is not  working, try to increase the number of taken spawners and repeat

			// NOTE
			// Another possibility would be to use spawners like in binary addition
			// 1st: Enemy1
			// 2nd: Enemy2
			// 3rd: Enemy2 + Enemy1
			// 4th: Enemy3
			// 5th: Enemy3 + Enemy1
			// 6th: Enemy3 + Enemy2
			// 7th: Enemy3 + Enemy2 + Enemy1
		}
	}

}
