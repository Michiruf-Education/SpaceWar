using Framework;
using Framework.Object;
using Framework.Render;

namespace SpaceWar.Game {

	public class DummyScene : Framework.Scene {

		public DummyScene() {
			var grid = new GameObject();
			grid.AddComponent(new DrawGridComponent());
			Spawn(grid);

			Spawn(new FrameworkGameObject());

			Spawn(new CameraGameObject());
			//Spawn(new DummyGameObject());
			//Spawn(new StaticGameObject());
			//Spawn(new DummFieldGameObject());

			Spawn(new DummyCollisionPlayer());
			Spawn(new DummyColliderGameObject());
		}
	}

}
