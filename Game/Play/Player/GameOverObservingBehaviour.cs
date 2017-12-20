using Framework;
using SpaceWar.Game.Play.UI;

namespace SpaceWar.Game.Play.Player {

	public class GameOverObservingBehaviour : GameObject {

		public override void Update() {
			base.Update();
			if (PlayerHelper.IsAPlayerAlive()) {
				return;
			}
			Scene.Current.Spawn(new GameOverOverlay());
			Scene.Current.Destroy(this);
		}
	}

}
