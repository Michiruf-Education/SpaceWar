using SpaceWar.Framework;
using SpaceWar.Framework.Render;

namespace SpaceWar.Game {

	public class DummyScene : Scene {

		public DummyScene() {
			var grid = new GameObject();
			grid.AddComponent(new DrawGridComponent());
			Spawn(grid);

			Spawn(new CameraGameObject());
			Spawn(new DummyGameObject());
			Spawn(new StaticGameObject());
			Spawn(new DummFieldGameObject());
		}
	}

}
