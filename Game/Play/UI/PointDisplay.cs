using System.Drawing;
using Framework;
using Framework.Render;
using SpaceWar.Game.Play.Player;
using Zenseless.Geometry;
using PlayerT = SpaceWar.Game.Play.Player.Player;

namespace SpaceWar.Game.Play.UI {

	public class PointDisplay : GameObject {

		private readonly RenderTextComponent text;

		public PointDisplay() : base(true) {
			AddComponent(text = new RenderTextComponent(
				"",
				Options.DEFAULT_FONT,
				new SolidBrush(Color.White),
				new Box2D(0, -0.1f, 0.2f, 0.1f)
			));
		}

		public override void OnStart() {
			base.OnStart();
			Transform.Translate(-0.95f, 0.45f, Space.World);
		}

		public override void Update() {
			base.Update();
			text.Text = PlayerHelper.GetPlayerPoints().ToString();
		}
	}

}
