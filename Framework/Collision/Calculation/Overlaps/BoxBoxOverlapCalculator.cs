using OpenTK;
using Zenseless.Geometry;

namespace Framework.Collision.CollisionCalculation.Overlaps {

	public static class BoxBoxOverlapCalculator {

		public static Vector2 UnrotatedOverlap(Box2D first, Box2D second) {
			var newFirst = new Box2D(first);
			newFirst.UndoOverlap(second);
			return new Vector2(newFirst.MinX - first.MinX, newFirst.MinY - first.MinY);
		}
	}

}
