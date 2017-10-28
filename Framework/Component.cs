using System.Collections.Generic;
using SpaceWar.Framework.Object;

namespace SpaceWar.Framework {

	public abstract class Component {

		public Lifecycle Lifecycle { get; } = new Lifecycle();

		public GameObject GameObject { get; internal set; }
		public bool IsEnabled { get; set; } = true;

		public TComponentType GetComponent<TComponentType>() {
			return GameObject.GetComponent<TComponentType>();
		}

		public List<TComponentType> GetComponents<TComponentType>() {
			return GameObject.GetComponents<TComponentType>();
		}
	}

}
