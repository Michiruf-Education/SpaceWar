using System.Drawing;
using Framework;
using Framework.Collision;
using Framework.Render;
using Zenseless.Geometry;

namespace SpaceWar.Game.Player {

	public class Player : GameObject {

		const float SIZE = 0.1f;

		public Player() {
			AddComponent(new FollowingCameraController());
			AddComponent(new PlayerController());
			AddComponent(new RenderBoxComponent(SIZE, SIZE).Fill(Color.White));
			AddComponent(new UnrotateableBoxCollider(new Box2D(-SIZE / 2, -SIZE / 2, SIZE, SIZE)));
		}
	}

}
