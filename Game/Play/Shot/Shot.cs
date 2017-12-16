using System;
using System.Drawing;
using Framework;
using Framework.Collision.Collider;
using Framework.Render;
using OpenTK;

namespace SpaceWar.Game.Play.Shot {

	public class Shot : GameObject {

		// Logic constants
		public const float SHOT_SPEED = 0.8f;

		// Visual constants
		public const float SHOT_SIZE = 0.025f;

		private readonly Vector2 position;

		public Shot(float direction, Vector2 position, Action onEnemyHit) {
			this.position = position;
			AddComponent(new ShotMovementController(direction));
			AddComponent(new ShotCollisionController(onEnemyHit));
			//AddComponent(new RenderBoxComponent(SHOT_SIZE, SHOT_SIZE).Fill(Color.Brown));
			//AddComponent(new BoxCollider(SHOT_SIZE, SHOT_SIZE));
			AddComponent(new RenderCircleComponent(SHOT_SIZE / 2).Fill(Color.Brown));
			AddComponent(new CircleCollider(SHOT_SIZE / 2));
		}

		public override void OnStart() {
			base.OnStart();
			Transform.WorldPosition = position;
		}
	}

}
