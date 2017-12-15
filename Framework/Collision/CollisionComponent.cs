namespace Framework.Collision {

	public interface CollisionComponent {

		// TODO Return a boolean to detemine whether the elements are allowed to overlap!
		void OnCollide(GameObject other);
	}

}
