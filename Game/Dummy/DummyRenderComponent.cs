using System;
using SpaceWar.Framework.Components;

namespace SpaceWar.Game {

	public class DummyRenderComponent : RenderComponent {

		public override void Render() {
			Console.WriteLine("DummyRenderComponent.Render() called!");
		}
	}

}
