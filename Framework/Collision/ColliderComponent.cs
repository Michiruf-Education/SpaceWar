namespace Framework.Collision {

	public abstract class ColliderComponent : Component {

		public abstract bool CollidesWith(ColliderComponent other);

		// NOTE Use this to upgrad the performance when clustering 
		// -> make abstract and implement simple calculation of bounds where the element cannot exceed the size
		public virtual void GetEstimatedBounds() {
		}

		public void UndoOverlap() {
			// TODO
		}

		public abstract void InvalidateCache();
	}

}
