using OpenTK;

namespace SpaceWar.Framework.Camera {

	public class CameraComponent : Component {

		public static CameraComponent Active { get; set; }

		public Vector2 Position { get => GameObject.Transform.Position; set => GameObject.Transform.Position = value; }
		public float Rotation { get => GameObject.Transform.Rotation; set => GameObject.Transform.Rotation = value; }
		public Vector2 ViewportScaling { get => GameObject.Transform.Scaling; set => GameObject.Transform.Scaling = value; }

		public CameraComponent() :
			this(Active == null) {
		}

		public CameraComponent(bool activeCamera) :
			this(new Vector2(Game.Instance.Window.Width, Game.Instance.Window.Height), activeCamera) {
		}

		public CameraComponent(Vector2 viewport) :
			this(viewport, Active == null) {
		}

		public CameraComponent(Vector2 viewport, bool activeCamera = false) {
			//ViewportScaling = viewport;
			// We cannot set this since this component is not immediately attached to a gameobject
			// (when calling the constructor)

			if (activeCamera) {
				Activate();
			}
		}

		public void Activate() {
			Active = this;
		}

		// TODO Validate anyhow if a active camera is present?
	}

}
