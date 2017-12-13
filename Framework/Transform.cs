using System;
using Framework.Algorithms;
using Framework.Camera;
using Framework.Debug;
using Framework.Utilities;
using OpenTK;
using Zenseless.Geometry;
using MathHelper = OpenTK.MathHelper;
using Matrix3x2 = System.Numerics.Matrix3x2;

namespace Framework {

	// TODO May invalidate cache only once every Update()-cycle, that could cause the "lag" agains borders!

	//private Matrix3x2 transformationMatrixCacheWithCamera = Matrix3x2Helper.NUMERICS_ZERO;
	//private Matrix3x2 transformationMatrixCache = Matrix3x2Helper.NUMERICS_ZERO;

	public class Transform {

		public GameObject GameObject { get; internal set; }

		public Transformation2D LocalToWorld { get; set; } = new Transformation2D();
		public Transformation2D WorldToLocal { get; set; } = new Transformation2D();

		private Vector2 worldPosition;
		private float worldRotation;
		private Vector2 worldScaling;

		private Matrix3x2 transformationMatrixCache = Matrix3x2Helper.NUMERICS_ZERO;
		private Matrix3x2 transformationMatrixCacheWithCamera = Matrix3x2Helper.NUMERICS_ZERO;

		public Vector2 LocalPosition {
			// NoFormat
			get => TransformPoint(worldPosition, Space.Local);
			set => WorldPosition = TransformPoint(value, Space.Local);
		}
		public float LocalRotation {
			// NoFormat
			get => TransformAngle(worldRotation, Space.Local);
			set => WorldRotation = TransformAngle(value, Space.Local);
		}
		public Vector2 LocalScaling {
			// NoFormat
			get => TransformPoint(worldScaling, Space.Local);
			set => WorldScaling = TransformPoint(value, Space.Local);
		}

		public Vector2 WorldPosition {
			// NoFormat
			get => worldPosition;
			set => Translate(value - WorldPosition);
		}
		public float WorldRotation {
			// NoFormat
			get => worldRotation;
			set => Rotate(value - WorldRotation);
		}
		public Vector2 WorldScaling {
			// NoFormat
			get => worldScaling;
			set {
				var currentWorldScaling = WorldScaling;
				Scale(new Vector2(value.X / currentWorldScaling.X, value.Y / currentWorldScaling.Y));
			}
		}

		public void Translate(float x, float y, Space space = Space.Local) {
			Translate(new Vector2(x, y), space);
		}

		public void Translate(Vector2 translation, Space space = Space.Local) {
			switch (space) {
				case Space.Local:
					Translate(TransformPoint(translation, Space.World), Space.World);
					return;
				case Space.World:
					// NOTE That worked before! (CalculateTransform did not exist)
					LocalToWorld.TranslateLocal(-translation.ToNumericsVector2());
					WorldToLocal.TranslateGlobal(translation.ToNumericsVector2());
					worldPosition += translation;
					break;
				default:
					throw new SpaceNotExistantException();
			}
			Invalidate();
		}

		public void Rotate(float angle) {
			// Note that rotation is the same for local and world space since were only
			// having a float instead like in 3d having a vector3
			
			// NOTE That worked before! (CalculateTransform did not exist)
			LocalToWorld.RotateGlobal(-angle);
			WorldToLocal.RotateLocal(angle);
			worldRotation += angle;
			Invalidate();
		}

		public void Rotate(float pivotX, float pivotY, float angle, Space space = Space.Local) {
			Rotate(new Vector2(pivotX, pivotY), angle, space);
		}

		public void Rotate(Vector2 pivot, float angle, Space space = Space.Local) {
			throw new ToDevelopException("Pivot rotation not implemented yet!");
		}

		public void Scale(float scaleX, float scaleY, Space space = Space.Local) {
			Scale(new Vector2(scaleX, scaleY), space);
		}

		public void Scale(Vector2 scale, Space space = Space.Local) {
			switch (space) {
				case Space.Local:
					Scale(TransformPoint(scale, Space.World), Space.World);
					return;
				case Space.World:
					worldScaling *= scale;
					// NOTE That worked before! (CalculateTransform did not exist)
					LocalToWorld.ScaleGlobal(new Vector2(1 / scale.X, 1 / scale.Y).ToNumericsVector2());
					WorldToLocal.ScaleLocal(scale.ToNumericsVector2());
					break;
				default:
					throw new SpaceNotExistantException();
			}
			Invalidate();
		}

		public void Scale(float scaleX, float scaleY, float pivotX, float pivotY, Space space = Space.Local) {
			Scale(new Vector2(worldScaling.X, worldScaling.Y), pivotX, pivotY, space);
		}

		public void Scale(Vector2 scaling, float pivotX, float pivotY, Space space = Space.Local) {
			throw new ToDevelopException("Pivot scaling not implemented yet!");
		}

		public Vector2 TransformPoint(Vector2 point, Space targetSpace, bool includeParens = false /* TODO */) {
			switch (targetSpace) {
				case Space.Local:
					return FastVector2Transform.Transform(point.X, point.Y, WorldToLocal);
				case Space.World:
					return FastVector2Transform.Transform(point.X, point.Y, LocalToWorld);
				default:
					throw new SpaceNotExistantException();
			}
		}

		public float TransformAngle(float degree, Space space, bool includeParens = false /* TODO */) {
			Matrix3x2 spaceMatrix;
			switch (space) {
				case Space.Local:
					spaceMatrix = LocalToWorld;
					break;
				case Space.World:
					spaceMatrix = LocalToWorld;
					break;
				default:
					throw new SpaceNotExistantException();
			}
			var rotatedPoint1 = FastVector2Transform.Transform(0, 0, spaceMatrix);
			var rotatedPoint2 = FastVector2Transform.Transform(1, 0, spaceMatrix);
			var roratedVector = rotatedPoint2 - rotatedPoint1;
			var worldToLocalRotation = Math.Atan2(roratedVector.Y, roratedVector.X);
			return (float) MathHelper.RadiansToDegrees(worldToLocalRotation) + degree;
		}

		internal Matrix3x2 GetTransformationMatrixCached(bool includeCamera) {
			if (includeCamera) {
				if (true || transformationMatrixCacheWithCamera == Matrix3x2Helper.NUMERICS_ZERO) {
					transformationMatrixCacheWithCamera = CameraComponent.ActiveCameraMatrix *
					                                      GetTransformationMatrixCached(false);
				}
				return transformationMatrixCacheWithCamera;
			}

			if (true || transformationMatrixCache == Matrix3x2Helper.NUMERICS_ZERO) {
				transformationMatrixCache = GetTransformationMatrix();
			}
			return transformationMatrixCache;
		}

		Matrix3x2 GetTransformationMatrix() {
			if (GameObject?.Parent != null) {
				return GameObject.Parent.Transform.GetTransformationMatrix() * WorldToLocal;
			}

			return WorldToLocal;
		}

		/* TODO HERE I AM
		internal void CalculateTransformations() {
			WorldToLocal = new Transformation2D();
			WorldToLocal.TranslateGlobal(worldPosition.ToNumericsVector2());
			WorldToLocal.RotateLocal(worldRotation);
			WorldToLocal.ScaleLocal(worldScaling.ToNumericsVector2());

			Matrix3x2.Invert(WorldToLocal, out var newLocalToWorld);
			LocalToWorld = new Transformation2D();
			LocalToWorld.TransformLocal(newLocalToWorld);
		}
		*/

		internal void Invalidate() {
//			CalculateTransformations();
			transformationMatrixCacheWithCamera = Matrix3x2Helper.NUMERICS_ZERO;
			transformationMatrixCache = Matrix3x2Helper.NUMERICS_ZERO;
		}
	}

	public enum Space {

		World,
		Local
	}

	public class SpaceNotExistantException : Exception {
	}

}
