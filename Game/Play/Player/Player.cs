using System.Drawing;
using Framework;
using Framework.Collision.Collider;
using Framework.ParticleSystem;
using Framework.Render;
using SpaceWar.Resources;

namespace SpaceWar.Game.Play.Player {

	public class Player : GameObject {

		// Logic constants
		public const int MAX_LIFES = 5;
		public const float INITIAL_SHOT_RATE = 0.15f;
		public const float INITIAL_SPEED = 0.45f;

		// Visual constants
		public const float PLAYER_SIZE = 0.05f;
		public const float COLLIDER_SIZE = PLAYER_SIZE * 0.8f;

		// Containers
		public PlayerAttributes Attributes { get; }
		public PlayerMovementController MovementController { get; }
		public PlayerShotController ShotController { get; }
		public PlayerCollisionController CollisionController { get; }
		public RenderTextureComponent RenderComponent { get; }
		public ParticleSystemComponent ParticleSystemComponent { get; }
		public CircleCollider Collider { get; }

		// Properties
		public int PlayerIndex { get; }
		public Color PlayerColor {
			get {
				switch (PlayerIndex) {
					case 1:
						return Color.LightSalmon;
					case 2:
						return Color.LightYellow;
					default:
						return Color.GreenYellow;
				}
			}
		}

		public Player(int playerIndex) {
			PlayerIndex = playerIndex;

			Attributes = new PlayerAttributes();
			MovementController = new PlayerMovementController();
			ShotController = new PlayerShotController();
			CollisionController = new PlayerCollisionController();
			RenderComponent = new RenderTextureComponent(Resource.PlayerV2, PLAYER_SIZE, PLAYER_SIZE)
				.SetColorFilter(PlayerColor);
			ParticleSystemComponent = new ParticleSystemComponent(new PlayerParticleEmitter(this));
			Collider = new CircleCollider(COLLIDER_SIZE / 2.2f);

			AddComponent(Attributes);
			AddComponent(MovementController);
			AddComponent(ShotController);
			AddComponent(CollisionController);
			AddComponent(RenderComponent);
			AddComponent(ParticleSystemComponent);
			AddComponent(Collider);
		}
	}

}
