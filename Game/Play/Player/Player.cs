using System.Drawing;
using Framework;
using Framework.Collision;
using Framework.Collision.Collider;
using Framework.Render;

namespace SpaceWar.Game.Play.Player {

	public class Player : GameObject {

		// Logic constants
		public const int MAX_LIFES = 5;
		public const float INITIAL_SHOT_RATE = 0.15f;
		public const float INITIAL_SPEED = 0.45f;

		// Visual constants
		public const float PLAYER_SIZE = 0.05f;

		// Containers
		public PlayerAttributes Attributes { get; }
		public PlayerMovementController MovementController { get; }
		public PlayerShotController ShotController { get; }
		public PlayerCollisionController CollisionController { get; }
		public RenderBoxComponent RenderBoxComponent { get; }
		public ColliderComponent BoxCollider { get; } // TODO Change type back later

		// Properties
		public int PlayerIndex { get; }

		public Player(int playerIndex) {
			Attributes = new PlayerAttributes();
			MovementController = new PlayerMovementController();
			ShotController = new PlayerShotController();
			CollisionController = new PlayerCollisionController();
			RenderBoxComponent = new RenderBoxComponent(PLAYER_SIZE, PLAYER_SIZE).Fill(Color.White);
			BoxCollider = playerIndex % 2 == 1 ? (ColliderComponent) new BoxCollider(PLAYER_SIZE, PLAYER_SIZE) : new CircleCollider(PLAYER_SIZE / 2);

			AddComponent(Attributes);
			AddComponent(MovementController);
			AddComponent(ShotController);
			AddComponent(CollisionController);
			AddComponent(RenderBoxComponent);
			AddComponent(BoxCollider);

			PlayerIndex = playerIndex;
		}
	}

}
