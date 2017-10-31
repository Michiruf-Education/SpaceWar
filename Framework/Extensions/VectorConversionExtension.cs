namespace SpaceWar.Framework.Extensions {

	public static class VectorConversionExtension {

		public static System.Numerics.Vector2 ToNumericsVector2(this OpenTK.Vector2 vector2) {
			return new System.Numerics.Vector2(vector2.X, vector2.Y);
		}

		public static OpenTK.Vector2 ToOpenTKVector2(this System.Numerics.Vector2  vector2) {
			return new OpenTK.Vector2(vector2.X, vector2.Y);
		}
	}

}
