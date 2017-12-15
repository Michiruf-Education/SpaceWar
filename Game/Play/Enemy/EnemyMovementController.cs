using Framework;
using Framework.Object;
using PlayerT = SpaceWar.Game.Play.Player.Player;

namespace SpaceWar.Game.Play.Enemy {

	public class EnemyMovementController : Component, UpdateComponent {

		private PlayerT player;

		public override void OnStart() {
			base.OnStart();
			player = Scene.Current.GetGameObject<PlayerT>();
		}

		public void Update() {
			var targetDirection = player.Transform.WorldPosition - GameObject.Transform.WorldPosition;
			targetDirection.Normalize();
			GameObject.Transform.Translate(targetDirection * Enemy.ENEMY_SPEED * Time.DeltaTime, Space.World);
			GameObject.Transform.LookAt(player.Transform.WorldPosition);
		}
	}

}
