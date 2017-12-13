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

		public TransformationUnit LocalToWorld { get; } = new TransformationUnit();
		public TransformationUnit WorldToLocal { get; } = new TransformationUnit();

		// Currently local data, could be world stuff?
		private Vector2 position;
		private float rotation;
		private Vector2 scaling;

		private Matrix3x2 transformationMatrixCache = Matrix3x2Helper.NUMERICS_ZERO;
		private Matrix3x2 transformationMatrixCacheWithCamera = Matrix3x2Helper.NUMERICS_ZERO;

		public Vector2 LocalPosition {
			get => position;
			set {
				var positionDifference = value - position;
				Translate(positionDifference);
			}
		}
		public float LocalRotation {
			get => rotation;
			set {
				var rotationDifference = value - rotation;
				Rotate(rotationDifference);
			}
		}
		public Vector2 LocalScaling {
			get => scaling;
			set {
				var scalingFactor = new Vector2(value.X / scaling.X, value.Y / scaling.Y);
				Scale(scalingFactor);
			}
		}

		public Vector2 WorldPosition {
			// NoFormat
			get => TransformPoint(position, Space.World);
			set => LocalPosition = TransformPoint(value, Space.Local);
		}
		public float WorldRotation {
			// NoFormat
			get => TransformAngle(rotation, Space.World);
			set => LocalRotation = TransformAngle(value, Space.Local);
		}
		public Vector2 WorldScaling {
			// NoFormat
			get => TransformPoint(scaling, Space.World);
			set => LocalScaling = TransformPoint(value, Space.Local);
		}

		public void Translate(float x, float y, Space space = Space.Local) {
			Translate(new Vector2(x, y), space);
		}

		public void Translate(Vector2 translation, Space space = Space.Local) {
			switch (space) {
				case Space.Local:
					WorldToLocal.Translate(translation);
					LocalToWorld.Translate(translation);
					position += translation;
					break;
				case Space.World:
					Translate(TransformPoint(translation, Space.Local));
					return;
				default:
					throw new SpaceNotExistantException();
			}
			Invalidate();
		}

		public void Rotate(float angle) {
			// Note that rotation is the same for local and world space since were only
			// having a float instead like in 3d having a vector3
			WorldToLocal.Rotate(angle);
			LocalToWorld.Rotate(-angle);
			rotation += angle;
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
					LocalToWorld.Scale(scale);
					WorldToLocal.Scale(new Vector2(1 / scale.X, 1 / scale.Y));
					scaling *= scale;
					break;
				case Space.World:
					Scale(TransformPoint(scale, Space.Local));
					return;
				default:
					throw new SpaceNotExistantException();
			}
			Invalidate();
		}

		public void Scale(float scaleX, float scaleY, float pivotX, float pivotY, Space space = Space.Local) {
			Scale(new Vector2(scaling.X, scaling.Y), pivotX, pivotY, space);
		}

		public void Scale(Vector2 scaling, float pivotX, float pivotY, Space space = Space.Local) {
			throw new ToDevelopException("Pivot scaling not implemented yet!");
		}

		public Vector2 TransformPoint(Vector2 point, Space targetSpace, bool includeParens = false /* TODO */) {
			switch (targetSpace) {
				case Space.Local:
					return FastVector2Transform.Transform(point.X, point.Y, WorldToLocal.Transformation);
				case Space.World:
					return FastVector2Transform.Transform(point.X, point.Y, LocalToWorld.Transformation);
				default:
					throw new SpaceNotExistantException();
			}
		}

		public float TransformAngle(float degree, Space space, bool includeParens = false /* TODO */) {
			Matrix3x2 spaceMatrix;
			switch (space) {
				case Space.Local:
					spaceMatrix = LocalToWorld.Transformation;
					break;
				case Space.World:
					spaceMatrix = LocalToWorld.Transformation;
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
				return GameObject.Parent.Transform.GetTransformationMatrix() * WorldToLocal.Transformation;
			}

			return WorldToLocal.Transformation;
		}

		internal void Invalidate() {
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

	public class TransformationUnit {

		public Transformation2D Transformation { get; } = new Transformation2D();

		public void Translate(Vector2 translation) {
			Translate(translation.X, translation.Y);
		}

		public void Translate(float x, float y) {
			Transformation.TranslateLocal(x, y);
		}

		public void Rotate(float angle) {
			Transformation.RotateLocal(angle);
		}

		public void Rotate(Vector2 pivot, float angle) {
			Rotate(pivot.X, pivot.Y, angle);
		}

		public void Rotate(float pivotX, float pivotY, float angle) {
			Transformation.TransformLocal(Transformation2D.CreateRotationAround(pivotX, pivotY, angle));
		}

		public void Scale(Vector2 scaling) {
			Scale(scaling.X, scaling.Y);
		}

		public void Scale(float scaleX, float scaleY) {
			Transformation.ScaleLocal(scaleX, scaleY);
		}

		public void Scale(Vector2 scaling, float pivotX, float pivotY) {
			Scale(scaling.X, scaling.Y, pivotX, pivotY);
		}

		public void Scale(float scaleX, float scaleY, float pivotX, float pivotY) {
			Transformation.TransformLocal(Transformation2D.CreateScaleAround(pivotX, pivotY, scaleX, scaleY));
		}
	}

}
