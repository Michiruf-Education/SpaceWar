using System.Drawing;
using Framework;
using Framework.Collision;
using Framework.Render;
using Zenseless.Geometry;

namespace SpaceWar.Game.Play.Player {

	public class Player : GameObject {

		public const int MAX_LIFES = 5;

		private const float PLAYER_SIZE = 0.1f;

		public int Lifes { get; private set; } = MAX_LIFES;

		public Player() {
			AddComponent(new FollowingCameraController());
			AddComponent(new PlayerController());
			AddComponent(new RenderBoxComponent(PLAYER_SIZE, PLAYER_SIZE).Fill(Color.White));
			AddComponent(new UnrotateableBoxCollider(new Box2D(-PLAYER_SIZE / 2, -PLAYER_SIZE / 2, PLAYER_SIZE, PLAYER_SIZE)));
		}
	}

}
