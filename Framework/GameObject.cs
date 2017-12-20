using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Framework.Object;

namespace Framework {

	// NOTE May needed:
	// NOTE - GetChild<Type> ?
	// NOTE - GetChildAt(int index)

	public class GameObject {

		public Transform Transform { get; }

		// General properties
		private bool isEnabled = true;
		public bool IsEnabled {
			// NoFormat
			get => isEnabled && (Parent == null || Parent.IsEnabled);
			set => isEnabled = value;
		}
		private bool isUiElement;
		public bool IsUiElement {
			// NoFormat
			get => isUiElement || Parent != null && Parent.IsUiElement;
			set => isUiElement = value;
		}

		// Parent and children
		public GameObject Parent { get; private set; }
		private readonly List<GameObject> children = new List<GameObject>();
		public ReadOnlyCollection<GameObject> Children { get; }

		// Components
		private readonly List<Component> components = new List<Component>();
		public ReadOnlyCollection<Component> Components { get; }


		public GameObject(bool isUiElement = false) {
			Transform = new Transform {GameObject = this};
			Children = new ReadOnlyCollection<GameObject>(children);
			Components = new ReadOnlyCollection<Component>(components);
			IsUiElement = isUiElement;
		}

		public virtual void OnStart() {
		}

		public virtual void OnDestroy() {
			GetLockedComponentsClone().ForEach(c => c?.OnDestroy());
		}

		public void AddChild(GameObject child) {
			lock (children) {
				if (child.Parent != null) {
					throw new ArgumentException("The child already has a parent!");
				}
				child.Parent = this;
				children.Add(child);
				child.OnStart();
			}
		}

		public void RemoveChild(GameObject child) {
			lock (children) {
				child.OnDestroy();
				children.Remove(child);
			}
		}

		public TComponentType GetComponent<TComponentType>() {
			try {
				var component = GetLockedComponentsClone().First(c => c is TComponentType);
				return (TComponentType) (object) component;
			} catch (InvalidOperationException) {
				return default(TComponentType);
			}
		}

		public List<TComponentType> GetComponents<TComponentType>() {
			var castedComponents = new List<TComponentType>();
			GetLockedComponentsClone().ForEach(c => {
				if (c is TComponentType) {
					castedComponents.Add((TComponentType) (object) c);
				}
			});
			return castedComponents;
		}

		public void AddComponent(Component component) {
			lock (components) {
				component.GameObject = this;
				components.Add(component);
				component.OnStart();
			}
		}

		public void RemoveComponent(Component component) {
			lock (components) {
				component.OnDestroy();
				components.Remove(component);
			}
		}

		private List<GameObject> GetLockedChildrenClone() {
			List<GameObject> childrenClone;
			lock (children) {
				childrenClone = children.ToList();
			}
			return childrenClone;
		}

		private List<Component> GetLockedComponentsClone() {
			List<Component> componentsClone;
			lock (components) {
				componentsClone = components.ToList();
			}
			return componentsClone;
		}

		public virtual void Update() {
			// Invalidate the transforms caches to not draw the same stuff like the last frame
			// and so be able to have a cache
			Transform.Invalidate();

			GetLockedChildrenClone().ForEach(go => {
				// Skip disabled gameobjects
				if (!go.IsEnabled) {
					return;
				}
				go.Update();
			});
			GetLockedComponentsClone().ForEach(c => {
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
			// Invalidate the transforms caches to not draw the same stuff like the last frame
			// and so be able to have a cache
			Transform.Invalidate();

			GetLockedChildrenClone().ForEach(go => {
				// Skip disabled gameobjects
				if (!go.IsEnabled) {
					return;
				}
				go.Render();
			});
			GetLockedComponentsClone().ForEach(c => {
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
