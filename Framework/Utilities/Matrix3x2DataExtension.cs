using System;
using System.Numerics;
using Vector2 = OpenTK.Vector2;

namespace Framework.Utilities {

	// ReSharper disable once InconsistentNaming
	public static class Matrix3x2DataExtension {

		public static Vector2 GetPosition(this Matrix3x2 data) {
			return new Vector2 {
				X = data.M31,
				Y = data.M32
			};
		}

		[Obsolete("It is impossible to receive this data from the matrix")]
		public static float GetRotation(this Matrix3x2 data) {
			return 0f;
		}

		[Obsolete("It is impossible to receive this data from the matrix")]
		public static Vector2 GetScaling(this Matrix3x2 data) {
			return Vector2.Zero;
		}
	}

}
