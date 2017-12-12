using System;
using Framework;
using Framework.Object;
using OpenTK.Input;

namespace SpaceWar.Game.Play.Player {

	public class PlayerMovementController : Component, UpdateComponent {

		private Player player;

		public override void OnStart() {
			base.OnStart();
			player = GameObject as Player;
		}

		public void Update() {
			// Detect keyboard movements first only for the first player
			if (player.PlayerIndex == 0) {
				var keyboardInputDetected = false;
				if (Keyboard.GetState().IsKeyDown(Key.W)) {
					GameObject.Transform.Translate(0, Player.INITIAL_SPEED * Time.DeltaTime, Space.World);
					keyboardInputDetected = true;
				}
				if (Keyboard.GetState().IsKeyDown(Key.S)) {
					GameObject.Transform.Translate(0, -Player.INITIAL_SPEED * Time.DeltaTime, Space.World);
					keyboardInputDetected = true;
				}
				if (Keyboard.GetState().IsKeyDown(Key.A)) {
					GameObject.Transform.Translate(-Player.INITIAL_SPEED * Time.DeltaTime, 0, Space.World);
					keyboardInputDetected = true;
				}
				if (Keyboard.GetState().IsKeyDown(Key.D)) {
					GameObject.Transform.Translate(Player.INITIAL_SPEED * Time.DeltaTime, 0, Space.World);
					keyboardInputDetected = true;
				}
				if (keyboardInputDetected) {
					return;
				}
			}

			// Detect gamepad inputs
			var gamepadAxis = GamePad.GetState(player.PlayerIndex).ThumbSticks.Left;
			if (Math.Abs(gamepadAxis.X) > Options.CONTROLLER_THRESHOLD) {
				GameObject.Transform.Translate(gamepadAxis.X * Player.INITIAL_SPEED * Time.DeltaTime, 0, Space.World);
			}
			if (Math.Abs(gamepadAxis.Y) > Options.CONTROLLER_THRESHOLD) {
				GameObject.Transform.Translate(0, gamepadAxis.Y * Player.INITIAL_SPEED * Time.DeltaTime, Space.World);
			}
		}
	}

}
