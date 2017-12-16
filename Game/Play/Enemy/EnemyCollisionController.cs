using Framework;
using Framework.Collision;

namespace SpaceWar.Game.Play.Enemy {

	public class EnemyCollisionController : Component, CollisionComponent {

		public void OnCollide(GameObject other) {
			if (!(other is Enemy)) {
				return;
			}

			// Enemies should not overlap
			GetComponent<ColliderComponent>().UndoOverlap(other.GetComponent<ColliderComponent>());
		}
	}

}
