using Framework;
using Framework.Object;
using OpenTK;
using PlayerT = SpaceWar.Game.Play.Player.Player;

namespace SpaceWar.Game.Play.Enemy {

	public class EnemyMovementController : Component, UpdateComponent {

		private PlayerT player;

		public void Update() {
			// TODO Do this somewhere else
			if (player == null)
				player = Scene.Current.GetGameObject<PlayerT>();

			//GameObject.Transform.WorldPosition = Vector2.Lerp(
			//	GameObject.Transform.WorldPosition,
			//	player.Transform.WorldPosition,
			//	Enemy.ENEMY_SPEED);

			var targetDirection = player.Transform.WorldPosition - GameObject.Transform.WorldPosition;
			targetDirection.Normalize();
			GameObject.Transform.WorldPosition += targetDirection * Enemy.ENEMY_SPEED * Time.DeltaTime;
		}
	}

}
