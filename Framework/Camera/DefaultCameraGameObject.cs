using OpenTK;

namespace Framework.Camera {

	public class DefaultCameraGameObject : GameObject {

		public DefaultCameraGameObject() {
			AddComponent(new CameraComponent());
		}

		public DefaultCameraGameObject(bool activeCamera) {
			AddComponent(new CameraComponent(activeCamera));
		}

		public DefaultCameraGameObject(Vector2 viewport) {
			AddComponent(new CameraComponent(viewport));
		}

		public DefaultCameraGameObject(Vector2 viewport, bool activeCamera = false) {
			AddComponent(new CameraComponent(viewport, activeCamera));
		}
	}

}
