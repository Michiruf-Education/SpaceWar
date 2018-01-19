using System;
using System.Drawing;
using Framework.Object;
using Framework.Utilities;
using OpenTK.Graphics.OpenGL;
using Zenseless.Geometry;

namespace Framework.Render {

	public class RenderCircleComponent : Component, RenderComponent {

		private readonly PointF center;
		private readonly float radius;
		private readonly float anglePrecision;

		private Color fillColor = Color.Empty;

		public RenderCircleComponent(Circle circle, float anglePrecision = 0.2f) :
			this(
				new PointF(circle.CenterX, circle.CenterY),
				circle.Radius,
				anglePrecision) {
		}

		public RenderCircleComponent(float radius, float anglePrecision = 0.2f) :
			this(PointF.Empty, radius, anglePrecision) {
		}

		public RenderCircleComponent(PointF center, float radius, float anglePrecision = 0.2f) {
			this.center = center;
			this.radius = radius;
			this.anglePrecision = anglePrecision;
		}

		public RenderCircleComponent Fill(Color fill) {
			fillColor = fill;
			return this;
		}

		public void Render() {
			// Enable blending for transparency
			GL.BlendFunc(BlendingFactorSrc.SrcAlpha, BlendingFactorDest.OneMinusSrcAlpha);
			GL.Enable(EnableCap.Blend);

			var matrix = GameObject?.Transform?.GetTransformationMatrixCached(!GameObject.IsUiElement) ?? 
			             System.Numerics.Matrix3x2.Identity;
			var centerT = FastVector2Transform.Transform(center.X, center.Y, matrix);
			var radiusT = radius; // NOTE We need to transform this with scaling later

			// Render filling
			if (fillColor != Color.Empty) {
				GL.Color4(fillColor);
				GL.Begin(PrimitiveType.TriangleFan);
				for (var angle = 0.0f; angle <= 360.0f; angle += anglePrecision) {
					GL.Vertex2(
						centerT.X + Math.Sin(angle) * radiusT,
						centerT.Y + Math.Cos(angle) * radiusT);
				}
				GL.End();
			}

			GL.Disable(EnableCap.Blend);
		}
	}

}
