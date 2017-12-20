using Framework.Collision.CollisionCalculation;
using Framework.Debug;
using Framework.Object;

namespace Framework.Collision {

	public class ColliderComponent : Component, RenderComponent {

		public ColliderShape Shape { get; set; }

		private readonly CachedObject<object> transformedShapeCached = new CachedObject<object>();
		private bool undoOverlapDone;

		public ColliderComponent(ColliderShape shape) {
			Shape = shape;
		}

		public bool CollidesWith(ColliderComponent other) {
			return CollisionCalculator.UnrotatedIntersects(GetTransformedShape(), other.GetTransformedShape());
		}

		public void UndoOverlap(ColliderComponent other, bool forceDuplicateUndo = false) {
			if (!forceDuplicateUndo && (undoOverlapDone || other.undoOverlapDone)) {
				return;
			}
			undoOverlapDone = true;
			var distance = CollisionCalculator.UnrotatedOverlap(GetTransformedShape(), other.GetTransformedShape());
			GameObject.Transform.Translate(distance, Space.World);
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
			undoOverlapDone = false;
		}

		public void Render() {
			if (FrameworkDebug.DrawColliders) {
				Shape.DebugRender(GameObject.Transform, GameObject.IsUiElement);
			}
		}
	}

}
