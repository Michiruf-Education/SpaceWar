using System;
using OpenTK;
using Zenseless.Geometry;
using MathHelper = Zenseless.Geometry.MathHelper;

namespace Framework.Collision.CollisionCalculation.Overlaps {

	public static class BoxCircleOverlapCalculator {

		/// see BoxCircleCollisionCalculator
		/// see https://stackoverflow.com/a/1879223
		public static Vector2 UnrotatedOverlap(Circle circle, Box2D rectangle) {
			// Find the closest point to the circle within the rectangle
			var closestX = MathHelper.Clamp(circle.CenterX, rectangle.MinX, rectangle.MaxX);
			var closestY = MathHelper.Clamp(circle.CenterY, rectangle.MinY, rectangle.MaxY);

			// Calculate the distance between the circle's center and this closest point
			var distanceX = circle.CenterX - closestX;
			var distanceY = circle.CenterY - closestY;

			// If the distance is less than the circle's radius, an intersection occurs
			var distance = Math.Sqrt(distanceX * distanceX + distanceY * distanceY);
			var distanceToMove = circle.Radius - distance;
			if (distanceToMove > 0) {
				var distanceAngle = Math.Atan2(distanceY, distanceX);
				var distanceToMoveX = distanceToMove * Math.Cos(distanceAngle);
				var distanceToMoveY = distanceToMove * Math.Sin(distanceAngle);
				return new Vector2((float) distanceToMoveX, (float) distanceToMoveY);
			}
			return Vector2.Zero;
		}

		public static Vector2 UnrotatedOverlap(Box2D rectangle, Circle circle) {
			return -UnrotatedOverlap(circle, rectangle);
		}
	}

}
