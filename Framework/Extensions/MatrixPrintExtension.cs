namespace Framework.Extensions {

	public static class MatrixPrintExtension {

		public static string NumericsMatrixPrettyPrint(this System.Numerics.Matrix3x2 matrix) {
			var output = "";
			output += "(" +matrix.M11 + "\t" + matrix.M12 + ")\n";
			output += "(" +matrix.M21 + "\t" + matrix.M22 + ")\n";
			output += "(" +matrix.M31 + "\t" + matrix.M32 + ")\n";
			return output;
		}
	}

}
