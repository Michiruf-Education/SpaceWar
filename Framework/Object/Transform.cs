using OpenTK;
using SpaceWar.Framework.Camera;
using SpaceWar.Framework.Extensions;
using Zenseless.Geometry;
using Matrix3x2 = System.Numerics.Matrix3x2;

namespace SpaceWar.Framework.Object {

	public class Transform {

		// TODO Implement a transform that holds the position of the GO, ...

		public GameObject GameObject { get; internal set; }

		public Vector2 Position { get; set; }
		public float Rotation { get; set; }
		public Vector2 Scaling { get; set; }

		public Transformation2D t = new Transformation2D();

		public void Translate(Vector2 translation, Space space = Space.Local) {
			Translate(translation.X, translation.Y, space);
		}

		public void Translate(float x, float y, Space space = Space.Local) {
			if (space == Space.Local) {
				t.TranslateLocal(x, y);
				return;
			}


			//if (space == Space.Local) {
			//	// NOTE Create a field for the position and use property as getter and setter for the field
			//	var position = Position;
			//	position.X += x;
			//	position.Y += y;
			//	Position = position;
			//	return;
			//}
			//// TODO
		}

		public void Rotate(float angle) {
			Rotate(Position, angle);
		}

		public void Rotate(Vector2 pivot, float angle, Space space = Space.Local) {
			Rotate(pivot.X, pivot.Y, angle, space);
		}

		public void Rotate(float pivotX, float pivotY, float angle, Space space = Space.Local) {
			// TODO
		}

		public void Scale(Vector2 scaling, Space space = Space.Local) {
			Scale(scaling.X, scaling.Y, space);
		}

		public void Scale(float xScaling, float yScaling, Space space = Space.Local) {
			// TODO
		}

		internal Vector2 CalculatePointPosition(float x, float y) {
			//return Vector2.Zero;
			// TODO!!!
			// This solution is just here to show that this would may work
			// We need to "inherit" the data from parent GO's and calculate the data including rotation and scaling

			return new Vector2 {
				X = x + Position.X,
				Y = y + Position.Y
			};
		}

		internal Matrix3x2 G_Cached() {
			// TODO Create a cache variable
			return G();
		}

		internal void Invalidate() {
			// TODO Reset cache
		}

		internal Matrix3x2 G() {
			if (GameObject.Parent != null) {
				return GameObject.Parent.Transform.G() * t;
			}

			var cameraTransformation = new Transformation2D();
			cameraTransformation.TranslateLocal(CameraComponent.Active.Position.ToNumericsVector2());
			cameraTransformation.RotateLocal(CameraComponent.Active.Rotation);
			Matrix3x2 cameraMatrix = cameraTransformation;
			Matrix3x2.Invert(cameraMatrix, out cameraMatrix);
			// TODO Viewport

			// TODO We should not cast or create objects here -> no conversions

			return cameraMatrix * t;
		}
	}

}
