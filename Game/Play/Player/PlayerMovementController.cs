using System;
using Framework;
using Framework.Object;
using OpenTK;
using OpenTK.Input;

namespace SpaceWar.Game.Play.Player {

	public class PlayerMovementController : Component, UpdateComponent {

		private Player player;
		
		public bool IsMoving { get; private set; }

		public override void OnStart() {
			base.OnStart();
			player = GameObject as Player;
		}

		public void Update() {
			IsMoving = false;
			
			// Do nothing if dead
			if (!player.Attributes.IsAlive) {
				return;
			}

			// NOTE For any reason, we could still clip through the border in top-right corner
			// when moving top right. This should be fixed better in future improvements!
			FixPlayerPosition();

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
					keyboardAxis.Normalize();
					GameObject.Transform.Translate(
						keyboardAxis.X * Player.INITIAL_SPEED * Time.DeltaTime,
						keyboardAxis.Y * Player.INITIAL_SPEED * Time.DeltaTime,
						Space.World);
					GameObject.Transform.LookAtDirection(keyboardAxis);
					IsMoving = true;

					// Do not detect controller if keyboard was pressed
					FixPlayerPosition();
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
				GameObject.Transform.LookAtDirection(gamepadAxis);
				IsMoving = true;
			}

			FixPlayerPosition();
		}

		private void FixPlayerPosition() {
			// NOTE Would not be needed because of collision detection
			// But for now disable clipping threw borders (bottom-left edge for example)
			var worldPosition = GameObject.Transform.WorldPosition;
			float maxWidth = (PlayScene.FIELD_WIDTH - Player.COLLIDER_SIZE - PlayScene.BORDER_WIDTH) / 2;
			if (worldPosition.X > maxWidth) {
				GameObject.Transform.WorldPosition = new Vector2(
					maxWidth,
					worldPosition.Y);
			}
			if (worldPosition.X < -maxWidth) {
				GameObject.Transform.WorldPosition = new Vector2(
					-maxWidth,
					worldPosition.Y);
			}
			float maxHeight = (PlayScene.FIELD_HEIGHT - Player.COLLIDER_SIZE - PlayScene.BORDER_WIDTH) / 2;
			if (worldPosition.Y > maxHeight) {
				GameObject.Transform.WorldPosition = new Vector2(
					worldPosition.X,
					maxHeight);
			}
			if (worldPosition.Y < -maxHeight) {
				GameObject.Transform.WorldPosition = new Vector2(
					worldPosition.X,
					-maxHeight);
			}
		}
	}

}
