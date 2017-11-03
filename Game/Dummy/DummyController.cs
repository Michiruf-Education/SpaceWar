using System;
using SpaceWar.Framework;
using SpaceWar.Framework.Collision;
using SpaceWar.Framework.Input;
using SpaceWar.Framework.Object;

namespace SpaceWar.Game {

	public class DummyController : Component, UpdateComponent, CollisionComponent {

		public void Update() {
			Console.WriteLine("DummyController.Update() called!");

			// TODO
			GameObject.Transform.Translate(0.001f, 0.001f);
			GameObject.Transform.Rotate(0.5f);

			if (InputHandler.KeyDown(InputActions.Fire)) {
				Console.WriteLine("FIRE!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");
			}

			if (InputHandler.KeyDown(InputActions.MoveUp)) {
				GameObject.Transform.Translate(0, 0.6f * Time.DeltaTime, Space.World);
			}
			if (InputHandler.KeyDown(InputActions.MoveDown)) {
				GameObject.Transform.Translate(0, -0.6f * Time.DeltaTime, Space.World);
			}
			if (InputHandler.KeyDown(InputActions.MoveLeft)) {
				GameObject.Transform.Translate(-0.6f * Time.DeltaTime, 0, Space.World);
			}
			if (InputHandler.KeyDown(InputActions.MoveRight)) {
				GameObject.Transform.Translate(0.6f * Time.DeltaTime, 0, Space.World);
			}
		}

		public void OnCollide(GameObject other) {
			throw new NotImplementedException();
		}
	}

}
