using Framework;
using Framework.Render;

namespace SpaceWar.Game.Play.UI {

	public class HealthBarItem : GameObject {

		public HealthBarItem() {
			AddComponent(new RenderTextureComponent(Resources.heart, HealthBar.ITEM_SIZE, HealthBar.ITEM_SIZE));
		}
	}

}
