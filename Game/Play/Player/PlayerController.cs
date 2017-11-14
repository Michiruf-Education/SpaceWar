using System;
using Framework;
using Framework.Collision;
using Framework.Input;
using Framework.Object;
using SpaceWar.Game.Play.Field;
using Zenseless.Geometry;

namespace SpaceWar.Game.Play.Player {

	public class PlayerController : Component, UpdateComponent, CollisionComponent {

		const float SPEED = 0.6f;

		public void Update() {
			//

			if (InputHandler.KeyDown(InputActions.MoveUp)) {
				GameObject.Transform.Translate(0, SPEED * Time.DeltaTime, Space.World);
			}
			if (InputHandler.KeyDown(InputActions.MoveDown)) {
				GameObject.Transform.Translate(0, -SPEED * Time.DeltaTime, Space.World);
			}
			if (InputHandler.KeyDown(InputActions.MoveLeft)) {
				GameObject.Transform.Translate(-SPEED * Time.DeltaTime, 0, Space.World);
			}
			if (InputHandler.KeyDown(InputActions.MoveRight)) {
				GameObject.Transform.Translate(SPEED * Time.DeltaTime, 0, Space.World);
			}
		}

		public void OnCollide(GameObject other) {
			Console.WriteLine(DateTime.Now + ":" + DateTime.Now.Millisecond + " Player collision with " + other.GetType().Name);

			// Deny going threw borders
			if (other is Border border) {
				var previousBox = GetComponent<UnrotateableBoxCollider>().GetBounds();
				var thisBox = new Box2D(previousBox);
				var otherBox = border.GetComponent<UnrotateableBoxCollider>().GetBounds();

				thisBox.UndoOverlap(otherBox);
				var diffX = thisBox.MinX - previousBox.MinX;
				var diffY = thisBox.MinY - previousBox.MinY;
				// TODO Currently causes the cube to "lag" agains the borders, because it may get not moved inside the 
				// borders. Why is currently unknown
				// It also happens if the directions to

				GameObject.Transform.Translate(diffX, diffY);
			}

			// Damage the player if it hits an enemy
			if (other is Enemy.Enemy enemy) {
				Console.WriteLine("COLLISION WITH ENEMY!!!");
			}
		}
	}

}
