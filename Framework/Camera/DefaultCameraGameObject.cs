using OpenTK;

namespace Framework.Camera {

	public class DefaultCameraGameObject : GameObject {

		public CameraComponent Component { get; }

		public DefaultCameraGameObject() {
			AddComponent(Component = new CameraComponent());
		}

		public DefaultCameraGameObject(bool activeCamera) {
			AddComponent(Component = new CameraComponent(activeCamera));
		}

		public DefaultCameraGameObject(Vector2 viewport) {
			AddComponent(Component = new CameraComponent(viewport));
		}

		public DefaultCameraGameObject(Vector2 viewport, bool activeCamera = false) {
			AddComponent(Component = new CameraComponent(viewport, activeCamera));
		}
	}

}
