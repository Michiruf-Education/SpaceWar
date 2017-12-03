using System;
using Framework;
using Framework.Collision;
using SpaceWar.Game.Play.Field;
using Zenseless.Geometry;

namespace SpaceWar.Game.Play.Player {

	public class PlayerCollisionController : Component, CollisionComponent {

		private Player player;

		public void OnCollide(GameObject other) {
			Console.WriteLine(DateTime.Now + ":" + DateTime.Now.Millisecond + " Player collision with " + other.GetType().Name);

			switch (other) {
				// Deny going threw borders
				case Border border:
					var previousBox = GetComponent<BoxCollider>().GetTransformedRectCached();
					var thisBox = new Box2D(previousBox);
					var otherBox = border.GetComponent<BoxCollider>().GetTransformedRectCached();

					thisBox.UndoOverlap(otherBox);
					var diffX = thisBox.MinX - previousBox.MinX;
					var diffY = thisBox.MinY - previousBox.MinY;
					// TODO Currently causes the cube to "lag" agains the borders, because it may get not moved inside the 
					// borders. Why is currently unknown
					// It also happens if the directions to

					GameObject.Transform.Translate(diffX, diffY);
					break;
				// Damage the player if it hits an enemy
				case Enemy.Enemy enemy:
					// TODO Do this somewhere else
					if (player == null)
						player = Scene.Current.GetGameObject<Player>();

					Scene.Current.Destroy(enemy);
					player.Attributes.Damage();
					break;
			}
		}
	}

}
