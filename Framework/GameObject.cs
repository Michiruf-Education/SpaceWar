using System.Collections.Generic;
using SpaceWar.Framework.Components;

namespace SpaceWar.Framework {

	public class GameObject {

		public List<Component> Components { get; } = new List<Component>();

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
