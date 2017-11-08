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
			try {
				var gameObject = gameObjects.First(go => go is TGameObjectType);
				return (TGameObjectType) (object) gameObject;
			} catch (InvalidOperationException) {
				return default(TGameObjectType);
			}
		}

		public List<TGameObjectType> GetGameObjects<TGameObjectType>() {
			// NOTE See comment in GetGameObject!
			var castedGameObjects = new List<TGameObjectType>();
			gameObjects.ForEach(c => {
				if (c is TGameObjectType) {
					castedGameObjects.Add((TGameObjectType) (object) c);
				}
			});
			return castedGameObjects;
		}

		public List<TComponentType> GetAllComponentsInScene<TComponentType>() {
			var components = new List<TComponentType>();
			gameObjects.ForEach(go => components.AddRange(go.GetComponents<TComponentType>()));
			return components;
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
