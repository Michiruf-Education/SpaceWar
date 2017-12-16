using Framework;
using Framework.Object;
using SpaceWar.Game.Play.Player;
using PlayerT = SpaceWar.Game.Play.Player.Player;

namespace SpaceWar.Game.Play.Enemy {

	public class EnemyMovementController : Component, UpdateComponent {

		public void Update() {
			var thisPosition = GameObject.Transform.WorldPosition;
			var player = PlayerHelper.GetNearestPlayer(thisPosition);
			var playerPosition = player.Transform.WorldPosition;
			var targetDirection = playerPosition - thisPosition;
			targetDirection.Normalize();
			GameObject.Transform.Translate(targetDirection * Enemy.ENEMY_SPEED * Time.DeltaTime, Space.World);
			GameObject.Transform.LookAt(playerPosition);
		}
	}

}
