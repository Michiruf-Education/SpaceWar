using System;
using Framework;
using Framework.Collision;
using SpaceWar.Game.Play.Field;
using PlayerT = SpaceWar.Game.Play.Player.Player;

namespace SpaceWar.Game.Play.Shot {

	public class ShotCollisionController : Component, CollisionComponent {

		public void OnCollide(GameObject other) {
			Console.WriteLine(DateTime.Now + ":" + DateTime.Now.Millisecond + " Shot collision with " + other.GetType().Name);

			switch (other) {
				case Border _:
					Scene.Current.Destroy(GameObject);
					break;
			}
		}
	}

}
