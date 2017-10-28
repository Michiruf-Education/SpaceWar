using System;
using SpaceWar.Framework;

namespace SpaceWar.Game {

	public class DummyUpdateComponent : UpdateComponent {

		public override void Update() {
			Console.WriteLine("DummyUpdateComponent.Update() called!");
		}
	}

}
