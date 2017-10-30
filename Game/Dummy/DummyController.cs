﻿using System;
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
			if (InputHandler.KeyDown(InputActions.MoveUp)) {
				GameObject.Transform.Translate(0, 0.01f);
			}
			if (InputHandler.KeyDown(InputActions.MoveDown)) {
				GameObject.Transform.Translate(0, -0.01f);
			}
			if (InputHandler.KeyDown(InputActions.MoveLeft)) {
				GameObject.Transform.Translate(-0.01f, 0);
			}
			if (InputHandler.KeyDown(InputActions.MoveRight)) {
				GameObject.Transform.Translate(0.01f, 0);
			}
		}

		public void OnCollide(GameObject other) {
			throw new NotImplementedException();
		}
	}

}
