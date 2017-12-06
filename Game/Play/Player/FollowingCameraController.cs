using Framework;
using Framework.Camera;
using Framework.Object;
using Framework.Utilities;
using OpenTK;
using NVector2 = System.Numerics.Vector2;

namespace SpaceWar.Game.Play.Player {

	public class FollowingCameraController : Component, UpdateComponent {

		public void Update() {
			CameraComponent.Active.Position = CameraLerp(
				CameraComponent.Active.Position,
				GameObject.Transform.WorldPosition,
				Player.CAMERA_SPEED,
				Player.CAMERA_MIN_SPEED);

			// NOTE @Marc
			// Du kannst ja statt die Geschwindigkeit fest zu machen (also was wie DISTANZ² / 10 Pixel pro Sekunde) 
			// die Geschwindigkeit erhöhen oder verringern um nen Betrag. Also wenn im Frame zuvor die Distanz größer
			// war (also die Kamera nun näher ist) verringerst du und sonst erhöhst die Geschwindigkeit um nen
			// kleinen Betrag
		}

		private static Vector2 CameraLerp(Vector2 a, Vector2 b, float lerpPercentage, float minLerpRate) {
			var lerpRate = (b - a).Length * lerpPercentage;
			if (lerpRate < minLerpRate) {
				lerpRate = minLerpRate;
				// TODO Minimum lerp rate does not take effect because were "overscrolling"?
			}
			var result = Vector2.Lerp(a, b, lerpRate);
			return result;
		}
	}

}
