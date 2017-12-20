using SpaceWar.Game.Menu;
using SpaceWar.Game.Play.UI.General;

namespace SpaceWar.Game.Play.UI {

	public class GameOverOverlay : TextOverlay {

		public const float OVERLAY_DURATION = 3f;

		public GameOverOverlay() :
			base("Game Over!", OVERLAY_DURATION, overlay => Framework.Game.Instance.ShowScene(new MenuScene())) {
		}

		// Alternatively a Thead.Sleep(GAME_OVER_DISPLAY_TIME * 1000f) would be possible to deny every other
		// action than look to the screen
	}

}
