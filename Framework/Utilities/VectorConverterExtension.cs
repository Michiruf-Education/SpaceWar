using System.Numerics;

namespace Framework.Utilities {

	public static class VectorConverterExtension {

		public static Vector2 ToNumericsVector2(this OpenTK.Vector2 vector2) {
			return new Vector2(vector2.X, vector2.Y);
		}

		public static OpenTK.Vector2 ToOpenTKVector2(this Vector2  vector2) {
			return new OpenTK.Vector2(vector2.X, vector2.Y);
		}
	}

}
