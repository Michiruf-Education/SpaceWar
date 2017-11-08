using System.Drawing;
using SpaceWar.Framework;
using SpaceWar.Framework.Collision;
using SpaceWar.Framework.Object;
using SpaceWar.Framework.Render;

namespace SpaceWar.Game {

	public class DummyFieldGameObject : GameObject {

		public DummyFieldGameObject() {
			AddComponent(new RenderBoxComponent(3f, 3f).Fill(Color.Red).Stroke(Color.GreenYellow, 1f));
			AddComponent(new BoxCollider());
		}

		public override void Update() {
			base.Update();
			Transform.Rotate(0.3f);
		}
	}

}
