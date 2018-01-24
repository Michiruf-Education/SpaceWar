namespace Framework.Utilities {

	public static class VectorConverterExtension {

		public static System.Numerics.Vector2 ToNumericsVector2(this OpenTK.Vector2 vector2) {
			return new System.Numerics.Vector2(vector2.X, vector2.Y);
		}

		public static OpenTK.Vector2 ToOpenTKVector2(this System.Numerics.Vector2 vector2) {
			return new OpenTK.Vector2(vector2.X, vector2.Y);
		}

		public static System.Numerics.Matrix3x2 ToNumericsMatrix(this OpenTK.Matrix3x2 matrix) {
			return new System.Numerics.Matrix3x2(matrix.M11, matrix.M12, matrix.M21, matrix.M22, matrix.M31, matrix.M32);
		}

		public static OpenTK.Matrix3x2 ToOpenTKMatrix(this System.Numerics.Matrix3x2 matrix) {
			return new OpenTK.Matrix3x2(matrix.M11, matrix.M12, matrix.M21, matrix.M22, matrix.M31, matrix.M32);
		}
	}

}
