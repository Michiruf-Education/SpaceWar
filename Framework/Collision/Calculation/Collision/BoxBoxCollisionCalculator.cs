using Zenseless.Geometry;

namespace Framework.Collision.Calculation.Collision {

	public class BoxBoxCollisionCalculator {

		public static bool UnrotatedIntersects(Box2D first, Box2D second) {
			return first.Intersects(second);
		}
	}

}
