using System;
using Framework;
using Framework.Object;
using Framework.Utilities;
using OpenTK.Input;

namespace SpaceWar.Game.Play.Player {

	public class PlayerShotController : Component, UpdateComponent {

		private readonly LimitedRateTimer shotTimer;

		public float ShotRate { get; private set; } = Player.INITIAL_SHOT_RATE;

		public PlayerShotController() {
			shotTimer = new LimitedRateTimer();
		}

		public void Update() {
			shotTimer.DoOnlyEvery(ShotRate, Shoot);
		}

		void Shoot() {
			var axis = GamePad.GetState(0).ThumbSticks.Right;

			// Skip if no inputs are given by using the correct float comparison
			if (Math.Abs(axis.X) < Options.CONTROLLER_THRESHOLD && Math.Abs(axis.Y) < Options.CONTROLLER_THRESHOLD) {
				return;
			}

			var direction = (float) Math.Atan2(axis.Y, axis.X);
			Scene.Current.Spawn(new Shot.Shot(direction, GameObject.Transform.WorldPosition));
		}
	}

}
