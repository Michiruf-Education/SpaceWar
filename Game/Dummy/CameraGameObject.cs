using OpenTK;
using SpaceWar.Framework;
using SpaceWar.Framework.Camera;

namespace SpaceWar.Game {

	public class CameraGameObject : GameObject {

		public CameraGameObject() {
			Transform.Rotate(90); // TODO Does not change anything yet!
			AddComponent(new CameraComponent(new Vector2(10, 10)));
		}
	}

}
