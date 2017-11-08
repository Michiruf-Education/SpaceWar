using System;
using System.Numerics;
using SpaceWar.Framework.Camera;
using SpaceWar.Framework.Extensions;
using SpaceWar.Framework.Helper;
using Zenseless.Geometry;
using Vector2 = OpenTK.Vector2;

namespace SpaceWar.Framework.Object {

	public class Transform {

		public GameObject GameObject { get; internal set; }

		public Transformation2D Transformation { get; set; } = new Transformation2D();
		// TODO Maybe split into local and global transformation
		// TODO -> all global transformations are calculated before all local (?!)
		// -> We would need this for interhitance of gameobjects
		private Matrix3x2 transformationMatrixCache = MatrixHelper.NUMERICS_ZERO;

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

		public void Rotate(float angle, Space space = Space.Local) {
			if (space == Space.Local) {
				Transformation.RotateLocal(angle);
				return;
			}

			Transformation.RotateGlobal(angle);
		}

		public void Rotate(Vector2 pivot, float angle, Space space = Space.Local) {
			Rotate(pivot.X, pivot.Y, angle, space);
		}

		public void Rotate(float pivotX, float pivotY, float angle, Space space = Space.Local) {
			var rotation = Transformation2D.CreateRotationAround(pivotX, pivotY, angle);

			if (space == Space.Local) {
				Transformation.TransformLocal(rotation);
				return;
			}

			Transformation.TransformGlobal(rotation);

			throw new NotImplementedException("Rotation in world space not implemented. Test current behaviour before!");
		}

		public void Scale(Vector2 scaling, Space space = Space.Local) {
			Scale(scaling.X, scaling.Y, space);
		}

		public void Scale(float scaleX, float scaleY, Space space = Space.Local) {
			if (space == Space.Local) {
				Transformation.ScaleLocal(scaleX, scaleY);
				return;
			}

			Transformation.ScaleGlobal(scaleX, scaleY);
		}

		public void Scale(Vector2 scaling, float pivotX, float pivotY, Space space = Space.Local) {
			Scale(scaling.X, scaling.Y, pivotX, pivotY, space);
		}

		public void Scale(float scaleX, float scaleY, float pivotX, float pivotY, Space space = Space.Local) {
			var scaling = Transformation2D.CreateScaleAround(pivotX, pivotY, scaleX, scaleY);

			if (space == Space.Local) {
				Transformation.TransformLocal(scaling);
				return;
			}

			Transformation.TransformGlobal(scaling);

			throw new NotImplementedException("Scaling in world space not implemented. Test current behaviour before!");
		}

		internal Matrix3x2 GetTransformationMatrixCached() {
			if (transformationMatrixCache == MatrixHelper.NUMERICS_ZERO) {
				transformationMatrixCache = GetTransformationMatrix();
			}

			return transformationMatrixCache;
		}

		Matrix3x2 GetTransformationMatrix() {
			if (GameObject.Parent != null) {
				return GameObject.Parent.Transform.GetTransformationMatrix() * Transformation;
			}

			var cameraTransformation = new Transformation2D();
			cameraTransformation.TranslateLocal(CameraComponent.Active.Position.ToNumericsVector2());
			cameraTransformation.RotateLocal(CameraComponent.Active.Rotation);
			cameraTransformation.ScaleLocal(CameraComponent.Active.ViewportScaling.ToNumericsVector2());
			Matrix3x2.Invert(cameraTransformation, out var cameraMatrix);
			if (false) {
				// NOTE Remove
				Console.WriteLine(cameraMatrix.NumericsMatrixPrettyPrint());
				Console.WriteLine();
			}

			return cameraMatrix * Transformation;

			// TODO We should not cast or create objects here -> no conversions
		}

		internal void Invalidate() {
			transformationMatrixCache = MatrixHelper.NUMERICS_ZERO;
		}
	}

}
