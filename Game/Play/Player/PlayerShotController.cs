using System;
using Framework;
using Framework.Input;
using Framework.Object;

namespace SpaceWar.Game.Play.Player {

	public class PlayerShotController : Component, UpdateComponent {

		public void Update() {
			var x = InputHandler.GetAxis();
			var y = InputHandler.GetAxis();

			// Skip if no inputs are given by using the correct float comparison
			if (Math.Abs(x) < 0.001 && Math.Abs(y) < 0.001) {
				return;
			}

			var direction = (float) Math.Atan2(y, x);
			var shot = new Shot.Shot(direction);
//			shot.Transform.Translate();
			Scene.Current.Spawn(shot);
			
			// TODO We need a handler to allow only n actions per time interval
		}
	}

}
