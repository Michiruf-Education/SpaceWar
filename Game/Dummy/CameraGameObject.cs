using OpenTK;
using SpaceWar.Framework;
using SpaceWar.Framework.Camera;

namespace SpaceWar.Game {

	public class CameraGameObject : GameObject {

		public CameraGameObject() {
			AddComponent(new CameraComponent(new Vector2(10, 10)));
		}
	}

}
