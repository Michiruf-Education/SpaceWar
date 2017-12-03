using System;
using Framework.Algorithms;
using Framework.Algorithms.CollisionDetection.Calculation;
using Zenseless.Geometry;

namespace Framework.Collision {

	public class CircleCollider : ColliderComponent {

		public Circle Circle { get; set; }

		private bool transformedCircleIsCached;
		private Circle transformedCircleCached;

		public CircleCollider(Circle circle) {
			SetCircle(circle);
		}

		public CircleCollider(float radius) : this(
			new Circle(0.0f, 0.0f, radius)) {
		}

		public CircleCollider SetCircle(Circle circle) {
			Circle = circle;
			return this;
		}

		public Circle GetTransformedCircleCached() {
			// TODO If the "true ||" check is not present,
			// we get the "flickering" effect when running agains a wall
			if (true || !transformedCircleIsCached) {
				var matrix = GameObject.Transform.GetTransformationMatrixCached(false);
				var center = FastVector2Transform.Transform(Circle.CenterX, Circle.CenterY, matrix);
				transformedCircleCached = new Circle(center.X, center.Y, Circle.Radius);
				transformedCircleIsCached = true;
			}

			return transformedCircleCached;
		}

		public override bool CollidesWith(ColliderComponent other) {
			switch (other) {
				case BoxCollider boxCollider:
					return BoxCircleCollisionCalculator.UnrotatedIntersects(
						GetTransformedCircleCached(), boxCollider.GetTransformedRectCached());
				case CircleCollider circleCollider:
					return GetTransformedCircleCached().Intersects(circleCollider.GetTransformedCircleCached());
				default:
					throw new ArgumentException("Other component for collision detection is of type " +
					                            other.GetType() + " which is not implemented yet!");
			}
		}

		public override void InvalidateCache() {
			transformedCircleIsCached = false;
			transformedCircleCached = null;
		}
		
		// TODO Debur Render
	}

}
