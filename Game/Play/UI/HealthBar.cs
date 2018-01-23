using System.Collections.Generic;
using Framework;
using SpaceWar.Game.Play.Player;
using PlayerT = SpaceWar.Game.Play.Player.Player;

namespace SpaceWar.Game.Play.UI {

	public class HealthBar : GameObject {

		public const float SPACE_BETWEEN_ITEMS = 0.05f;
		public const float ITEM_SIZE = 0.037f;

		private readonly List<HealthBarItem> items = new List<HealthBarItem>();

		public HealthBar() : base(true) {
		}

		public override void OnStart() {
			base.OnStart();
			Transform.Translate(-0.95f, -0.45f, Space.World);

			for (var i = 0; i < PlayerHelper.GetPlayerCount() * PlayerT.MAX_LIFES; i++) {
				var item = new HealthBarItem();
				item.Transform.Translate(SPACE_BETWEEN_ITEMS * i, 0, Space.Local);
				items.Add(item);
				AddChild(item);
			}
		}

		public override void Update() {
			base.Update();
			for (var i = 0; i < PlayerHelper.GetPlayerCount() * PlayerT.MAX_LIFES; i++) {
				items[i].IsEnabled = PlayerHelper.GetPlayerLifes() > i;
			}
		}
	}

}
