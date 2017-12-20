using Framework;
using SpaceWar.Game.Play.UI.General;

namespace SpaceWar.Game.Play.UI {

	public class NewWaveOverlay : TextOverlay {

		public const float OVERLAY_DURATION = 1f;

		public NewWaveOverlay() :
			base("New Wave!", OVERLAY_DURATION, overlay => Scene.Current.Destroy(overlay)) {
		}

		// Alternatively a Thead.Sleep(GAME_OVER_DISPLAY_TIME * 1000f) would be possible to deny every other
		// action than look to the screen
	}

}
