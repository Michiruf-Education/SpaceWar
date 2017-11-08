using OpenTK;
using SpaceWar.Framework;
using SpaceWar.Framework.Camera;

namespace SpaceWar.Game {

	public class CameraGameObject : GameObject {

		public CameraGameObject() {
			var c = new CameraComponent(new Vector2(1f, 1f));
			AddComponent(c);

			// Must be after adding (for now -> TODO?)
//			c.ViewportScaling = new Vector2(0.1f, 0.1f);
//			c.Position = new Vector2(0.05f, 0.05f);

//			c.ViewportScaling = new Vector2(10f, 10f);
//			c.Position = new Vector2(5f, 1f);

			// Note that the camera must be (1, 1) because we are not calculating
			c.ViewportScaling = new Vector2(1f, 1f);
			c.Position = new Vector2(0f, 0f);
		}
	}

}
