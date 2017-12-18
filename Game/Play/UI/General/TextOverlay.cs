using System;
using System.Drawing;
using Framework;
using Framework.Render;
using Framework.Utilities;
using Zenseless.Geometry;

namespace SpaceWar.Game.Play.UI.General {

	public class TextOverlay : GameObject {

		private readonly float duration;
		private readonly Action<TextOverlay> closeCallback;

		private readonly MyTimer timer = new MyTimer();

		public TextOverlay(string text, float duration, Action<TextOverlay> closeCallback = null) : base(true) {
			this.duration = duration;
			this.closeCallback = closeCallback;
			AddComponent(new RenderTextComponent(text, Options.DEFAULT_FONT, Brushes.White,
				new Box2D(-0.5f, -0.1f, 1f, 0.2f)));
		}

		public override void Update() {
			base.Update();
			timer.DoOnce(duration, () => {
				Scene.Current.Destroy(this);
				closeCallback?.Invoke(this);
			});
		}
	}

}
