using System;
using System.Drawing;
using Framework.Object;
using OpenTK;
using OpenTK.Input;
using Zenseless.Geometry;

namespace Framework.Widget {

	public class BoxClickComponent : Component, UpdateComponent {

		public Box2D Rect { get; set; }
		public Action OnClick { get; set; }

		public BoxClickComponent(Box2D rect) {
			Rect = rect;
		}

		public BoxClickComponent(Box2D rect, Action onClick) : this(rect) {
			OnClick = onClick;
		}

		public Box2D GetTransformedRect() {
			var transformedRectCached = new Box2D(Rect);
			transformedRectCached.TransformCenter(GameObject.Transform.GetTransformationMatrixCached(true));
			return transformedRectCached;
		}

		public void Update() {
			var mouseDevice = Game.Instance.Window.Mouse;
			if (!mouseDevice.GetState().IsAnyButtonDown) {
				return;
			}

			var mousePositionRelativeToWindow = new Vector2(
				mouseDevice.X / (float) Game.Instance.Window.Width,
				mouseDevice.Y / (float) Game.Instance.Window.Height);

			var p = (mousePositionRelativeToWindow - new Vector2(0.5f, 0.5f)) * new Vector2(2f, 1f); // TODO Translate to world correctly
			Console.WriteLine(p);

			// TODO Mouse position is not aligned to used grid
			var bounds = GetTransformedRect();
			if (p.X >= bounds.MinX && p.X <= bounds.MaxX &&
			    p.Y >= bounds.MinY && p.X <= bounds.MaxY) {
				OnClick?.Invoke();
			}
		}
	}

}
