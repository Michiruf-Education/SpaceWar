using System;
using Framework;
using Framework.Input;
using Framework.Object;
using OpenTK.Input;

namespace SpaceWar.Game.Play.Player {

	public class PlayerMovementController : Component, UpdateComponent {

		public void Update() {
			var axis = GamePad.GetState(0).ThumbSticks.Left;
			if (Math.Abs(axis.X) > Options.CONTROLLER_THRESHOLD) {
				GameObject.Transform.Translate(axis.X * Player.INITIAL_SPEED * Time.DeltaTime, 0, Space.World);
			}
			if (Math.Abs(axis.Y) > Options.CONTROLLER_THRESHOLD) {
				GameObject.Transform.Translate(0, axis.Y * Player.INITIAL_SPEED * Time.DeltaTime, Space.World);
			}

			if (InputHandler.KeyDown(InputActions.MoveUp)) {
				GameObject.Transform.Translate(0, Player.INITIAL_SPEED * Time.DeltaTime, Space.World);
			}
			if (InputHandler.KeyDown(InputActions.MoveDown)) {
				GameObject.Transform.Translate(0, -Player.INITIAL_SPEED * Time.DeltaTime, Space.World);
			}
			if (InputHandler.KeyDown(InputActions.MoveLeft)) {
				GameObject.Transform.Translate(-Player.INITIAL_SPEED * Time.DeltaTime, 0, Space.World);
			}
			if (InputHandler.KeyDown(InputActions.MoveRight)) {
				GameObject.Transform.Translate(Player.INITIAL_SPEED * Time.DeltaTime, 0, Space.World);
			}
		}
	}

}
