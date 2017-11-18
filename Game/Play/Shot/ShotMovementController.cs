using System;
using Framework;
using Framework.Object;

namespace SpaceWar.Game.Play.Shot {

	public class ShotMovementController : Component, UpdateComponent {

		private readonly float x;
		private readonly float y;

		public ShotMovementController(float direction) {
			x = (float) Math.Cos(direction) * Shot.SHOT_SPEED;
			y = (float) Math.Sin(direction) * Shot.SHOT_SPEED;
		}

		public void Update() {
			GameObject.Transform.Translate(x * Time.DeltaTime, y * Time.DeltaTime);
		}
	}

}
