using System;

namespace Framework.Debug {

	public class ToDevelopException : SystemException {

		public ToDevelopException() {
		}

		public ToDevelopException(string message)
			: base(message) {
		}
	}

}
