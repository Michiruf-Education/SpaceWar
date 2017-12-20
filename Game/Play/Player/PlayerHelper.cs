using System.Collections.Generic;
using System.Linq;
using Framework;
using OpenTK;

namespace SpaceWar.Game.Play.Player {

	public static class PlayerHelper {

		public static List<Player> GetPlayers(bool includeDead = false) {
			var players = Scene.Current.GetGameObjects<Player>();
			if (!includeDead) {
				players = players.Where((player, i) => player.Attributes.IsAlive).ToList();
			}
			return players;
		}

		public static bool IsAPlayerAlive() {
			return GetPlayers().Count > 0;
		}

		public static int GetPlayerCount() {
			return GetPlayers(true).Count;
		}

		public static int GetPlayerLifes() {
			return GetPlayers().Aggregate(0, (i, player) => i + player.Attributes.Lifes);
		}

		public static int GetPlayerPoints() {
			var points = 0;
			GetPlayers(true).ForEach(player => points += player.Attributes.Points);
			return points;
		}

		public static Vector2 GetPlayerPositionCentroid(bool includeDead = false) {
			var result = new Vector2();
			var players = GetPlayers(includeDead);
			players.ForEach(player => result += player.Transform.WorldPosition);
			return result / players.Count;
		}

		public static Player GetNearestPlayer(Vector2 position, bool includeDead = false) {
			Player result = null;
			GetPlayers(includeDead).ForEach(player => {
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
