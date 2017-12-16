namespace Framework.Collision {

	public abstract class ColliderShape {

		public abstract object GetTransformedShape(Transform transform);

		internal abstract void DebugRender(Transform transform, bool isUiElement);
	}

}
