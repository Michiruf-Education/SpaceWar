using System;
using Framework;
using SpaceWar.Game.Play.Enemy.General;

namespace SpaceWar.Game.Play.Player {

	public class PlayerAttributes : Component {

		public int Lifes { get; private set; } = Player.MAX_LIFES;
		public bool IsAlive => GameDebug.UnDieable || Lifes > 0;

		public int Points { get; private set; } = GameDebug.InitialPoints;

		public void Damage() {
			Lifes--;
		}

		public void OnEnemyKill(AbstractEnemy enemy) {
			if (enemy == null) {
				throw new ArgumentException("No enemy given");
			}
			Points += enemy.PointsForKilling;
		}
	}

}
