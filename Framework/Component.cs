using System.Collections.Generic;

namespace SpaceWar.Framework {

	public abstract class Component {

		public GameObject GameObject { get; internal set; }

		public ComponentType GetComponent<ComponentType>() {
			return GameObject.GetComponent<ComponentType>();
		}

		public List<ComponentType> GetComponents<ComponentType>() {
			return GameObject.GetComponents<ComponentType>();
		}
	}

}
