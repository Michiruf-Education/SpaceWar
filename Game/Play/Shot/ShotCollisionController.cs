using System;
using Framework;
using Framework.Collision;
using SpaceWar.Game.Play.Field;

namespace SpaceWar.Game.Play.Shot {

	public class ShotCollisionController : Component, CollisionComponent {

		private readonly Player.Player player;

		public ShotCollisionController() {
			player = Scene.Current.GetGameObject<Player.Player>();
		}

		public void OnCollide(GameObject other) {
			Console.WriteLine(DateTime.Now + ":" + DateTime.Now.Millisecond + " Shot collision with " + other.GetType().Name);

			switch (other) {
				case Enemy.Enemy enemy:
					Scene.Current.Destroy(enemy);
					player.Attributes.OnEnemyKill();
					break;
				case Border _:
					Scene.Current.Destroy(GameObject);
					break;
			}
		}
	}

}
