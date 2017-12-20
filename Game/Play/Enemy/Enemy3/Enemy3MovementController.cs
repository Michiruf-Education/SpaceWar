using Framework;
using Framework.Object;
using Framework.Utilities;
using OpenTK;
using SpaceWar.Game.Play.Enemy.General;
using SpaceWar.Game.Play.Player;

namespace SpaceWar.Game.Play.Enemy.Enemy3 {

	public class Enemy3MovementController : Component, UpdateComponent {

		private readonly float speed;
		private readonly float restDuration;
		private readonly float chaseDuration;

		private AbstractEnemy enemy;

		private readonly MyTimer phaseTimer = new MyTimer();
		private bool isFirstChase = true;
		private Vector2 chaseDirection = Vector2.Zero;

		public Enemy3MovementController(float speed, float restDuration, float chaseDuration) {
			this.speed = speed;
			this.restDuration = restDuration;
			this.chaseDuration = chaseDuration;
		}

		public override void OnStart() {
			base.OnStart();
			enemy = GameObject as AbstractEnemy;
		}

		public void Update() {
			if (enemy.IsSpawned) {
				// Move only if the enemy is spawned and chase phase active
				if (chaseDirection != Vector2.Zero) {
					// Move the shizzle
					GameObject.Transform.Translate(chaseDirection * speed * Time.DeltaTime, Space.World);
					GameObject.Transform.LookAtDirection(chaseDirection);

					// Reset the chase direction after the period
					phaseTimer.DoEvery(chaseDuration, () => chaseDirection = Vector2.Zero, MyTimer.When.End);
					return;
				}

				// Set the chase direction after the perio
				phaseTimer.DoEvery(isFirstChase ? 0.1f : restDuration, () => {
					isFirstChase = false;

					// This is executed later, so we need to get the data again
					var playerNow = PlayerHelper.GetNearestPlayer(GameObject.Transform.WorldPosition);
					if (playerNow == null) {
						return;
					}
					chaseDirection = playerNow.Transform.WorldPosition - GameObject.Transform.WorldPosition;
					chaseDirection.Normalize();
				}, MyTimer.When.End);
			}

			// Look to the player if spawning or resting
			var player = PlayerHelper.GetNearestPlayer(GameObject.Transform.WorldPosition);
			if (player != null) {
				GameObject.Transform.LookAt(player.Transform.WorldPosition);
			}
		}
	}

}
