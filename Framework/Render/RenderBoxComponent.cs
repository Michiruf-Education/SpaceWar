using System;
using System.Drawing;
using OpenTK.Graphics.OpenGL;
using SpaceWar.Framework.Helper;
using SpaceWar.Framework.Object;
using Zenseless.Geometry;

namespace SpaceWar.Framework.Render {

	public class RenderBoxComponent : Component, RenderComponent {

		private readonly PointF p1;
		private readonly PointF p2;
		private readonly PointF p3;
		private readonly PointF p4;
		
		private Color fillColor = Color.Empty;
		private Color strokeColor = Color.Empty;
		private float strokeWidth;

		public RenderBoxComponent(float width, float height)
			: this(new Box2D(-width / 2, -height / 2, width, height)) {
		}

		public RenderBoxComponent(Box2D box) : this(
			new PointF(box.MinX, box.MinY),
			new PointF(box.MinX, box.MaxY),
			new PointF(box.MaxX, box.MaxY),
			new PointF(box.MaxX, box.MinY)) {
		}

		public RenderBoxComponent(PointF p1, PointF p2, PointF p3, PointF p4) {
			this.p1 = p1;
			this.p2 = p2;
			this.p3 = p3;
			this.p4 = p4;
		}

		public RenderBoxComponent Fill(Color fill) {
			fillColor = fill;
			return this;
		}

		public RenderBoxComponent Stroke(Color stroke, float strokeWidth) {
			strokeColor = stroke;
			this.strokeWidth = strokeWidth;
			return this;
		}

		public void Render() {
			var matrix = GameObject.Transform.GetTransformationMatrixCached();
			var p1Fill = FastVector2Transform.Transform(p1.X, p1.Y, matrix);
			var p2Fill = FastVector2Transform.Transform(p2.X, p2.Y, matrix);
			var p3Fill = FastVector2Transform.Transform(p3.X, p3.Y, matrix);
			var p4Fill = FastVector2Transform.Transform(p4.X, p4.Y, matrix);
			
			// Render filling
			if (fillColor != Color.Empty) {
				GL.Color4(fillColor);
				GL.Begin(PrimitiveType.Quads);
				GL.Vertex2(p1Fill);
				GL.Vertex2(p2Fill);
				GL.Vertex2(p3Fill);
				GL.Vertex2(p4Fill);
				GL.End();
			}

			// Render stroke / outline
			if (strokeColor != Color.Empty && Math.Abs(strokeWidth) > 0.001f) {
				// TODO The stroke is not drawed "outside" of the rectangle, but directly on the edges
				// TODO Is this good?
				GL.LineWidth(strokeWidth);
				GL.Color4(strokeColor);
				GL.Begin(PrimitiveType.LineLoop);
				GL.Vertex2(p1Fill);
				GL.Vertex2(p2Fill);
				GL.Vertex2(p3Fill);
				GL.Vertex2(p4Fill);
				GL.End();
			}

			// TODO
			//if (FrameworkDebugMode.IsEnabled) {
			//	GL.Color4(Color.Red);
			//	GL.PointSize(lineWidth);
			//	GL.Begin(PrimitiveType.Points);
			//	GL.Vertex2(fromPoint);
			//	GL.Vertex2(toPoint);
			//	GL.End();
			//}
		}

	}

}
