using System;
using System.Drawing;
using Framework.Algorithms;
using OpenTK.Graphics.OpenGL;
using Framework.Object;
using Zenseless.Geometry;

namespace Framework.Render {

	public class RenderCircleComponent : Component, RenderComponent {

		private readonly PointF center;
		private readonly float radius;

		private Color fillColor = Color.Empty;

		public RenderCircleComponent(Circle circle) : this(
			new PointF(circle.CenterX, circle.CenterY),
			circle.Radius) {
		}

		public RenderCircleComponent(float radius) : this(
			PointF.Empty,
			radius) {
		}

		public RenderCircleComponent(PointF center, float radius) {
			this.center = center;
			this.radius = radius;
		}

		public RenderCircleComponent Fill(Color fill) {
			fillColor = fill;
			return this;
		}

		public void Render() {
			var matrix = GameObject.Transform.GetTransformationMatrixCached(!GameObject.IsUiElement);
			var centerT = FastVector2Transform.Transform(center.X, center.Y, matrix);
			var radiusT = radius; // NOTE we need to transform this with scaling later

			// Render filling
			if (fillColor != Color.Empty) {
				GL.Color4(fillColor);
				GL.Begin(PrimitiveType.TriangleFan);
				for (float angle = 1.0f; angle < 361.0f; angle += 0.2f) {
					GL.Vertex2(
						centerT.X + Math.Sin(angle) * radiusT,
						centerT.Y + Math.Cos(angle) * radiusT);
				}
				GL.End();
			}
		}
	}

}
