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
