using Zenseless.Geometry;

namespace Framework.Collision.Calculation.Collision {

	public class CircleCircleCollisionCalculator {

		public static bool Intersects(Circle first, Circle second) {
			return first.Intersects(second);
		}
	}

}
