using System;
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
			var mouse = Mouse.GetState();
			if (mouse.IsAnyButtonDown) {
				var mousePosition = new Vector2(mouse.X, mouse.Y);
				// TODO Mouse position is not aligned to used grid
				var bounds = GetTransformedRect();
				if (mousePosition.X >= bounds.MinX && mousePosition.X <= bounds.MaxX &&
				    mousePosition.Y >= bounds.MinY && mousePosition.X <= bounds.MaxY) {
					OnClick?.Invoke();
				}
			}
		}
	}

}
