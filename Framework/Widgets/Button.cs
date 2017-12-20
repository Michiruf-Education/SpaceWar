using System;
using System.Drawing;
using Framework.Render;
using Zenseless.Geometry;

namespace Framework.Widget {

	public class Button : GameObject {

		public Button(Box2D size, string label, Font font, Brush brush, Color background, Action action) {
			AddComponent(new BoxClickComponent(size, action));
			AddComponent(new RenderBoxComponent(size).Fill(background));
			AddComponent(new RenderTextComponent(label, font, brush, size));
		}
	}

}
