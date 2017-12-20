using Framework;
using Framework.Render;
using SpaceWar.Resources;

namespace SpaceWar.Game.Play.Field {

	public class Background : GameObject {

		public const float SIZE = 3f;

		public Background() {
			AddComponent(new RenderTextureComponent(Resource.background, 1.6f * SIZE, 0.9f * SIZE));
		}
	}

}
