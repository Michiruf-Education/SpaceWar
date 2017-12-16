using OpenTK;
using Matrix3x2 = System.Numerics.Matrix3x2;

namespace Framework.Utilities {

	public static class FastVector2Transform {

		public static Vector2 Transform(Vector2 vector, Matrix3x2 matrix) {
			return Transform(vector.X, vector.Y, matrix);
		}

		public static Vector2 Transform(System.Numerics.Vector2 vector, Matrix3x2 matrix) {
			return Transform(vector.X, vector.Y, matrix);
		}

		/// Copied from existing function
		/// @see System.Numerics.Vector2.Transform(Vector2 position, Matrix3x2 matrix)
		public static Vector2 Transform(float x, float y, Matrix3x2 matrix) {
			return new Vector2(x * matrix.M11 + y * matrix.M21 + matrix.M31, x * matrix.M12 + y * matrix.M22 + matrix.M32);
		}
	}

}
