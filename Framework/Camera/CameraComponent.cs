using OpenTK;
using Matrix3x2 = System.Numerics.Matrix3x2;

namespace Framework.Camera {

	public class CameraComponent : Component {

		// TODO Is internal because we may need to reset it when the game gets deconstructed!
		// TODO 2: Set this as part of the scene, then we need no singleton here? ------> Nice!
		public static CameraComponent Active { get; internal set; }
		public static Matrix3x2 ActiveCameraMatrix {
			get {
				Matrix3x2.Invert(Active.GameObject.Transform.GetTransformationMatrixCached(false),
					out var cameraMatrix);
				return cameraMatrix;
			}
		}

		// TODO World or Local Stuff?!?!
		public Vector2 Position {
			// NoFormat
			get => GameObject.Transform.WorldPosition;
			set => GameObject.Transform.WorldPosition = value;
		}
		public float Rotation {
			// NoFormat
			get => GameObject.Transform.WorldRotation;
			set => GameObject.Transform.WorldRotation = value;
		}
		public Vector2 ViewportScaling {
			// NoFormat
			get => GameObject.Transform.WorldScaling;
			set => GameObject.Transform.WorldScaling = value;
		}

		public CameraComponent() :
			this(Active == null) {
		}

		public CameraComponent(bool activeCamera) {
			if (activeCamera) {
				Activate();
			}
		}

		public void Activate() {
			Active = this;
		}
	}

}
