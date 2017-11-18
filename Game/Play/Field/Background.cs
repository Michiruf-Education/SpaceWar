using Framework;
using Framework.Render;

namespace SpaceWar.Game.Play.Field {

	public class Background : GameObject {

		const float SIZE = 3f;

		public Background() {
			AddComponent(new RenderTextureComponent(Resources.background, 1.6f * SIZE, 0.9f * SIZE));
		}
	}

}
