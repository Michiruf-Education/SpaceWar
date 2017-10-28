using System;
using System.Collections.Generic;
using System.Linq;
using SpaceWar.Framework.Object;
using Zenseless.Geometry;

namespace SpaceWar.Framework {

	public class GameObject {

		public bool IsEnabled { get; set; } = true;

		public Transform Transform { get; set; }

		// TODO Should be readyonly
		public List<Component> Components { get; private set; } = new List<Component>();

		public GameObject() : this(new Transform()) {
		}

		public GameObject(Transform transform) {
			Transform = transform;
		}

		public void AddComponent(Component component) {
			component.GameObject = this;
			Components.Add(component);
		}

		public TComponentType GetComponent<TComponentType>() {
			var component = Components.FirstOrDefault(c => c is TComponentType);
			return (TComponentType) Convert.ChangeType(component, typeof(TComponentType));
		}

		public List<TComponentTypes> GetComponents<TComponentTypes>() {
			var castedComponents = new List<TComponentTypes>();
			Components.Select(c => c is TComponentTypes)
				.ToList()
				.ForEach(c => castedComponents.Add(
					(TComponentTypes) Convert.ChangeType(c, typeof(TComponentTypes))));
			return castedComponents;
		}

		public virtual void Update() {
			// Do not update anything if the gameobject is not enabled
			if (!IsEnabled) {
				return;
			}

			Components.ForEach(c => {
				// Skip disabled components
				if (!c.IsEnabled) {
					return;
				}

				if (c is UpdateComponent updateComponent) {
					updateComponent.Update();
				}
			});
		}

		public virtual void Render() {
			// Do not draw anything if the gameobject is not enabled
			if (!IsEnabled) {
				return;
			}

			Components.ForEach(c => {
				// Skip disabled components
				if (!c.IsEnabled) {
					return;
				}

				if (c is RenderComponent renderComponent) {
					renderComponent.Render();
				}
			});
		}
	}

}
