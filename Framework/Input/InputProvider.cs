using System;
using System.Collections.Generic;

namespace SpaceWar.Framework.Input {

	public abstract class InputProvider {

		protected abstract void GetDefaultInputs(Dictionary<Enum, object> inputs);

		public Dictionary<Enum, object> LoadInputs() {
			var inputs = new Dictionary<Enum, object>();
			GetDefaultInputs(inputs);
			LoadSavedInputs(inputs);
			return inputs;
		}

		void LoadSavedInputs(Dictionary<Enum, object> inputs) {
			// TODO
		}

		public void SaveInputs() {
			// TODO
		}
	}

}
