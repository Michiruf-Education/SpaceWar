using Framework.Camera;
using Framework.Object;
using OpenTK;
using SpaceWar.Game.Play;

namespace SpaceWar.Game.Scene {

	public class PlayScene : Framework.Scene {

		public PlayScene() {
			var camera = new DefaultCameraGameObject(new Vector2(1f, 1f));
			camera.GetComponent<CameraComponent>().ViewportScaling = new Vector2(1f, 1f);
			camera.GetComponent<CameraComponent>().Position = new Vector2(0f, 0f);
			Spawn(camera);

			Spawn(new Border(-1, 0, 0.02f, 1f));
			Spawn(new Border(1, 0, 0.02f, 1f));
			Spawn(new Border(0, 0.5f, 2f, 0.02f));
			Spawn(new Border(0, -0.5f, 2f, 0.02f));
			Spawn(new Player.Player());
			
			Spawn(new FrameworkGameObject());
		}
	}

}
