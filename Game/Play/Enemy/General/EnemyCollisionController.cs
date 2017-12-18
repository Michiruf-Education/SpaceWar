using System;
using Framework;
using Framework.Collision;
using Framework.Debug;

namespace SpaceWar.Game.Play.Enemy.General {

	public class EnemyCollisionController : Component, CollisionComponent {

		private AbstractEnemy enemy;

		public override void OnStart() {
			base.OnStart();
			enemy = GameObject as AbstractEnemy;
		}

		public void OnCollide(GameObject other) {
			// OVERKILL avoidance:
			// Avoid duplicate shots hit the enemy and when the enemy is spawning it shell not get killed
			if (!enemy.IsAlive || !enemy.IsSpawned) {
				return;
			}

			FrameworkDebug.LogCollision(DateTime.Now + ":" + DateTime.Now.Millisecond + " " + enemy.GetType().Name +
			                            " collision with " + other.GetType().Name);

			switch (other) {
				case Shot.Shot shot:
					shot.OwningPlayer.Attributes.OnEnemyKill(enemy);
					Scene.Current.Destroy(GameObject); // For now, destroy it immediately
					Scene.Current.Destroy(shot); // For now, destroy also the shot
					break;
			}
		}
	}

}
