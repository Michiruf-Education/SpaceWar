using Framework;
using Framework.Render;

namespace SpaceWar.Game.Play.UI {

	public class HealthBarItem : GameObject {

		public HealthBarItem() {
			AddComponent(new RenderTextureComponent(Resources.heart, 0.05f, 0.05f));
		}
		
	}

}
