using System;
using Framework;
using Framework.Collision;
using Framework.Debug;

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

			FrameworkDebug.LogCollision(DateTime.Now + ":" + DateTime.Now.Millisecond + " " + enemy.GetType().Name +
			                            " collision with " + other.GetType().Name);

			switch (other) {
				case AbstractEnemy _:
					// Enemies should not overlap
					GetComponent<ColliderComponent>().UndoOverlap(other.GetComponent<ColliderComponent>());
					break;
			}
		}
	}

}
