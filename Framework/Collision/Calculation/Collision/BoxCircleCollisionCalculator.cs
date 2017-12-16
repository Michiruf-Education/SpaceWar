using Zenseless.Geometry;

namespace Framework.Collision.Calculation.Collision {

	public static class BoxCircleCollisionCalculator {

		/// see https://stackoverflow.com/a/1879223
		public static bool UnrotatedIntersects(Circle circle, Box2D rectangle) {
			// Find the closest point to the circle within the rectangle
			var closestX = MathHelper.Clamp(circle.CenterX, rectangle.MinX, rectangle.MaxX);
			var closestY = MathHelper.Clamp(circle.CenterY, rectangle.MinY, rectangle.MaxY);

			// Calculate the distance between the circle's center and this closest point
			var distanceX = circle.CenterX - closestX;
			var distanceY = circle.CenterY - closestY;

			// If the distance is less than the circle's radius, an intersection occurs
			var distanceSquared = distanceX * distanceX + distanceY * distanceY;
			return distanceSquared < circle.Radius * circle.Radius;
		}
	}

}
