using System.Collections.Generic;

namespace Framework.Utilities {

	public static class DictionaryExtension {

		public static V GetOrDefault<T, V>(this IDictionary<T, V> dictionary, T key) {
			dictionary.TryGetValue(key, out var value);
			return value;
		}
	}

}
