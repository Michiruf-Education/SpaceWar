using System;
using Framework;
using Framework.Collision;

namespace SpaceWar.Game.Play.Enemy.General {

	public class EnemyCollisionController : Component, CollisionComponent {

		private AbstractEnemy enemy;

		public override void OnStart() {
			base.OnStart();
			enemy = GameObject as AbstractEnemy;
		}

		public void OnCollide(GameObject other) {
			Console.WriteLine(DateTime.Now + ":" + DateTime.Now.Millisecond + " Enemy collision with " + other.GetType().Name);

			switch (other) {
				case Shot.Shot shot:
					shot.OwningPlayer.Attributes.OnEnemyKill(enemy.PointsForKilling);
					Scene.Current.Destroy(GameObject); // For now, destroy it immediately
					Scene.Current.Destroy(shot); // For now, destroy also the shot
					break;
			}
		}
	}

}
