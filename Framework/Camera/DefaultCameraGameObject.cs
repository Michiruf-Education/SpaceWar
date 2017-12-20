namespace Framework.Camera {

	public class DefaultCameraGameObject : GameObject {

		public CameraComponent Component { get; }

		public DefaultCameraGameObject() {
			AddComponent(Component = new CameraComponent());
		}

		public DefaultCameraGameObject(bool activeCamera) {
			AddComponent(Component = new CameraComponent(activeCamera));
		}
	}

}
