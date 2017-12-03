using System;
using System.Drawing;
using Framework;
using Framework.Collision;
using Framework.Render;
using OpenTK;
using Zenseless.Geometry;

namespace SpaceWar.Game.Play.Shot {

	public class Shot : GameObject {

		// Logic constants
		public const float SHOT_SPEED = 0.7f;

		// Visual constants
		public const float SHOT_SIZE = 0.01f;

		public Shot(float direction, Vector2 position, Action onEnemyHit) {
			AddComponent(new ShotMovementController(direction));
			AddComponent(new ShotCollisionController(onEnemyHit));
			//AddComponent(new RenderBoxComponent(SHOT_SIZE, SHOT_SIZE).Fill(Color.Brown));
			//AddComponent(new BoxCollider(SHOT_SIZE, SHOT_SIZE));
			AddComponent(new RenderCircleComponent(SHOT_SIZE).Fill(Color.Brown));
			AddComponent(new CircleCollider(SHOT_SIZE));
			Transform.WorldPosition = position;
		}
	}

}
