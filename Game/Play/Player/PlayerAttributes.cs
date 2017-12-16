using Framework;

namespace SpaceWar.Game.Play.Player {

	public class PlayerAttributes : Component {

		public int Lifes { get; private set; } = Player.MAX_LIFES;
		public bool IsAlive => Lifes > 0;

		public int Points { get; private set; }

		public void Damage() {
			Lifes--;
		}

		public void OnEnemyKill() {
			Points++;
		}
	}

}
