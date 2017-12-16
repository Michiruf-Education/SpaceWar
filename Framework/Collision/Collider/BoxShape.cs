using System.Drawing;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using Zenseless.Geometry;

namespace Framework.Collision.Collider {

	public class BoxShape : ColliderShape {

		public Box2D Rect { get; set; }

		public BoxShape(Box2D rect) {
			Rect = rect;
		}

		public override object GetTransformedShape(Transform transform) {
			var transformedRect = new Box2D(Rect);
			transformedRect.TransformCenter(transform.GetTransformationMatrixCached(false));
			return transformedRect;
		}

		internal Circle GetTransformedRect(Transform transform) {
			return GetTransformedShape(transform) as Circle;
		}

		internal override void DebugRender(Transform transform, bool isUiElement) {
			var transformedRect = new Box2D(Rect);
			transformedRect.TransformCenter(transform.GetTransformationMatrixCached(!isUiElement));
			var p1 = new Vector2(transformedRect.MinX, transformedRect.MinY);
			var p2 = new Vector2(transformedRect.MinX, transformedRect.MaxY);
			var p3 = new Vector2(transformedRect.MaxX, transformedRect.MaxY);
			var p4 = new Vector2(transformedRect.MaxX, transformedRect.MinY);

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
