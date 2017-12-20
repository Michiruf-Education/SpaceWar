using Framework;
using Framework.Camera;
using OpenTK;

namespace SpaceWar.Game.Play.Player {

	public class FollowingCameraBehaviour : GameObject {

		public const float CAMERA_SPEED = 0.5f;
		public const float CAMERA_MIN_SPEED = 0.05f;

		public override void Update() {
			base.Update();
			
			// Do not move the camera if there is no player left
			if (!PlayerHelper.IsAPlayerAlive()) {
				return;
			}
			
			CameraComponent.Active.Position = CameraLerp(
				CameraComponent.Active.Position,
				PlayerHelper.GetPlayerPositionCentroid(),
				CAMERA_SPEED,
				CAMERA_MIN_SPEED);

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
