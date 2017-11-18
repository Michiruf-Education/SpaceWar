using System.Drawing;
using OpenTK.Graphics.OpenGL;
using Framework.Debug;
using Framework.Object;
using OpenTK;
using Zenseless.Geometry;

namespace Framework.Collision {

	public class UnrotateableBoxCollider : ColliderComponent, RenderComponent {

		public readonly Box2D rect;

		public UnrotateableBoxCollider(Box2D rect) {
			this.rect = rect;
		}

		public Box2D GetBounds() {
			var boundsRect = new Box2D(rect);
			boundsRect.TransformCenter(GameObject.Transform.GetTransformationMatrixCached(false));
			return boundsRect;
		}

		public void Render() {
			if (FrameworkDebugMode.IsEnabled) {
				var bounds = GetBounds();
				var p1 = new Vector2(bounds.MinX, bounds.MinY);
				var p2 = new Vector2(bounds.MinX, bounds.MaxY);
				var p3 = new Vector2(bounds.MaxX, bounds.MaxY);
				var p4 = new Vector2(bounds.MaxX, bounds.MinY);

				GL.LineWidth(1f);
				GL.Color4(Color.Red);
				GL.Begin(PrimitiveType.LineLoop);
				GL.Vertex2(p1);
				GL.Vertex2(p2);
				GL.Vertex2(p3);
				GL.Vertex2(p4);
				GL.End();
				GL.Begin(PrimitiveType.Lines);
				GL.Vertex2(p1);
				GL.Vertex2(p3);
				GL.Vertex2(p2);
				GL.Vertex2(p4);
				GL.End();
			}
		}
	}

}
