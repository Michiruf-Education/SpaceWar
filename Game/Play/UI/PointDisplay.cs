using System.Drawing;
using Framework;
using Framework.Render;
using Zenseless.Geometry;
using PlayerT = SpaceWar.Game.Play.Player.Player;

namespace SpaceWar.Game.Play.UI {

	public class PointDisplay : GameObject {

		private readonly RenderTextComponent text;
		private PlayerT player;

		public PointDisplay() {
			text = new RenderTextComponent(
				"Hallo",
				new Font(new FontFamily("Arial"), 10),
				new SolidBrush(Color.White),
				new Box2D(0, -0.1f, 0.2f, 0.1f)
			);
			AddComponent(text);
			Transform.Translate(-0.95f, 0.45f);
		}

		public override void Update() {
			base.Update();

			// TODO
			if (player == null)
				player = Scene.Current.GetGameObject<PlayerT>();

			text.Text = player.Attributes.Points.ToString();
		}
	}

}
