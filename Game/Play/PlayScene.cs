using Framework;
using Framework.Camera;
using Framework.Engine;
using OpenTK;
using SpaceWar.Game.Play.Enemy;
using SpaceWar.Game.Play.Field;
using SpaceWar.Game.Play.UI;

namespace SpaceWar.Game.Play {

	public class PlayScene : Scene {

		private readonly int playerCount;

		public PlayScene(int playerCount = 1) {
			this.playerCount = playerCount;
		}

		public override void OnStart() {
			base.OnStart();

			// Camera
			var camera = new DefaultCameraGameObject();
//			camera.Component.ViewportScaling = new Vector2(10f, 10f);
//			camera.Component.Position = new Vector2(-1f, 1f);
//			camera.Component.Rotation = 90;
			Spawn(camera);

			// World
			Spawn(new Background());
			Spawn(new Border(-1f, 0f, 0.02f, 1f));
			Spawn(new Border(1f, 0f, 0.02f, 1f));
			Spawn(new Border(0f, 0.5f, 2f, 0.02f));
			Spawn(new Border(0f, -0.5f, 2f, 0.02f));

			// Player
			for (int i = 0; i < playerCount; i++) {
				Spawn(new Player.Player(i));
			}
			Spawn(new HealthBar());
			Spawn(new PointDisplay());

			// Enemies
			Spawn(new EnemySpawnBehaviour());

			// Engine
			Spawn(new FrameworkEngineGameObject());
		}
	}

}
