using Zenseless.Geometry;

namespace Framework.Collision.CollisionCalculation {

	public class BoxBoxCollisionCalculator {

		public static bool UnrotatedIntersects(Box2D first, Box2D second) {
			return first.Intersects(second);
		}
	}

}
