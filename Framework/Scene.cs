using System.Collections.Generic;
using System.Collections.ObjectModel;
using SpaceWar.Framework.Object;

namespace SpaceWar.Framework {

	public class Scene {

		public static Scene Current => Game.Instance.ActiveScene;

		public Lifecycle Lifecycle { get; } = new Lifecycle();

		private readonly List<GameObject> gameObjects = new List<GameObject>();
		public ReadOnlyCollection<GameObject> GameObjects { get; }

		protected Scene() {
			GameObjects = new ReadOnlyCollection<GameObject>(gameObjects);

			// Register lifecycle delegation for gameobjects
			Lifecycle.onDestroy += () => gameObjects.ForEach(go => go.Lifecycle?.onDestroy?.Invoke());
		}

		public void Spawn(GameObject gameObject) {
			gameObject.Lifecycle?.onCreate?.Invoke();
			gameObjects.Add(gameObject);
		}

		public void Destroy(GameObject gameObject) {
			gameObject.Lifecycle?.onDestroy?.Invoke();
			gameObjects.Remove(gameObject);
		}

		public virtual void Update() {
			gameObjects.ForEach(go => {
				// Skip disabled gameobjects
				if (!go.IsEnabled) {
					return;
				}
				go.Update();
			});
		}

		public virtual void Render() {
			gameObjects.ForEach(go => {
				// Skip disabled gameobjects
				if (!go.IsEnabled) {
					return;
				}
				go.Render();
			});
		}
	}

}
