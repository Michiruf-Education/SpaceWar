using System;
using System.Collections.Generic;
using System.Linq;
using Zenseless.Geometry;

namespace SpaceWar.Framework {

	public class GameObject {

		public Transformation2D Transform { get; set; }

		public bool IsVisible { get; set; } = true;

		// TODO Should be readyonly
		public List<Component> Components { get; private set; } = new List<Component>();

		public GameObject() : this(new Transformation2D()) {
		}

		public GameObject(Transformation2D transform) {
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
			Components.ForEach(c => {
				if (c is UpdateComponent updateComponent) {
					updateComponent.Update();
				}
			});
		}

		public virtual void Render() {
			Components.ForEach(c => {
				if (c is RenderComponent renderComponent) {
					renderComponent.Render();
				}
			});
		}
	}

}
