using System;

namespace Framework.Utilities {

	public static class RandomExtension {

		public static float NextFloat(this Random random) {
			return (float) random.NextDouble();
		}

		public static float NextFloat(this Random random, float minValue, float maxValue) {
			return (float) random.NextDouble(minValue, maxValue);
		}

		public static double NextDouble(this Random random, double minValue, double maxValue) {
			return random.NextDouble() * (maxValue - minValue) + minValue;
		}
	}

}
