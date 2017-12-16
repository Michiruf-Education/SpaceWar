using System;
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

			throw new ArgumentException("Collisions betwee " + first.GetType().Name + " and " +
			                            second.GetType().Name + " not implemented yet!");
		}
	}

}
