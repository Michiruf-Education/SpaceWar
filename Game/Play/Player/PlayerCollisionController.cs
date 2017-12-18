using System;
using Framework;
using Framework.Collision;
using Framework.Debug;
using SpaceWar.Game.Play.Enemy.General;
using SpaceWar.Game.Play.Field;

namespace SpaceWar.Game.Play.Player {

	public class PlayerCollisionController : Component, CollisionComponent {

		private Player player;

		public override void OnStart() {
			base.OnStart();
			player = GameObject as Player;
		}

		public void OnCollide(GameObject other) {
			// Do nothing if dead
			if (!player.Attributes.IsAlive) {
				return;
			}

			FrameworkDebug.LogCollision(DateTime.Now + ":" + DateTime.Now.Millisecond + " Player collision with " + 
			                            other.GetType().Name);

			switch (other) {
				// Deny going threw borders
				case Border _:
					GetComponent<ColliderComponent>().UndoOverlap(other.GetComponent<ColliderComponent>());
					break;
				// Deny going threw other players
				case Player otherPlayer:
					if (otherPlayer.Attributes.IsAlive) {
						GetComponent<ColliderComponent>().UndoOverlap(other.GetComponent<ColliderComponent>(), true);
					}
					break;
				// Damage the player if it hits an enemy
				case AbstractEnemy enemy:
					if (!enemy.IsAlive || !enemy.IsSpawned) {
						break;
					}
					Scene.Current.Destroy(enemy);
					player.Attributes.Damage();
					break;
			}
		}
	}

}
