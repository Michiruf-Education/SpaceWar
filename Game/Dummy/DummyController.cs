using System;
using SpaceWar.Framework;
using SpaceWar.Framework.Collision;
using SpaceWar.Framework.Object;

namespace SpaceWar.Game {

	public class DummyController : Component, UpdateComponent, CollisionComponent {

		public void Update() {
			Console.WriteLine("DummyController.Update() called!");

			// TODO
			GameObject.Transform.Translate(0.001f, 0.001f);
		}

		public void OnCollide(GameObject other) {
			throw new NotImplementedException();
		}
	}

}
