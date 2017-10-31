using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
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
			if (!gameObjects.Contains(gameObject)) {
				throw new Exception("Scene does not contain this GameObject!");
			}
			gameObject.Lifecycle?.onDestroy?.Invoke();
			gameObjects.Remove(gameObject);
		}
		
		public TGameObjectType GetGameObject<TGameObjectType>() {
			// NOTE This would be a "FindByName" method later, because we will
			// not need to extend GameObject's in order to add components, but have
			// a file that will descript those and we instantiate them by name and look
			// them up also by name!
			var component = gameObjects.FirstOrDefault(c => c is TGameObjectType);
			return (TGameObjectType) Convert.ChangeType(component, typeof(TGameObjectType));
		}

		public List<TGameObjectType> GetGameObjects<TGameObjectType>() {
			// NOTE See comment in GetGameObject!
			var castedGameObjects = new List<TGameObjectType>();
			gameObjects.Select(c => c is TGameObjectType)
				.ToList()
				.ForEach(c => castedGameObjects.Add(
					(TGameObjectType) Convert.ChangeType(c, typeof(TGameObjectType))));
			return castedGameObjects;
		}
		
		// TODO Maybe GetComponentsInScene?

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
