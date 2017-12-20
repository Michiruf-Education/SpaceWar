using System;
using System.Drawing;
using Framework.Utilities;
using OpenTK.Graphics.OpenGL;
using Zenseless.Geometry;

namespace Framework.Collision.Collider {

	public class CircleShape : ColliderShape {

		public Circle Circle { get; }

		public CircleShape(Circle circle) {
			Circle = circle;
		}

		public override object GetTransformedShape(Transform transform) {
			var matrix = transform.GetTransformationMatrixCached(false);
			var center = FastVector2Transform.Transform(Circle.CenterX, Circle.CenterY, matrix);
			return new Circle(center.X, center.Y, Circle.Radius);
		}

		internal override void DebugRender(Transform transform, bool isUiElement) {
			var matrix = transform.GetTransformationMatrixCached(!isUiElement);
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
