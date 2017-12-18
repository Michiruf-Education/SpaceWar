using System;

namespace Framework.Debug {

	public static class FrameworkDebug {

		public static bool Enabled { get; set; }

		private static bool drawColliders = true;
		public static bool DrawColliders {
			// NoFormat
			get => Enabled && drawColliders;
			set => drawColliders = value;
		}

		private static bool printCollisionDetection = true;
		public static bool PrintCollisionDetection {
			// NoFormat
			get => Enabled && printCollisionDetection;
			set => printCollisionDetection = value;
		}

		public static void LogCollision(string text) {
			if (PrintCollisionDetection) {
				Console.WriteLine(text);
			}
		}
	}

}
