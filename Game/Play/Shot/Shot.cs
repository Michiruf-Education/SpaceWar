using Framework;
using Framework.Collision.Collider;
using Framework.Render;
using Framework.Sound;
using OpenTK;
using SpaceWar.Resources;

namespace SpaceWar.Game.Play.Shot {

	public class Shot : GameObject {

		// Logic constants
		public const float SHOT_SPEED = 0.8f;

		// Visual constants
		public const float SHOT_SIZE = 0.025f;

		public Player.Player OwningPlayer { get; }

		private readonly float rotation;
		private readonly Vector2 initialPosition;

		public Shot(float direction, Vector2 position, Player.Player owningPlayer) {
			OwningPlayer = owningPlayer;
			initialPosition = position;
			rotation = MathHelper.RadiansToDegrees(direction);

			AddComponent(new ShotMovementController(direction));
			AddComponent(new ShotCollisionController());
			AddComponent(new RenderTextureComponent("Shot", () => Resource.Shot,
				SHOT_SIZE * 2, SHOT_SIZE * 2));
			AddComponent(new CircleCollider(SHOT_SIZE / 1.5f));
		}

		public override void OnStart() {
			base.OnStart();
			Transform.WorldPosition = initialPosition;
			Transform.WorldRotation = rotation;
		}
	}

}
