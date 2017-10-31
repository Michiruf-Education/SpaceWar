using OpenTK;
using SpaceWar.Framework.Camera;
using SpaceWar.Framework.Extensions;
using Zenseless.Geometry;
using Matrix3x2 = System.Numerics.Matrix3x2;

namespace SpaceWar.Framework.Object {

	public class Transform {

		public GameObject GameObject { get; internal set; }

		public Transformation2D Transformation { get; set; } = new Transformation2D();
		private Matrix3x2 transformationMatrixCache = Matrix3x2.Identity;

		public Vector2 Position { get; set; } // TODO Calc by "Transformation"
		public float Rotation { get; set; } // TODO Calc by "Transformation"
		public Vector2 Scaling { get; set; } // TODO Calc by "Transformation"

		public void Translate(Vector2 translation, Space space = Space.Local) {
			Translate(translation.X, translation.Y, space);
		}

		public void Translate(float x, float y, Space space = Space.Local) {
			if (space == Space.Local) {
				Transformation.TranslateLocal(x, y);
				return;
			}

			Transformation.TranslateGlobal(x, y);
		}

		public void Rotate(float angle) {
			//Rotate(Position, angle); // TODO
			Transformation.RotateLocal(angle);
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

		internal Matrix3x2 GetTransformMatrixCached() {
			// TODO Which to use?
			if (transformationMatrixCache == Matrix3x2.Identity) {
				transformationMatrixCache = GetTransformMatrix();
			}

			return transformationMatrixCache;
		}

		Matrix3x2 GetTransformMatrix() {
			if (GameObject.Parent != null) {
				return GameObject.Parent.Transform.GetTransformMatrix() * Transformation;
			}

			//// TODO This does not make any changes yet?!
			//var cameraTransformation = new Transformation2D();
			//cameraTransformation.TranslateLocal(CameraComponent.Active.Position.ToNumericsVector2());
			//cameraTransformation.RotateLocal(CameraComponent.Active.Rotation);
			//Matrix3x2 cameraMatrix = cameraTransformation;
			//Matrix3x2.Invert(cameraMatrix, out cameraMatrix);
			//// TODO Viewport
			//return cameraMatrix * Transformation;

			// TODO We should not cast or create objects here -> no conversions

			return Transformation;
		}

		internal void Invalidate() {
			transformationMatrixCache = Matrix3x2.Identity;
		}
	}

}
