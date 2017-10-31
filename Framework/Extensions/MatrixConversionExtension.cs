namespace SpaceWar.Framework.Extensions {

	public static class MatrixConversionExtension {

		public static OpenTK.Matrix3x2 ToOpenTKMatrix3x2(this System.Numerics.Matrix3x2 matrix) {
			return new OpenTK.Matrix3x2(matrix.M11, matrix.M12, matrix.M21, matrix.M22, matrix.M31, matrix.M32);
		}
	}

}
