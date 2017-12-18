using System;
using Framework;
using Framework.Object;
using OpenTK;
using OpenTK.Input;

namespace SpaceWar.Game.Play.Player {

	public class PlayerMovementController : Component, UpdateComponent {

		private Player player;

		public override void OnStart() {
			base.OnStart();
			player = GameObject as Player;
		}

		public void Update() {
			// Do nothing if dead
			if (!player.Attributes.IsAlive) {
				return;
			}

			// Detect keyboard movements first only for the first player
			if (player.PlayerIndex == 0) {
				var keyboardAxis = Vector2.Zero;
				if (Keyboard.GetState().IsKeyDown(Key.W)) {
					keyboardAxis.Y++;
				}
				if (Keyboard.GetState().IsKeyDown(Key.S)) {
					keyboardAxis.Y--;
				}
				if (Keyboard.GetState().IsKeyDown(Key.A)) {
					keyboardAxis.X--;
				}
				if (Keyboard.GetState().IsKeyDown(Key.D)) {
					keyboardAxis.X++;
				}
				if (keyboardAxis != Vector2.Zero) {
					GameObject.Transform.Translate(
						keyboardAxis.X * Player.INITIAL_SPEED * Time.DeltaTime,
						keyboardAxis.Y * Player.INITIAL_SPEED * Time.DeltaTime,
						Space.World);
					GameObject.Transform.WorldRotation = MathHelper.RadiansToDegrees(
						(float) Math.Atan2(keyboardAxis.Y, keyboardAxis.X));

					// Do not detect controller if keyboard was pressed
					return;
				}
			}

			// Detect gamepad inputs
			var gamepadAxis = GamePad.GetState(player.PlayerIndex).ThumbSticks.Left;
			if (Math.Abs(gamepadAxis.Length) > Options.CONTROLLER_THRESHOLD) {
				GameObject.Transform.Translate(
					gamepadAxis.X * Player.INITIAL_SPEED * Time.DeltaTime,
					gamepadAxis.Y * Player.INITIAL_SPEED * Time.DeltaTime,
					Space.World);
				GameObject.Transform.WorldRotation = MathHelper.RadiansToDegrees(
					(float) Math.Atan2(gamepadAxis.Y, gamepadAxis.X));
			}
		}
	}

}
