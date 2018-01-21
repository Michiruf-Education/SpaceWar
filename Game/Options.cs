using System.Drawing;

namespace SpaceWar.Game {

	public static class Options {

		public static readonly Font DEFAULT_FONT = new Font(new FontFamily("Arial"), 10);
		
		public const float CONTROLLER_THRESHOLD = 0.3f;

		public const float SOUND_BACKGROUND_VOLUME = 0.5f;
		public const float SOUND_SHOT_VOLUME = 0.4f;
	}

}
