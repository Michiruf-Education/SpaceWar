using Framework;
using Framework.Object;
using SpaceWar.Game.Play.Player;
using PlayerT = SpaceWar.Game.Play.Player.Player;

namespace SpaceWar.Game.Play.Enemy.General {

	public class EnemyLinearFollowNearestPlayerMovementController : Component, UpdateComponent {

		private readonly float speed;

		private AbstractEnemy enemy;

		public EnemyLinearFollowNearestPlayerMovementController(float speed) {
			this.speed = speed;
		}

		public override void OnStart() {
			base.OnStart();
			enemy = GameObject as AbstractEnemy;
		}
		
		// TODO Sometimes enemies get "thrown away heavily", check for position and may destroy and spawn new one?!
		// TODO Test this case again!!!

		public void Update() {
			var thisPosition = GameObject.Transform.WorldPosition;
			var player = PlayerHelper.GetNearestPlayer(thisPosition);
			// Fail safety
			if (player == null) {
				return;
			}
			var playerPosition = player.Transform.WorldPosition;

			// Move only if the enemy is spawned
			if (enemy.IsSpawned) {
				var targetDirection = playerPosition - thisPosition;
				targetDirection.Normalize();
				GameObject.Transform.Translate(targetDirection * speed * Time.DeltaTime, Space.World);
			}
			// Always look to the player, not only if spawned
			GameObject.Transform.LookAt(playerPosition);
		}
	}

}
