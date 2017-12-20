using System;
using OpenTK;

namespace SpaceWar.Game.Play.Enemy.Calculator {

	public static class EdgeSpawnPositionCalculator {

		private static readonly Random POSITION_GENERATOR = new Random();
		private const float RANDOM_FACTOR = 0.9f;
		private const float RANDOM_DIVISOR = 2f;

		public static Vector2 RandomEdgePosition(float fieldWidth, float fieldHeight) {
			switch (POSITION_GENERATOR.Next(0, 4)) {
				// Top-left
				case 0:
					return new Vector2(
						-GetProp(RANDOM_FACTOR) * fieldWidth / 2,
						GetProp(RANDOM_FACTOR) * fieldHeight / 2);
				// Top-right
				case 1:
					return new Vector2(
						GetProp(RANDOM_FACTOR) * fieldWidth / 2,
						GetProp(RANDOM_FACTOR) * fieldHeight / 2);
				// Bottom-right
				case 2:
					return new Vector2(
						GetProp(RANDOM_FACTOR) * fieldWidth / 2,
						-GetProp(RANDOM_FACTOR) * fieldHeight / 2);
				// Bottom-left
				case 3:
					return new Vector2(
						-GetProp(RANDOM_FACTOR) * fieldWidth / 2,
						-GetProp(RANDOM_FACTOR) * fieldHeight / 2);
				default:
					throw new ArgumentException("Position generator failed!");
			}
		}

		private static float GetProp(float maxValue) {
			return GetProp(0f, maxValue);
		}

		private static float GetProp(float value, float maxValue) {
			if (value > maxValue) {
				return value;
			}
			return GetProp((float) POSITION_GENERATOR.NextDouble(), maxValue / RANDOM_DIVISOR);
		}
	}

}
