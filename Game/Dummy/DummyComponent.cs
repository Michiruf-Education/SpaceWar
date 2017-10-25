using System;
using SpaceWar.Framework;
using SpaceWar.Framework.Components;

namespace SpaceWar.Game {

	public class DummyComponent : UpdateComponent {

		public override void Update() {
			Console.WriteLine("DummyComponent.Update() called!");
		}
	}

}
