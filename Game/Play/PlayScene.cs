using Framework;
using Framework.Camera;
using Framework.Object;
using OpenTK;
using SpaceWar.Game.Play.Enemy;
using SpaceWar.Game.Play.Field;
using SpaceWar.Game.Play.UI;

namespace SpaceWar.Game.Play {

	public class PlayScene : Scene {

		public PlayScene() {
			// Camera
			var camera = new DefaultCameraGameObject(new Vector2(1f, 1f));
			camera.GetComponent<CameraComponent>().ViewportScaling = new Vector2(1f, 1f);
			camera.GetComponent<CameraComponent>().Position = new Vector2(0f, 0f);
			Spawn(camera);

			// World
			Spawn(new Border(-1, 0, 0.02f, 1f));
			Spawn(new Border(1, 0, 0.02f, 1f));
			Spawn(new Border(0, 0.5f, 2f, 0.02f));
			Spawn(new Border(0, -0.5f, 2f, 0.02f));
			//Spawn(new Background());

			// Player
			Spawn(new Player.Player());
			Spawn(new HealthBar());

			// Enemies
			Spawn(new EnemySpawnBehaviour());

			// Engine
			Spawn(new FrameworkGameObject());
		}
	}

}
