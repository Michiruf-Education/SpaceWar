using Framework;
using OpenTK;

namespace SpaceWar.Game.Play.Player {

	public static class PlayerHelper {

		public static Vector2 GetPlayerPositionCentroid() {
			var result = new Vector2();

			var players = Scene.Current.GetGameObjects<Player>();
			players.ForEach(player => result += player.Transform.WorldPosition);

			return result / players.Count;
		}

		public static Player GetNearestPlayer(Vector2 position) {
			Player result = null;

			var players = Scene.Current.GetGameObjects<Player>();
			players.ForEach(player => {
				if (result == null ||
				    (player.Transform.WorldPosition - position).Length <
				    (result.Transform.WorldPosition - position).Length) {
					result = player;
				}
			});

			return result;
		}
	}

}
