using System;
using SpaceWar.Framework;
using SpaceWar.Framework.Components;

namespace SpaceWar.Game {

	public class DummyUpdateComponent : UpdateComponent {

		public override void Update() {
			Console.WriteLine("DummyUpdateComponent.Update() called!");
		}
	}

}
