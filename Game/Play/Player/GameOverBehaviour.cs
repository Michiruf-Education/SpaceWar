using Framework;
using SpaceWar.Game.Play.UI;

namespace SpaceWar.Game.Play.Player {

	public class GameOverBehaviour : GameObject {

		public override void Update() {
			base.Update();
			var gameRunning = false;
			Scene.Current.GetGameObjects<Player>().ForEach(player => gameRunning |= player.Attributes.Lifes > 0);
			if (!gameRunning) {
				Scene.Current.Spawn(new GameOverOverlay());
				Scene.Current.Destroy(this);
			}
		}
	}

}
