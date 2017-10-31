using System.Drawing;
using OpenTK.Graphics.OpenGL;
using SpaceWar.Framework.Algorithms;
using SpaceWar.Framework.Debug;
using SpaceWar.Framework.Object;

namespace SpaceWar.Framework.Render {

	public class RenderLineComponent : Component, RenderComponent {

		private readonly Point from;
		private readonly Point to;
		private readonly Color color;
		private readonly float lineWidth;

		public RenderLineComponent(Point from, Point to, Color color, float lineWidth = 1f) {
			this.from = from;
			this.to = to;
			this.color = color;
			this.lineWidth = lineWidth;
		}

		public void Render() {
			GL.Color4(color);
			GL.LineWidth(lineWidth);
			
			var matrix = GameObject.Transform.GetTransformationMatrixCached();
			var fromPoint = FastVector2Transform.Transform(from.X, from.X, matrix);
			var toPoint = FastVector2Transform.Transform(to.X, to.X, matrix);

			GL.Begin(PrimitiveType.Lines);
			GL.Vertex2(fromPoint);
			GL.Vertex2(toPoint);
			GL.End();

			if (FrameworkDebugMode.IsEnabled) {
				GL.Color4(Color.Red);
				GL.PointSize(lineWidth);
				GL.Begin(PrimitiveType.Points);
				GL.Vertex2(fromPoint);
				GL.Vertex2(toPoint);
				GL.End();
			}
		}
	}

}
