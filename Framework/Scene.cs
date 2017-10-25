using System.Collections.Generic;

namespace SpaceWar.Framework {

	public class Scene {

		public List<GameObject> GameObjects { get; } = new List<GameObject>();

		public virtual void Update() {
			GameObjects.ForEach(go => go.Update());
		}

		public virtual void Render() {
			GameObjects.ForEach(go => go.Render());
		}
	}

}
