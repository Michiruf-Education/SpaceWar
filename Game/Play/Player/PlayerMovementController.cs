using Framework;
using Framework.Input;
using Framework.Object;

namespace SpaceWar.Game.Play.Player {

	public class PlayerMovementController : Component, UpdateComponent {

		const float SPEED = 0.6f;

		public void Update() {
			//

			if (InputHandler.KeyDown(InputActions.MoveUp)) {
				GameObject.Transform.Translate(0, SPEED * Time.DeltaTime, Space.World);
			}
			if (InputHandler.KeyDown(InputActions.MoveDown)) {
				GameObject.Transform.Translate(0, -SPEED * Time.DeltaTime, Space.World);
			}
			if (InputHandler.KeyDown(InputActions.MoveLeft)) {
				GameObject.Transform.Translate(-SPEED * Time.DeltaTime, 0, Space.World);
			}
			if (InputHandler.KeyDown(InputActions.MoveRight)) {
				GameObject.Transform.Translate(SPEED * Time.DeltaTime, 0, Space.World);
			}
		}
	}

}
