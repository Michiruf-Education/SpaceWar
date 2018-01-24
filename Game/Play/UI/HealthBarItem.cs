using Framework;
using Framework.Render;
using SpaceWar.Resources;

namespace SpaceWar.Game.Play.UI {

	public class HealthBarItem : GameObject {

		public HealthBarItem() {
			AddComponent(new RenderTextureComponent(Resource.Heart, HealthBar.ITEM_SIZE, HealthBar.ITEM_SIZE));
		}
	}

}
