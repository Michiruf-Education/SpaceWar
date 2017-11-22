using System.Collections.Generic;
using Framework;
using PlayerT = SpaceWar.Game.Play.Player.Player;

namespace SpaceWar.Game.Play.UI {

	public class HealthBar : GameObject {

		public const float SPACE_BETWEEN_ITEMS = 0.05f;
		public const float ITEM_SIZE = 0.05f;

		private readonly List<HealthBarItem> items;
		private PlayerT player;

		public HealthBar() {
			items = new List<HealthBarItem>();
			Init();

//			// TODO: This does not work yet. Implement a list of objects that were just attached and the call the onCreate
//			Lifecycle.onCreate += () => {
//				player = Scene.Current.GetGameObject<PlayerT>();
//			};
		}

		private void Init() {
			Transform.Translate(-0.95f, -0.45f);

			for (var i = 0; i < PlayerT.MAX_LIFES; i++) {
				var item = new HealthBarItem();
				item.Transform.Translate(SPACE_BETWEEN_ITEMS * i, 0);
				items.Add(item);
				AddChild(item);
			}
		}

		public override void Update() {
			base.Update();

			// TODO Do this in constructor when possible
			// (currently not possible because we need better lifecycle integration)
			// -> Scene.Current is still null in constructor
			if (player == null)
				player = Scene.Current.GetGameObject<PlayerT>();

			for (var i = 0; i < PlayerT.MAX_LIFES; i++) {
				items[i].IsEnabled = player.Attributes.Lifes >= i;
			}
		}
	}

}
