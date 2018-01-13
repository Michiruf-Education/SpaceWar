using System.Drawing;
using Framework;
using Framework.Collision.Collider;
using Framework.Render;
using OpenTK;
using Zenseless.Geometry;

namespace SpaceWar.Game.Play.Field {

	public class Border : GameObject {

		private readonly float x;
		private readonly float y;

		public Border(float fieldWidth, float fieldHeight, float borderWidth, float padding, Position position) {
			Box2D renderBox = null;
			Box2D collisionBox = null;
			switch (position) {
				case Position.Left:
					x = -fieldWidth / 2;
					y = 0;
					renderBox = new Box2D(-borderWidth / 2, -fieldHeight / 2, borderWidth, fieldHeight);
					collisionBox = new Box2D(
						-borderWidth / 2 - padding,
						-fieldHeight / 2 - padding / 2,
						borderWidth + padding,
						fieldHeight + padding);
					break;
				case Position.Top:
					x = 0;
					y = fieldHeight / 2;
					renderBox = new Box2D(-fieldWidth / 2, -borderWidth / 2, fieldWidth, borderWidth);
					collisionBox = new Box2D(
						-fieldWidth / 2 - padding / 2,
						-borderWidth / 2,
						fieldWidth + padding,
						borderWidth + padding);
					break;
				case Position.Right:
					x = fieldWidth / 2;
					y = 0;
					renderBox = new Box2D(-borderWidth / 2, -fieldHeight / 2, borderWidth, fieldHeight);
					collisionBox = new Box2D(
						-borderWidth / 2,
						-fieldHeight / 2 - padding / 2,
						borderWidth + padding,
						fieldHeight + padding);
					break;
				case Position.Bottom:
					x = 0;
					y = -fieldHeight / 2;
					renderBox = new Box2D(-fieldWidth / 2, -borderWidth / 2, fieldWidth, borderWidth);
					collisionBox = new Box2D(
						-fieldWidth / 2 - padding / 2,
						-borderWidth / 2 - padding,
						fieldWidth + padding,
						borderWidth + padding);
					break;
			}

			if (GameDebug.ShaderDisabled) {
				AddComponent(new RenderBoxComponent(renderBox).Fill(Color.White));
			} else {
				AddComponent(new BorderShader(renderBox));
			}

			AddComponent(new BoxCollider(collisionBox));
		}

		public override void OnStart() {
			base.OnStart();
			Transform.WorldPosition = new Vector2(x, y);
		}

		public enum Position {

			Left,
			Top,
			Right,
			Bottom
		}
	}

}
