using Framework;
using Framework.Object;
using PlayerT = SpaceWar.Game.Play.Player.Player;

namespace SpaceWar.Game.Play.Enemy {

	public class EnemyMovementController : Component, UpdateComponent {

		private PlayerT player;

		public void Update() {
			// TODO Do this somewhere else
			if (player == null)
				player = Scene.Current.GetGameObject<PlayerT>();

			var targetDirection = player.Transform.WorldPosition - GameObject.Transform.WorldPosition;
			targetDirection.Normalize();
			GameObject.Transform.WorldPosition += targetDirection * Enemy.ENEMY_SPEED * Time.DeltaTime;
		}
	}

}
