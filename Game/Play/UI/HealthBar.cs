using System.Collections.Generic;
using Framework;
using PlayerT = SpaceWar.Game.Play.Player.Player;

namespace SpaceWar.Game.Play.UI {

	public class HealthBar : GameObject {

		public const float SPACE_BETWEEN_ITEMS = 0.05f;
		public const float ITEM_SIZE = 0.05f;

		private readonly List<HealthBarItem> items = new List<HealthBarItem>();
		private PlayerT player;

		public HealthBar() : base(true) {
		}

		public override void OnStart() {
			base.OnStart();
			player = Scene.Current.GetGameObject<PlayerT>();
			Transform.Translate(-0.95f, -0.45f, Space.World);

			for (var i = 0; i < PlayerT.MAX_LIFES; i++) {
				var item = new HealthBarItem();
				item.Transform.Translate(SPACE_BETWEEN_ITEMS * i, 0, Space.Local);
				items.Add(item);
				AddChild(item);
			}
		}

		public override void Update() {
			base.Update();
			for (var i = 0; i < PlayerT.MAX_LIFES; i++) {
				items[i].IsEnabled = player.Attributes.Lifes >= i;
			}
		}
	}

}
