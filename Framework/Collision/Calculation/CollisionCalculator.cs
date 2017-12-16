using System;
using Framework.Collision.Calculation.Collision;
using Framework.Collision.CollisionCalculation.Overlaps;
using OpenTK;
using Zenseless.Geometry;

namespace Framework.Collision.CollisionCalculation {

	public static class CollisionCalculator {

		public static bool UnrotatedIntersects(object first, object second) {
			switch (first) {
				case Box2D firstBox:
					switch (second) {
						case Box2D secondBox:
							return BoxBoxCollisionCalculator.UnrotatedIntersects(firstBox, secondBox);
						case Circle secondCircle:
							return BoxCircleCollisionCalculator.UnrotatedIntersects(secondCircle, firstBox);
					}
					break;
				case Circle firstCircle:
					switch (second) {
						case Box2D secondBox:
							return BoxCircleCollisionCalculator.UnrotatedIntersects(firstCircle, secondBox);
						case Circle secondCircle:
							return CircleCircleCollisionCalculator.Intersects(firstCircle, secondCircle);
					}
					break;
			}

			throw new ArgumentException("Collision between " + first.GetType().Name + " and " +
			                            second.GetType().Name + " not implemented yet!");
		}

		public static Vector2 UnrotatedOverlap(object first, object second) {
			switch (first) {
				case Box2D firstBox:
					switch (second) {
						case Box2D secondBox:
							return BoxBoxOverlapCalculator.UnrotatedOverlap(firstBox, secondBox);
						case Circle secondCircle:
							return BoxCircleOverlapCalculator.UnrotatedOverlap(firstBox, secondCircle);
					}
					break;
				case Circle firstCircle:
					switch (second) {
						case Box2D secondBox:
							return BoxCircleOverlapCalculator.UnrotatedOverlap(firstCircle, secondBox);
						case Circle secondCircle:
							return CircleCircleOverlapCalculator.UnrotatedOverlap(firstCircle, secondCircle);
					}
					break;
			}

			throw new ArgumentException("UndoOverlap between " + first.GetType().Name + " and " +
			                            second.GetType().Name + " not implemented yet!");
		}
	}

}
