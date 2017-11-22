using System;

namespace Framework.Object {

	public class Lifecycle {

		public Action onCreate; // TODO Maybe: onAttached?

		public Action onDestroy;
		
		// TODO Integrate this into Component with OnAttached and OnDetached as a virtual method
	}

}
