using System;
using Framework;
using Framework.Collision;
using SpaceWar.Game.Play.Field;
using PlayerT = SpaceWar.Game.Play.Player.Player;

namespace SpaceWar.Game.Play.Shot {

	public class ShotCollisionController : Component, CollisionComponent {

		private readonly Action onEnemyHit;

		public ShotCollisionController(Action onEnemyHit) {
			this.onEnemyHit = onEnemyHit;
		}

		public void OnCollide(GameObject other) {
			Console.WriteLine(DateTime.Now + ":" + DateTime.Now.Millisecond + " Shot collision with " + other.GetType().Name);

			switch (other) {
				case Enemy.Enemy enemy:
					Scene.Current.Destroy(GameObject); // For now, destroy also the shot
					Scene.Current.Destroy(enemy);
					onEnemyHit?.Invoke();
					break;
				case Border _:
					Scene.Current.Destroy(GameObject);
					break;
			}
		}
	}

}
