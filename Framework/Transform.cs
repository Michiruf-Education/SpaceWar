using System;
using System.Numerics;
using Framework.Camera;
using Framework.Utilities;
using Zenseless.Geometry;
using Vector2 = OpenTK.Vector2;

namespace Framework {

	public class Transform {

		public GameObject GameObject { get; internal set; }

		public Transformation2D Transformation { get; } = new Transformation2D();
		// TODO Maybe split into local and global transformation
		// TODO -> all global transformations are calculated before all local (?!)
		// -> We would need this for interhitance of gameobjects
		private Matrix3x2 transformationMatrixCacheWithCamera = Matrix3x2Helper.NUMERICS_ZERO;
		private Matrix3x2 transformationMatrixCache = Matrix3x2Helper.NUMERICS_ZERO;

		public Vector2 LocalPosition {
			// NoFormat
			get => ((Matrix3x2) Transformation).GetPosition();
			set => Translate(value - LocalPosition);
		}
		public float LocalRotation {
			get {
				var m = (Matrix3x2) Transformation;
				return m.GetRotation();
			}
			set { } // TODO
		}
		public Vector2 LocalScaling {
			get {
				var m = (Matrix3x2) Transformation;
				return m.GetScaling();
			}
			set { } // TODO
		}
		public Vector2 WorldPosition {
			// NoFormat
			get => GetTransformationMatrixCached(false).GetPosition();
			set => Translate(value - WorldPosition, Space.World);
		}
		[Obsolete("May implemented in the future")]
		public float WorldRotation {
			get {
				var m = GetTransformationMatrixCached(false);
				return m.GetRotation();
			}
			set { } // TODO
		}
		[Obsolete("May implemented in the future")]
		public Vector2 WorldScaling {
			get {
				var m = GetTransformationMatrixCached(false);
				return m.GetScaling();
			}
			set { } // TODO
		}

		public void Translate(Vector2 translation, Space space = Space.Local) {
			Translate(translation.X, translation.Y, space);
		}

		public void Translate(float x, float y, Space space = Space.Local) {
			if (space == Space.Local) {
				Transformation.TranslateLocal(x, y);
				Invalidate();
				return;
			}

			Transformation.TranslateGlobal(x, y);
			Invalidate();
		}

		public void Rotate(float angle, Space space = Space.Local) {
			if (space == Space.Local) {
				Transformation.RotateLocal(angle);
				Invalidate();
				return;
			}

			Transformation.RotateGlobal(angle);
			Invalidate();
		}

		public void Rotate(Vector2 pivot, float angle, Space space = Space.Local) {
			Rotate(pivot.X, pivot.Y, angle, space);
		}

		public void Rotate(float pivotX, float pivotY, float angle, Space space = Space.Local) {
			var rotation = Transformation2D.CreateRotationAround(pivotX, pivotY, angle);

			if (space == Space.Local) {
				Transformation.TransformLocal(rotation);
				Invalidate();
				return;
			}

			Transformation.TransformGlobal(rotation);
			Invalidate();

			throw new NotImplementedException("Rotation in world space not implemented. Test current behaviour before!");
		}

		public void Scale(Vector2 scaling, Space space = Space.Local) {
			Scale(scaling.X, scaling.Y, space);
		}

		public void Scale(float scaleX, float scaleY, Space space = Space.Local) {
			if (space == Space.Local) {
				Transformation.ScaleLocal(scaleX, scaleY);
				Invalidate();
				return;
			}

			Transformation.ScaleGlobal(scaleX, scaleY);
			Invalidate();
		}

		public void Scale(Vector2 scaling, float pivotX, float pivotY, Space space = Space.Local) {
			Scale(scaling.X, scaling.Y, pivotX, pivotY, space);
		}

		public void Scale(float scaleX, float scaleY, float pivotX, float pivotY, Space space = Space.Local) {
			var scaling = Transformation2D.CreateScaleAround(pivotX, pivotY, scaleX, scaleY);

			if (space == Space.Local) {
				Transformation.TransformLocal(scaling);
				Invalidate();
				return;
			}

			Transformation.TransformGlobal(scaling);
			Invalidate();

			throw new NotImplementedException("Scaling in world space not implemented. Test current behaviour before!");
		}

		internal Matrix3x2 GetTransformationMatrixCached(bool includeCamera) {
			if (includeCamera) {
				if (transformationMatrixCacheWithCamera == Matrix3x2Helper.NUMERICS_ZERO) {
					transformationMatrixCacheWithCamera = CameraComponent.ActiveCameraMatrix *
					                                      GetTransformationMatrixCached(false);
				}
				return transformationMatrixCacheWithCamera;
			}

			if (transformationMatrixCache == Matrix3x2Helper.NUMERICS_ZERO) {
				transformationMatrixCache = GetTransformationMatrix();
			}
			return transformationMatrixCache;
		}

		Matrix3x2 GetTransformationMatrix() {
			if (GameObject?.Parent != null) {
				return GameObject.Parent.Transform.GetTransformationMatrix() * Transformation;
			}

			return Transformation;
		}

		internal void Invalidate() {
			transformationMatrixCacheWithCamera = Matrix3x2Helper.NUMERICS_ZERO;
			transformationMatrixCache = Matrix3x2Helper.NUMERICS_ZERO;
		}
	}
	
	public enum Space {
		
		World, Local
	}

}
