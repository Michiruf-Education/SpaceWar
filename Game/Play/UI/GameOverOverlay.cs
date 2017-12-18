using System.Drawing;
using System.Threading;
using Framework;
using Framework.Render;
using SpaceWar.Game.Menu;
using Zenseless.Geometry;

namespace SpaceWar.Game.Play.UI {

	public class GameOverOverlay : GameObject {

		public const float GAME_OVER_DISPLAY_TIME = 3f;

		public GameOverOverlay() : base(true) {
			AddComponent(new RenderTextComponent("Game Over!", Options.DEFAULT_FONT, Brushes.White,
				new Box2D(-0.5f, -0.1f, 1f, 0.2f)));
		}

		public override void Update() {
			// Just use thread sleep because then there will be no action in this time
			Thread.Sleep((int) (GAME_OVER_DISPLAY_TIME * 1000f));
			Framework.Game.Instance.ShowScene(new MenuScene());
		}
	}

}
