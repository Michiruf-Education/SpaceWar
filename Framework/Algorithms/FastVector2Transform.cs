using System.Numerics;

namespace Framework.Algorithms {

	public static class FastVector2Transform {

		/// Copied from existing function
		/// @see System.Numerics.Vector2.Transform(Vector2 position, Matrix3x2 matrix)
		public static OpenTK.Vector2 Transform(float x, float y, Matrix3x2 matrix) {
			return new OpenTK.Vector2(x * matrix.M11 + y * matrix.M21 + matrix.M31, x * matrix.M12 + y * matrix.M22 + matrix.M32);
		}

		/// Copied from existing function
		/// @see System.Numerics.Vector2.Transform(Vector2 position, Matrix3x2 matrix)
		public static float[] TransformFloatArray(float x, float y, Matrix3x2 matrix) {
			return new[] {x * matrix.M11 + y * matrix.M21 + matrix.M31, x * matrix.M12 + y * matrix.M22 + matrix.M32};
		}
	}

}
