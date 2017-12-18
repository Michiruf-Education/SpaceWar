using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Framework {

	public class Scene {

		public static Scene Current => Game.Instance.ActiveScene;

		private readonly List<GameObject> gameObjects = new List<GameObject>();
		public ReadOnlyCollection<GameObject> GameObjects { get; }

		protected Scene() {
			GameObjects = new ReadOnlyCollection<GameObject>(gameObjects);
		}

		public virtual void OnStart() {
		}

		public virtual void OnDestroy() {
			gameObjects.ToList().ForEach(Destroy);
		}

		public void Spawn(GameObject gameObject) {
			gameObjects.Add(gameObject);
			gameObject.OnStart();
		}

		public void Destroy(GameObject gameObject) {
			if (!gameObjects.Contains(gameObject)) {
				return;
			}
			gameObject.OnDestroy();
			gameObjects.Remove(gameObject);
		}

		public TGameObjectType GetGameObject<TGameObjectType>() {
			// NOTE This would be a "FindByName" method later, because we will
			// not need to extend GameObject's in order to add components, but have
			// a file that will descript those and we instantiate them by name and look
			// them up also by name!
			
			// NOTE Methods currently not search for children
			
			try {
				var gameObject = gameObjects.First(go => go is TGameObjectType);
				return (TGameObjectType) (object) gameObject;
			} catch (InvalidOperationException) {
				return default(TGameObjectType);
			}
		}

		public List<TGameObjectType> GetGameObjects<TGameObjectType>() {
			// NOTE See comments in GetGameObject!
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
			// NOTE The ToList()-call is required to modify the list (by spawning or destroying) while updating, 
			// because it clones the list.
			// Else: System.InvalidOperationException: Collection was modified; enumeration operation may not execute.
			// We might need to use Immutables?
			gameObjects.ToList().ForEach(go => {
				// Skip disabled gameobjects
				if (!go.IsEnabled) {
					return;
				}
				go.Update();
			});
		}

		public virtual void Render() {
			// NOTE The ToList()-call is required to modify the list (by spawning or destroying) while updating, 
			// because it clones the list.
			// Else: System.InvalidOperationException: Collection was modified; enumeration operation may not execute.
			// We might need to use Immutables?
			gameObjects.ToList().ForEach(go => {
				// Skip disabled gameobjects
				if (!go.IsEnabled) {
					return;
				}
				go.Render();
			});
		}
	}

}
