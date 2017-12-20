using OpenTK;
using Zenseless.Geometry;

namespace Framework.Collision.CollisionCalculation.Overlaps {

	public static class CircleCircleOverlapCalculator {

		public static Vector2 UnrotatedOverlap(Circle first, Circle second) {
			var newFirst = new Circle(first.CenterX, first.CenterY, first.Radius);
			newFirst.UndoOverlap(second);
			return new Vector2(newFirst.CenterX - first.CenterX, newFirst.CenterY - first.CenterY);
		}
	}

}
