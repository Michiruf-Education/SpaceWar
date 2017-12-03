using System;
using Framework;
using Framework.Input;
using Framework.Object;
using Framework.Utilities;
using OpenTK;
using OpenTK.Input;

namespace SpaceWar.Game.Play.Player {

	public class PlayerShotController : Component, UpdateComponent {

		private readonly LimitedRateTimer shotTimer = new LimitedRateTimer();

		public float ShotRate { get; private set; } = Player.INITIAL_SHOT_RATE;

		public void Update() {
			shotTimer.DoOnlyEvery(ShotRate, Shoot);
		}

		void Shoot() {
			var axis = GamePad.GetState(0).ThumbSticks.Right;

			// Skip if no inputs are given by using the correct float comparison
			if (Math.Abs(axis.X) < Options.CONTROLLER_THRESHOLD && Math.Abs(axis.Y) < Options.CONTROLLER_THRESHOLD) {
				//return;
				goto Temp;
			}

			var direction = (float) Math.Atan2(axis.Y, axis.X);
			Scene.Current.Spawn(new Shot.Shot(direction, GameObject.Transform.WorldPosition, 
				// TODO
				() => (GameObject as Player).Attributes.OnEnemyKill()));


			Temp:
			// Temporary keyboard controls
			var simpleAxis = Vector2.Zero;
			if (InputHandler.KeyDown(InputActions.SimpleFireUp)) {
				simpleAxis.Y++;
			}
			if (InputHandler.KeyDown(InputActions.SimpleFireDown)) {
				simpleAxis.Y--;
			}
			if (InputHandler.KeyDown(InputActions.SimpleFireLeft)) {
				simpleAxis.X--;
			}
			if (InputHandler.KeyDown(InputActions.SimpleFireRight)) {
				simpleAxis.X++;
			}
			if (simpleAxis != Vector2.Zero) {
				var simpleDirection = (float) Math.Atan2(simpleAxis.Y, simpleAxis.X);
				Scene.Current.Spawn(new Shot.Shot(simpleDirection, GameObject.Transform.WorldPosition, 
					// TODO
					() => (GameObject as Player).Attributes.OnEnemyKill()));
			}
		}
	}

}
