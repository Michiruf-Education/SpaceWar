using Framework.Camera;
using OpenTK;

namespace SpaceWar.Game.Scene {

	public class PlayScene : Framework.Scene {

		public PlayScene() {
			var camera = new DefaultCameraGameObject(new Vector2(1f, 1f));
			camera.GetComponent<CameraComponent>().ViewportScaling = new Vector2(1f, 1f);
			camera.GetComponent<CameraComponent>().Position = new Vector2(0f, 0f);
			Spawn(camera);

			Spawn(new Player.Player());
		}
	}

}
