using System;
using System.IO;
using Framework;
using Framework.Camera;
using Framework.Engine;
using Framework.Sound;
using OpenTK;
using SpaceWar.Game.Play.Enemy;
using SpaceWar.Game.Play.Enemy.Enemy1;
using SpaceWar.Game.Play.Field;
using SpaceWar.Game.Play.Player;
using SpaceWar.Game.Play.UI;
using SpaceWar.Resources;

namespace SpaceWar.Game.Play {

	public class PlayScene : Scene {

		private static Random SOUND_RANDOM = new Random();

		public const float FIELD_WIDTH = 2f;
		public const float FIELD_HEIGHT = 1f;
		public const float BORDER_WIDTH = 0.02f;
		public const float BORDER_PADDING = 0.1f;
		public const float PLAYER_SPAWN_DISTANCE = 0.1f;

		private readonly int playerCount;

		public PlayScene(int playerCount = 1) {
			this.playerCount = playerCount;
		}

		public override void OnStart() {
			base.OnStart();

			// Camera
			Spawn(new DefaultCameraGameObject());

			// World
			Spawn(new Background());
			Spawn(new Border(FIELD_WIDTH, FIELD_HEIGHT, BORDER_WIDTH, BORDER_PADDING, Border.Position.Left));
			Spawn(new Border(FIELD_WIDTH, FIELD_HEIGHT, BORDER_WIDTH, BORDER_PADDING, Border.Position.Top));
			Spawn(new Border(FIELD_WIDTH, FIELD_HEIGHT, BORDER_WIDTH, BORDER_PADDING, Border.Position.Right));
			Spawn(new Border(FIELD_WIDTH, FIELD_HEIGHT, BORDER_WIDTH, BORDER_PADDING, Border.Position.Bottom));

			// Player
			SpawnPlayers();
			Spawn(new FollowingCameraBehaviour());
			Spawn(new GameOverObservingBehaviour());
			Spawn(new HealthBar());
			Spawn(new PointDisplay());

			// Enemies
			Spawn(new EnemySpawnBehaviour());

			// Engine
			Spawn(new FrameworkEngineGameObject());

			// Sound
			PlayRandomPlaySound();
		}

		private void SpawnPlayers() {
			for (int i = 0; i < playerCount; i++) {
				var player = new Player.Player(i);
				Spawn(player);
				if (playerCount > 1) {
					var deg = i * 360 / playerCount + 180;
					player.Transform.WorldPosition = new Vector2(
						(float) Math.Cos(MathHelper.DegreesToRadians(deg)) * PLAYER_SPAWN_DISTANCE,
						(float) Math.Sin(MathHelper.DegreesToRadians(deg)) * PLAYER_SPAWN_DISTANCE);
				}
			}
		}

		private void PlayRandomPlaySound() {
			UnmanagedMemoryStream s;
			switch (SOUND_RANDOM.Next(0, 0)) { // TODO Embed more Sounds
				case 0:
					s = Resource.PlaySound0;
					break;
				default:
					throw new InvalidOperationException("No sound for random value defined");
			}

			// Play the menu sound
			AudioPlayer.Get().Play(new Sound(s, SoundFormat.Mp3)
				.Volume(0.5f)
				.Repeat(true)
				.StartSeek(2));
		}
	}

}
