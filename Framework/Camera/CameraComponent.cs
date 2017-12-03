using OpenTK;
using Matrix3x2 = System.Numerics.Matrix3x2;

namespace Framework.Camera {

	public class CameraComponent : Component {
		
		// TODO Is internal because we may need to reset it when the game gets deconstructed!
		// TODO 2: Set this as part of the scene, then we need no singleton here? ------> Nice!
		public static CameraComponent Active { get; internal set; }
		public static Matrix3x2 ActiveCameraMatrix {
			get {
				Matrix3x2.Invert(Active.GameObject.Transform.Transformation, out var cameraMatrix);
				return cameraMatrix;
			}
		}

		// TODO World or Local Stuff?!?!
		public Vector2 Position {
			// NoFormat
			get => GameObject.Transform.LocalPosition;
			set => GameObject.Transform.LocalPosition = value;
		}
		public float Rotation {
			// NoFormat
			get => GameObject.Transform.LocalRotation;
			set => GameObject.Transform.LocalRotation = value;
		}
		public Vector2 ViewportScaling {
			// NoFormat
			get => GameObject.Transform.LocalScaling;
			set => GameObject.Transform.LocalScaling = value;
		}

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
	}

}
