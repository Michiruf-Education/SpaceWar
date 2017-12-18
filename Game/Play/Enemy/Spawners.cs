using System.Collections.Generic;
using SpaceWar.Game.Play.Enemy.Enemy1;
using SpaceWar.Game.Play.Enemy.Enemy2;
using SpaceWar.Game.Play.Enemy.General;

namespace SpaceWar.Game.Play.Enemy {

	public class Spawners {

		private static Spawners INSTANCE;
		public static List<AbstractSpawner> All => INSTANCE.spawners;
		
		public static void Init() {
			INSTANCE = new Spawners();
		}

		private readonly List<AbstractSpawner> spawners = new List<AbstractSpawner>();

		private Spawners() {
			spawners.Add(new Enemy1Spawner1());
			spawners.Add(new Enemy1Spawner2());
			spawners.Add(new Enemy2Spawner1());
			spawners.Add(new Enemy2Spawner2());
		}
	}

}
