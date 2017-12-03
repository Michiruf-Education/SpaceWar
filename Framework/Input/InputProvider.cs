using System;
using System.Collections.Generic;

namespace Framework.Input {

	public abstract class InputProvider {

		protected abstract void GetDefaultInputs(Dictionary<Enum, object> inputs);

		public Dictionary<Enum, object> LoadInputs() {
			var inputs = new Dictionary<Enum, object>();
			GetDefaultInputs(inputs);
			LoadSavedInputs(inputs);
			return inputs;
		}

		void LoadSavedInputs(Dictionary<Enum, object> inputs) {
			// NOTE Implement this when the game is going to get final because we do not know
			// if we need this and if there are any changes in the input mechanism
		}

		public void SaveInputs() {
			// NOTE Implement this when the game is going to get final because we do not know
			// if we need this and if there are any changes in the input mechanism
		}
	}

}
