using System;
using System.Drawing;
using System.Threading;
using Framework;
using Framework.Render;
using Framework.Utilities;
using Zenseless.Geometry;

namespace SpaceWar.Game.Play.UI.General {

	public class TextOverlay : GameObject {

		private readonly float duration;
		private readonly Action<TextOverlay> closeCallback;
		private readonly bool freeze;

		private readonly MyTimer timer = new MyTimer();

		public TextOverlay(string text, float duration, Action<TextOverlay> closeCallback = null, bool freeze = false) :
			base(true) {
			this.duration = duration;
			this.closeCallback = closeCallback;
			this.freeze = freeze;
			AddComponent(new RenderTextComponent(text, Options.DEFAULT_FONT, Brushes.White,
				new Box2D(-0.5f, -0.1f, 1f, 0.2f)));
		}

		public override void Update() {
			base.Update();
			if (freeze) {
				Thread.Sleep((int) (duration * 1000f));
			} else {
				timer.DoOnce(duration, () => {
					Scene.Current.Destroy(this);
					closeCallback?.Invoke(this);
				});
			}
		}
	}

}
