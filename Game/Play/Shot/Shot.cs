using System.Drawing;
using Framework;
using Framework.Collision.Collider;
using Framework.ParticleSystem;
using Framework.Render;
using OpenTK;

namespace SpaceWar.Game.Play.Shot {

	public class Shot : GameObject {

		// Logic constants
		public const float SHOT_SPEED = 0.8f;

		// Visual constants
		public const float SHOT_SIZE = 0.025f;
		public static readonly Color SHOT_COLOR = Color.Brown;

		public Player.Player OwningPlayer { get; }

		private readonly Vector2 initialPosition;

		public Shot(float direction, Vector2 position, Player.Player owningPlayer) {
			OwningPlayer = owningPlayer;
			initialPosition = position;

			AddComponent(new ShotMovementController(direction));
			AddComponent(new ShotCollisionController());
			AddComponent(new RenderCircleComponent(SHOT_SIZE / 2).Fill(SHOT_COLOR));
			AddComponent(new ParticleSystemComponent(new ShotParticleEmitter()));
			AddComponent(new CircleCollider(SHOT_SIZE / 2));
		}

		public override void OnStart() {
			base.OnStart();
			Transform.WorldPosition = initialPosition;
		}
	}

}
