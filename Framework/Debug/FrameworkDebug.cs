using System;

namespace Framework.Debug {

	public static class FrameworkDebug {

		public static bool Enabled { get; set; }

		private static bool DRAW_COLLIDERS = true;
		public static bool DrawColliders {
			// NoFormat
			get => Enabled && DRAW_COLLIDERS;
			set => DRAW_COLLIDERS = value;
		}

		private static bool PRINT_COLLISION_DETECTION = true;
		public static bool PrintCollisionDetection {
			// NoFormat
			get => Enabled && PRINT_COLLISION_DETECTION;
			set => PRINT_COLLISION_DETECTION = value;
		}

		public static void LogCollision(string text) {
			if (PrintCollisionDetection) {
				Console.WriteLine(text);
			}
		}
	}

}
