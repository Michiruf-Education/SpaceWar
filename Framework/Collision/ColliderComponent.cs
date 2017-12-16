using System;
using Framework.Collision.CollisionCalculation;
using Framework.Debug;
using Framework.Object;

namespace Framework.Collision {

	public class ColliderComponent : Component, RenderComponent {

		public ColliderShape Shape { get; set; }

		private readonly CachedObject<object> transformedShapeCached = new CachedObject<object>();

		public ColliderComponent(ColliderShape shape) {
			Shape = shape;
		}

		public bool CollidesWith(ColliderComponent other) {
			return CollisionCalculator.UnrotatedIntersects(GetTransformedShape(), other.GetTransformedShape());
		}

		public void UndoOverlap(ColliderComponent other) {
//			throw new NotImplementedException();
		}

		public object GetTransformedShape() {
			if (!transformedShapeCached.HasData) {
				transformedShapeCached.Data = Shape.GetTransformedShape(GameObject.Transform);
			}
			return transformedShapeCached.Data;
		}


		// NOTE Use this to upgrad the performance when clustering 
		// -> make abstract and implement simple calculation of bounds where the element cannot exceed the size
		public virtual void GetEstimatedShapeBounds() {
		}

		public void Invalidate() {
			transformedShapeCached.Invalidate();
		}

		public void Render() {
			if (FrameworkDebugMode.IsEnabled) {
				Shape.DebugRender(GameObject.Transform, GameObject.IsUiElement);
			}
		}
	}

}
