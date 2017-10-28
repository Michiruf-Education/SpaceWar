using System.Drawing;
using OpenTK.Graphics.OpenGL;
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

			GL.Begin(PrimitiveType.Lines);
			GL.Vertex2(from.X, from.Y);
			GL.Vertex2(to.X, to.Y);
			GL.End();

			if (FrameworkDebugMode.IsEnabled) {
				GL.Color4(Color.Red);
				GL.PointSize(lineWidth);
				GL.Begin(PrimitiveType.Points);
				GL.Vertex2(from.X, from.Y);
				GL.Vertex2(to.X, to.Y);
				GL.End();
			}
		}
	}

}
