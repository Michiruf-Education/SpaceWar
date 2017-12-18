using System;
using Framework.Camera;
using Framework.Debug;
using Framework.Object;
using Framework.Utilities;
using OpenTK;
using Zenseless.Geometry;
using MathHelper = OpenTK.MathHelper;
using Matrix3x2 = System.Numerics.Matrix3x2;

namespace Framework {

	// NOTE Daniel: 
	// 2 Transforms:
	// * worldToLocal
	// * localToWorld.
	// Both having values for rotation, position, scaling (in primitive types)

	public class Transform {

		public GameObject GameObject { get; internal set; }

		// Local means relative to the parent!
		private Vector2 localPosition;
		private float localRotation;
		private Vector2 localScaling = new Vector2(1f, 1f);

		internal Matrix3x2 ParentToLocal {
			get {
				// Cache hit?
				if (parentToLocalCache.HasData) {
					return parentToLocalCache.Data;
				}

				var t = new Transformation2D();
				t.TranslateLocal(localPosition.ToNumericsVector2());
				t.RotateLocal(localRotation);
				t.ScaleLocal(localScaling.ToNumericsVector2());
				return t;
			}
		}
		private readonly CachedObject<Matrix3x2> parentToLocalCache = new CachedObject<Matrix3x2>();
		internal bool HasChanged => !parentToLocalCache.HasData;
		internal Matrix3x2 LocalToParent {
			get {
				// Cache hit?
				if (localToParentCache.HasData) {
					return localToParentCache.Data;
				}

				Matrix3x2.Invert(ParentToLocal, out var newLocalToParent);
				var t = new Transformation2D();
				t.TransformLocal(newLocalToParent);
				return t;
			}
		}
		private readonly CachedObject<Matrix3x2> localToParentCache = new CachedObject<Matrix3x2>();

		public Matrix3x2 LocalToWorld {
			get {
				// Cache hit?
				if (localToWorldCache.HasData) {
					return localToWorldCache.Data;
				}

				if (GameObject?.Parent != null) {
					return GameObject.Parent.Transform.LocalToWorld * LocalToParent;
				}
				return LocalToParent;
			}
		}
		private readonly CachedObject<Matrix3x2> worldToLocalCache = new CachedObject<Matrix3x2>();
		public Matrix3x2 WorldToLocal {
			get {
				// Cache hit?
				if (worldToLocalCache.HasData) {
					return worldToLocalCache.Data;
				}

				if (GameObject?.Parent != null) {
					return GameObject.Parent.Transform.WorldToLocal * ParentToLocal;
				}
				return ParentToLocal;
			}
		}
		private readonly CachedObject<Matrix3x2> localToWorldCache = new CachedObject<Matrix3x2>();

		private readonly CachedObject<Matrix3x2> transformationMatrixCacheWithCamera = new CachedObject<Matrix3x2>();
		private readonly CachedObject<Matrix3x2> transformationMatrixCache = new CachedObject<Matrix3x2>();

		// Local means relative to the parent!
		public Vector2 LocalPosition {
			// NoFormat
			get => localPosition;
			set => Translate(value - localPosition, Space.Local);
		}
		public float LocalRotation {
			// NoFormat
			get => localRotation;
			set => Rotate(value - localRotation);
		}
		[Obsolete("Might not be working correctly yet (Scale() should be working at least in World space. " +
		          "At least scaling camera with this does not apply")]
		public Vector2 LocalScaling {
			// NoFormat
			get => localScaling;
			set {
				var currentLocalScaling = LocalScaling;
				Scale(new Vector2(value.X / currentLocalScaling.X, value.Y / currentLocalScaling.Y), Space.Local);
			}
		}

		public Vector2 WorldPosition {
			// NoFormat
			get => TransformPoint(localPosition, Space.World);
			set => Translate(value - WorldPosition, Space.World);
		}
		public float WorldRotation {
			// NoFormat
			get => TransformAngle(localRotation, Space.World);
			set => Rotate(value - WorldRotation);
		}
		[Obsolete("Might not be working correctly yet (Scale() should be working at least in World space. " +
		          "At least scaling camera with this does not apply")]
		public Vector2 WorldScaling {
			// NoFormat
			get => TransformPoint(localScaling, Space.World);
			set {
				var currentWorldScaling = WorldScaling;
				Scale(new Vector2(value.X / currentWorldScaling.X, value.Y / currentWorldScaling.Y), Space.World);
			}
		}

		public void Translate(float x, float y, Space space) {
			Translate(new Vector2(x, y), space);
		}

		public void Translate(Vector2 translation, Space space) {
			switch (space) {
				case Space.Local:
					// Substract the current position after apply the transformation to only use scaling and
					// rotation for the calculation
					localPosition += TransformPoint(translation, ParentToLocal) - localPosition;
					break;
				case Space.World:
					localPosition += TransformPoint(translation, Space.Local);
					return;
				default:
					throw new SpaceNotExistantException();
			}
			Invalidate();
		}

		public void Rotate(float angle) {
			// Note that rotation is the same for local and world space since were only
			// having a float instead like in 3d having a vector3
			localRotation += angle;
			Invalidate();
		}

		public void Rotate(float pivotX, float pivotY, float angle, Space space) {
			Rotate(new Vector2(pivotX, pivotY), angle, space);
		}

		public void Rotate(Vector2 pivot, float angle, Space space) {
			throw new ToDevelopException("Pivot rotation not implemented yet!");
		}

		public void Scale(float scaleX, float scaleY, Space space) {
			Scale(new Vector2(scaleX, scaleY), space);
		}

		public void Scale(Vector2 scale, Space space) {
			switch (space) {
				case Space.Local:
					// Divide by the current position after apply the transformation to only use position and
					// rotation for the calculation
					var transformedScale = TransformPoint(scale, ParentToLocal);
					localScaling *= new Vector2(transformedScale.X / localScaling.X, transformedScale.Y / localScaling.Y);
					break;
				case Space.World:
					localScaling *= TransformPoint(scale, Space.Local);
					return;
				default:
					throw new SpaceNotExistantException();
			}
			Invalidate();
		}

		public void Scale(float scaleX, float scaleY, float pivotX, float pivotY, Space space) {
			Scale(new Vector2(scaleX, scaleY), pivotX, pivotY, space);
		}

		public void Scale(Vector2 scaling, float pivotX, float pivotY, Space space) {
			throw new ToDevelopException("Pivot scaling not implemented yet!");
		}

		public void LookAt(Vector2 position) {
			var direction = position - WorldPosition;
			WorldRotation = MathHelper.RadiansToDegrees((float) Math.Atan2(direction.Y, direction.X));
		}

		public Vector2 TransformPoint(Vector2 point, Space targetSpace) {
			switch (targetSpace) {
				case Space.Local:
					return TransformPoint(point, WorldToLocal * LocalToParent);
				case Space.World:
					// Calculate until local and go one back up to only calculate values until the parent of
					// the current element. This avoids duplicate calculation of the current data
					return TransformPoint(point, ParentToLocal * LocalToWorld);
				default:
					throw new SpaceNotExistantException();
			}
		}

		public Vector2 TransformPoint(Vector2 point, Matrix3x2 targetSpace) {
			return FastVector2Transform.Transform(point, targetSpace);
		}

		public float TransformAngle(float degree, Space space) {
			Matrix3x2 spaceMatrix;
			switch (space) {
				case Space.Local:
					spaceMatrix = WorldToLocal * LocalToParent;
					break;
				case Space.World:
					spaceMatrix = ParentToLocal * LocalToWorld;
					break;
				default:
					throw new SpaceNotExistantException();
			}
			return TransformAngle(degree, spaceMatrix);
		}

		public float TransformAngle(float degree, Matrix3x2 targetSpace) {
			var rotatedPoint1 = FastVector2Transform.Transform(0, 0, targetSpace);
			var rotatedPoint2 = FastVector2Transform.Transform(1, 0, targetSpace);
			var roratedVector = rotatedPoint2 - rotatedPoint1;
			var rotation = Math.Atan2(roratedVector.Y, roratedVector.X);
			return (float) MathHelper.RadiansToDegrees(rotation) + degree;
		}

		internal Matrix3x2 GetTransformationMatrixCached(bool includeCamera) {
			if (includeCamera) {
				if (!transformationMatrixCacheWithCamera.HasData || CameraComponent.ActiveCameraMatrixChanged) {
					transformationMatrixCacheWithCamera.Data = WorldToLocal * CameraComponent.ActiveCameraMatrix;
				}
				return transformationMatrixCacheWithCamera.Data;
			}

			if (!transformationMatrixCache.HasData) {
				transformationMatrixCache.Data = WorldToLocal;
			}
			return transformationMatrixCache.Data;
		}

		internal void Invalidate() {
			parentToLocalCache.Invalidate();
			localToParentCache.Invalidate();
			worldToLocalCache.Invalidate();
			localToWorldCache.Invalidate();
			transformationMatrixCache.Invalidate();
			transformationMatrixCacheWithCamera.Invalidate();

			// Also invalidate all childen, because they changed too!
			if (GameObject != null) {
				foreach (var child in GameObject.Children) {
					child.Transform?.Invalidate();
				}
			}
		}
	}

	public enum Space {

		World,
		Local
	}

	public class SpaceNotExistantException : Exception {
	}

}
