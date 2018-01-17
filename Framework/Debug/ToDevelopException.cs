using System;

namespace Framework.Debug {

	[Serializable]
	public class ToDevelopException : SystemException {

		public ToDevelopException() {
		}

		public ToDevelopException(string message)
			: base(message) {
		}
	}

}
