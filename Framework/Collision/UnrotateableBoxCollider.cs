using System.Drawing;
using OpenTK.Graphics.OpenGL;
using Framework.Debug;
using Framework.Helper;
using Framework.Object;
using Zenseless.Geometry;

namespace Framework.Collision {

	public class UnrotateableBoxCollider : ColliderComponent, RenderComponent {

		public readonly Box2D rect;

		public UnrotateableBoxCollider(Box2D rect) {
			this.rect = rect;
		}

		public Box2D GetBounds() {
			var matrix = GameObject.Transform.GetTransformationMatrixCached();
			var p1 = FastVector2Transform.Transform(rect.MinX, rect.MinX, matrix);
			var p3 = FastVector2Transform.Transform(rect.MaxY, rect.MaxY, matrix);
			return new Box2D(p1.X, p1.Y, p3.X - p1.X, p3.Y - p1.Y);
		}

		public void Render() {
			if (FrameworkDebugMode.IsEnabled) {
				var matrix = GameObject.Transform.GetTransformationMatrixCached();
				var p1 = FastVector2Transform.Transform(rect.MinX, rect.MinX, matrix);
				var p2 = FastVector2Transform.Transform(rect.MinX, rect.MaxY, matrix);
				var p3 = FastVector2Transform.Transform(rect.MaxY, rect.MaxY, matrix);
				var p4 = FastVector2Transform.Transform(rect.MaxY, rect.MinY, matrix);
				
				GL.LineWidth(1f);
				GL.Color4(Color.Red);
				GL.Begin(PrimitiveType.LineLoop);
				GL.Vertex2(p1);
				GL.Vertex2(p2);
				GL.Vertex2(p3);
				GL.Vertex2(p4);
				GL.End();
			}
		}
	}

}
