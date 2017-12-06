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
			AddComponent(text = new RenderTextComponent(
				"",
				new Font(new FontFamily("Arial"), 10),
				new SolidBrush(Color.White),
				new Box2D(0, -0.1f, 0.2f, 0.1f)
			));
		}

		public override void OnStart() {
			base.OnStart();
			player = Scene.Current.GetGameObject<PlayerT>();
			Transform.Translate(-0.95f, 0.45f);
		}

		public override void Update() {
			base.Update();
			text.Text = player.Attributes.Points.ToString();
		}
	}

}
