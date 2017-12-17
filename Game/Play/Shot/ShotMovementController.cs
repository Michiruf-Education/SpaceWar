using System;
using Framework;
using Framework.Object;
using OpenTK;

namespace SpaceWar.Game.Play.Shot {

	public class ShotMovementController : Component, UpdateComponent {

		private readonly float direction;
		private readonly float x;
		private readonly float y;

		public ShotMovementController(float direction) {
			this.direction = direction;
			x = (float) Math.Cos(direction) * Shot.SHOT_SPEED;
			y = (float) Math.Sin(direction) * Shot.SHOT_SPEED;
		}

		public override void OnStart() {
			base.OnStart();
			GameObject.Transform.WorldRotation = MathHelper.DegreesToRadians(direction);
		}

		public void Update() {
			GameObject.Transform.Translate(x * Time.DeltaTime, y * Time.DeltaTime, Space.World);
		}
	}

}
