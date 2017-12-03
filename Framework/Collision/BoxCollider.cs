using System;
using System.Drawing;
using Framework.Algorithms.CollisionDetection.Calculation;
using Framework.Debug;
using Framework.Object;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using Zenseless.Geometry;

namespace Framework.Collision {

	public class BoxCollider : ColliderComponent, RenderComponent {

		public Box2D Rect { get; set; }
		public float Rotation { get => 0; set => throw new ToDevelopException("Rotation not yet implemented"); }

		private bool transformedRectIsCached;
		private Box2D transformedRectCached;

		public BoxCollider(Box2D rect) {
			SetRect(rect);
		}

		public BoxCollider(float width, float height) :
			this(new Box2D(-width / 2, -height / 2,
				width, height)) {
		}

		public BoxCollider(Box2D rect, float rotation) {
			SetRect(rect);
			SetRotation(rotation);
		}

		public BoxCollider SetRect(Box2D rect) {
			Rect = rect;
			return this;
		}

		public BoxCollider SetRotation(float rotation) {
			Rotation = rotation;
			return this;
		}

		public Box2D GetTransformedRectCached() {
			// TODO If the "true ||" check is not present,
			// we get the "flickering" effect when running agains a wall
			if (true || !transformedRectIsCached) {
				transformedRectCached = new Box2D(Rect);
				transformedRectCached.TransformCenter(GameObject.Transform.GetTransformationMatrixCached(false));
				transformedRectIsCached = true;
			}

			return transformedRectCached;
		}

		public override bool CollidesWith(ColliderComponent other) {
			switch (other) {
				case BoxCollider boxCollider:
					return GetTransformedRectCached().Intersects(boxCollider.GetTransformedRectCached());
				case CircleCollider circleCollider:
					return BoxCircleCollisionCalculator.UnrotatedIntersects(
						circleCollider.GetTransformedCircleCached(), GetTransformedRectCached());
				default:
					throw new ArgumentException("Other component for collision detection is of type " +
					                            other.GetType() + " which is not implemented yet!");
			}
		}

		public override void InvalidateCache() {
			transformedRectIsCached = false;
			transformedRectCached = null;
		}		
		
		public void Render() {
			if (FrameworkDebugMode.IsEnabled) {
				var bounds = GetTransformedRectCached();
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
