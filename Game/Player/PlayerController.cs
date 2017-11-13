using Framework;
using Framework.Collision;
using Framework.Input;
using Framework.Object;

namespace SpaceWar.Game.Player {

	public class PlayerController : Component, UpdateComponent, CollisionComponent {

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

		public void OnCollide(GameObject other) {
			// TODO
		}
	}

}
