using System;
using System.Drawing;
using Framework.Algorithms;
using Framework.Algorithms.CollisionDetection.Calculation;
using Framework.Debug;
using Framework.Object;
using OpenTK.Graphics.OpenGL;
using Zenseless.Geometry;

namespace Framework.Collision {

	public class CircleCollider : ColliderComponent, RenderComponent {

		public Circle Circle { get; set; }

		private CachedObject<Circle> transformedCircleCache;

		public CircleCollider(Circle circle) {
			SetCircle(circle);
		}

		public CircleCollider(float radius) : this(
			new Circle(0.0f, 0.0f, radius)) {
		}

		public CircleCollider SetCircle(Circle circle) {
			Circle = circle;
			InvalidateCache();
			return this;
		}

		public Circle GetTransformedCircleCached() {
			if (!transformedCircleCache.HasData) {
				var matrix = GameObject.Transform.GetTransformationMatrixCached(false);
				var center = FastVector2Transform.Transform(Circle.CenterX, Circle.CenterY, matrix);
				transformedCircleCache.Data = new Circle(center.X, center.Y, Circle.Radius);
			}
			return transformedCircleCache.Data;
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
			transformedCircleCache.Invalidate();
		}

		public void Render() {
			if (FrameworkDebugMode.IsEnabled) {
				var matrix = GameObject.Transform.GetTransformationMatrixCached(!GameObject.IsUiElement);
				var centerT = FastVector2Transform.Transform(Circle.CenterX, Circle.CenterY, matrix);
				var radiusT = Circle.Radius; // NOTE We need to transform this with scaling later

				GL.Color4(Color.Red);
				GL.Begin(PrimitiveType.LineStripAdjacency);
				for (var angle = 0.0f; angle <= 360.0f; angle += 0.2f) {
					GL.Vertex2(
						centerT.X + Math.Sin(angle) * radiusT,
						centerT.Y + Math.Cos(angle) * radiusT);
				}
				GL.End();
			}
		}
	}

}
