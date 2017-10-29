using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using SpaceWar.Framework.Object;

namespace SpaceWar.Framework {

	public class GameObject {

		public Lifecycle Lifecycle { get; } = new Lifecycle();

		public bool IsEnabled { get; set; } = true;
		public Transform Transform { get; }
		public GameObject Parent { get; internal set; }
		private readonly List<GameObject> children = new List<GameObject>();
		public ReadOnlyCollection<GameObject> Children { get; }
		private readonly List<Component> components = new List<Component>();
		public ReadOnlyCollection<Component> Components { get; }


		public GameObject() : this(new Transform()) {
		}

		public GameObject(Transform transform) {
			Transform = transform;
			Transform.GameObject = this;
			Components = new ReadOnlyCollection<Component>(components);

			// Register lifecycle delegation for components
			Lifecycle.onDestroy += () => components.ForEach(c => c.Lifecycle?.onDestroy?.Invoke());
		}

		public void AddChild(GameObject child) {
			child.Parent = this;
			child.Lifecycle?.onCreate?.Invoke();
			children.Add(child);
		}

		public void RemoveChild(GameObject child) {
			child.Lifecycle?.onDestroy?.Invoke();
			children.Remove(child);
		}

		public TComponentType GetComponent<TComponentType>() {
			var component = components.FirstOrDefault(c => c is TComponentType);
			return (TComponentType) Convert.ChangeType(component, typeof(TComponentType));
		}

		public List<TComponentTypes> GetComponents<TComponentTypes>() {
			var castedComponents = new List<TComponentTypes>();
			components.Select(c => c is TComponentTypes)
				.ToList()
				.ForEach(c => castedComponents.Add(
					(TComponentTypes) Convert.ChangeType(c, typeof(TComponentTypes))));
			return castedComponents;
		}

		public void AddComponent(Component component) {
			component.GameObject = this;
			component.Lifecycle?.onCreate?.Invoke();
			components.Add(component);
		}

		public void RemoveComponent(Component component) {
			component.Lifecycle?.onDestroy?.Invoke();
			components.Remove(component);
		}

		public virtual void Update() {
			children.ForEach(go => {
				// Skip disabled gameobjects
				if (!go.IsEnabled) {
					return;
				}
				go.Update();
			});
			components.ForEach(c => {
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
			children.ForEach(go => {
				// Skip disabled gameobjects
				if (!go.IsEnabled) {
					return;
				}
				go.Render();
			});
			components.ForEach(c => {
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
