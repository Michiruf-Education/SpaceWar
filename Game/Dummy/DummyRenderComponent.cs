using System;
using SpaceWar.Framework.Render;
using Zenseless.Geometry;

namespace SpaceWar.Game {

	public class DummyRenderComponent : RenderTextureComponent {

		public DummyRenderComponent(string file, Box2D rect) : base(file, rect) {
		}

		public override void Render() {
			base.Render();
			Console.WriteLine("DummyRenderComponent.Render() called!");
		}
	}

}
