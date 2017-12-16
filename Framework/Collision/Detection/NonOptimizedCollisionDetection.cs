using System.Collections.Generic;

namespace Framework.Collision.Detection {

	public class NonOptimizedCollisionDetection : CollisionDetection {

		public void DetectCollisions() {
			var colliders = Scene.Current.GetAllComponentsInScene<ColliderComponent>();
			DetectCollisions(colliders);
			InvalidateCollisionCaches(colliders);
		}

		internal void DetectCollisions(List<ColliderComponent> colliders) {
			foreach (var c1 in colliders) {
				foreach (var c2 in colliders) {
					// Ignore equal gameobjects
					if (c1.GameObject == c2.GameObject) {
						continue;
					}

					// We assume if "A" collides with "B", "B" also collides with "A"
					if (!c1.CollidesWith(c2)) {
						continue;
					}

					// Redirect found collisions
					c1.GetComponents<CollisionComponent>().ForEach(c => c.OnCollide(c2.GameObject));
					c2.GetComponents<CollisionComponent>().ForEach(c => c.OnCollide(c1.GameObject));
				}
			}
		}

		private void InvalidateCollisionCaches(List<ColliderComponent> colliders) {
			colliders?.ForEach(component => component.Invalidate());
		}
	}

}
