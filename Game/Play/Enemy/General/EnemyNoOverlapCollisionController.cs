using Framework;
using Framework.Collision;

namespace SpaceWar.Game.Play.Enemy.General {

	public class EnemyNoOverlapCollisionController : Component, CollisionComponent {

		private AbstractEnemy enemy;

		public override void OnStart() {
			base.OnStart();
			enemy = GameObject as AbstractEnemy;
		}

		public void OnCollide(GameObject other) {
			// Cancel if the enemy is currently spawning
			if (!enemy.IsSpawned) {
				return;
			}

			switch (other) {
				case AbstractEnemy _:
					// Enemies should not overlap
					GetComponent<ColliderComponent>().UndoOverlap(other.GetComponent<ColliderComponent>());
					break;
			}
		}
	}

}
