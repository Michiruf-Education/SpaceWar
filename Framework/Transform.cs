using System.Numerics;
using Framework.Camera;
using Framework.Debug;
using Framework.Utilities;
using Zenseless.Geometry;
using Vector2 = OpenTK.Vector2;

namespace Framework {

	// TODO May invalidate cache only once every Update()-cycle, that could cause the "lag" agains borders!

	public class Transform {

		public GameObject GameObject { get; internal set; }

		public Transformation2D Transformation { get; } = new Transformation2D();
		// NOTE Maybe split into local and global transformation
		// -> all global transformations are calculated before all local (?!)
		// -> We would need this for interhitance of gameobjects
		private Matrix3x2 transformationMatrixCacheWithCamera = Matrix3x2Helper.NUMERICS_ZERO;
		private Matrix3x2 transformationMatrixCache = Matrix3x2Helper.NUMERICS_ZERO;

		/*
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
			set { } // NOTE
		}
		public Vector2 LocalScaling {
			get {
				var m = (Matrix3x2) Transformation;
				return m.GetScaling();
			}
			set { } // NOTE
		}
		public Vector2 WorldPosition {
			// NoFormat
			get => GetTransformationMatrixCached(false).GetPosition();
			set => Translate(value - WorldPosition, Space.World);
		}
		[System.Obsolete("May implemented in the future")]
		public float WorldRotation {
			get {
				var m = GetTransformationMatrixCached(false);
				return m.GetRotation();
			}
			set { } // NOTE
		}
		[System.Obsolete("May implemented in the future")]
		public Vector2 WorldScaling {
			get {
				var m = GetTransformationMatrixCached(false);
				return m.GetScaling();
			}
			set { } // NOTE
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

			throw new ToDevelopException("Rotation in world space not implemented. Test current behaviour before!");
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

			throw new ToDevelopException("Scaling in world space not implemented. Test current behaviour before!");
		}
		//*/

		internal Matrix3x2 GetTransformationMatrixCached(bool includeCamera) {
			if (includeCamera) {
				if (transformationMatrixCacheWithCamera == Matrix3x2Helper.NUMERICS_ZERO) {
					transformationMatrixCacheWithCamera = CameraComponent.ActiveCameraMatrix *
					                                      GetTransformationMatrixCached(false);
				}
				return transformationMatrixCacheWithCamera;
			}

			if (transformationMatrixCache == Matrix3x2Helper.NUMERICS_ZERO) {
				//transformationMatrixCache = GetTransformationMatrix();
				transformationMatrixCache = GetSimpleTransformationMatrix();
			}
			return transformationMatrixCache;
		}

		Matrix3x2 GetTransformationMatrix() {
			if (GameObject?.Parent != null) {
				return GameObject.Parent.Transform.GetTransformationMatrix() * Transformation;
			}

			return Transformation;
		}

		/*******************************************************
		 * Simple matrix calculation
		 *******************************************************/
		//*
		private Vector2 position;
		private float rotation;
		private Vector2 scaling = new Vector2(1f, 1f);

		public Vector2 LocalPosition {
			get => position;
			set {
				position = value;
				Invalidate();
			}
		}
		public float LocalRotation {
			get => rotation;
			set {
				rotation = value;
				Invalidate();
			}
		}
		public Vector2 LocalScaling {
			get => scaling;
			set {
				scaling = value;
				Invalidate();
			}
		}
		public Vector2 WorldPosition {
			get => position;
			set {
				position = value;
				Invalidate();
			}
		}
		public float WorldRotation {
			get => rotation;
			set {
				rotation = value;
				Invalidate();
			}
		}
		public Vector2 WorldScaling {
			get => scaling;
			set {
				scaling = value;
				Invalidate();
			}
		}

		public void Translate(Vector2 translation, Space space = Space.Local) {
			Translate(translation.X, translation.Y, space);
		}

		public void Translate(float x, float y, Space space = Space.Local) {
			position = new Vector2(position.X + x, position.Y + y);
			Invalidate();
		}

		public void Rotate(float angle, Space space = Space.Local) {
			rotation += angle;
			Invalidate();
		}

		public void Scale(Vector2 scaling, Space space = Space.Local) {
			Scale(scaling.X, scaling.Y, space);
			Invalidate();
		}

		public void Scale(float scaleX, float scaleY, Space space = Space.Local) {
			scaling = new Vector2(scaling.X * scaleX, scaling.Y * scaleY);
			Invalidate();
		}

		Matrix3x2 GetSimpleTransformationMatrix() {
			var transform = new Transformation2D();
			transform.ScaleLocal(scaling.ToNumericsVector2());
			transform.RotateLocal(rotation);
			transform.TranslateLocal(position.ToNumericsVector2());

			if (GameObject?.Parent != null) {
				return GameObject.Parent.Transform.GetSimpleTransformationMatrix() * transform;
			}

			return transform;
		}
		//*/
		/*******************************************************
		 * End simple matrix calculation
		 *******************************************************/

		internal void Invalidate() {
			transformationMatrixCacheWithCamera = Matrix3x2Helper.NUMERICS_ZERO;
			transformationMatrixCache = Matrix3x2Helper.NUMERICS_ZERO;
		}
	}

	public enum Space {

		World,
		Local
	}

}
