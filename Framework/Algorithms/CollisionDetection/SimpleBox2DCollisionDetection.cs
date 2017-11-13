using Framework.Collision;

namespace Framework.Algorithms.CollisionDetection {

	public class SimpleBox2DCollisionDetection : CollisionDetection {

		public void DetectCollisions() {
			var colliders = Scene.Current.GetAllComponentsInScene<UnrotateableBoxCollider>();
			foreach (var c1 in colliders) {
				foreach (var c2 in colliders) {
					// Ignore equal gameobjects
					if (c1.GameObject == c2.GameObject) {
						continue;
					}

					if (!c1.GetBounds().Intersects(c2.GetBounds())) {
						continue;
					}
					c1.GetComponent<CollisionComponent>()?.OnCollide(c2.GameObject);
					c2.GetComponent<CollisionComponent>()?.OnCollide(c1.GameObject);
					// TODO GetComponents instead of just one?
				}
			}
		}
	}

}
