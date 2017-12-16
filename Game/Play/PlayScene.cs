using System;
using Framework;
using Framework.Camera;
using Framework.Engine;
using OpenTK;
using SpaceWar.Game.Play.Enemy;
using SpaceWar.Game.Play.Field;
using SpaceWar.Game.Play.Player;
using SpaceWar.Game.Play.UI;

namespace SpaceWar.Game.Play {

	public class PlayScene : Scene {

		private readonly int playerCount;

		public PlayScene(int playerCount = 3) {
			this.playerCount = playerCount;
		}

		public override void OnStart() {
			base.OnStart();

			// Camera
			Spawn(new DefaultCameraGameObject());

			// World
			Spawn(new Background());
			Spawn(new Border(-1f, 0f, 0.02f, 1f));
			Spawn(new Border(1f, 0f, 0.02f, 1f));
			Spawn(new Border(0f, 0.5f, 2f, 0.02f));
			Spawn(new Border(0f, -0.5f, 2f, 0.02f));

			// Player
			SpawnPlayer();
			Spawn(new FollowingCameraBehaviour());
			Spawn(new HealthBar());
			Spawn(new PointDisplay());

			// Enemies
			Spawn(new EnemySpawnBehaviour());

			// Engine
			Spawn(new FrameworkEngineGameObject());
		}

		private void SpawnPlayer() {
			for (int i = 0; i < playerCount; i++) {
				var player = new Player.Player(i);
				Spawn(player);
				if (playerCount > 1) {
					var deg = i * 360 / playerCount + 180;
					player.Transform.WorldPosition = new Vector2(
						(float) Math.Cos(MathHelper.DegreesToRadians(deg)) * 0.1f,
						(float) Math.Sin(MathHelper.DegreesToRadians(deg)) * 0.1f);
				}
			}
		}
	}

}
