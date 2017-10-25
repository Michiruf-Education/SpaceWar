using System;
using SpaceWar.Framework.Components;

namespace SpaceWar.Game {

	public class DummyComponent : UpdateComponent {

		public void Update() {
			Console.WriteLine("DummyComponent.Update() called!");
		}
	}

}
