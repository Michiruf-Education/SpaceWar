using System;
using Framework;
using Framework.Object;
using Framework.Utilities;
using OpenTK;
using OpenTK.Input;

namespace SpaceWar.Game.Play.Player {

	public class PlayerShotController : Component, UpdateComponent {

		private readonly MyTimer shotTimer = new MyTimer();

		public float ShotRate { get; private set; } = Player.INITIAL_SHOT_RATE;

		private Player player;

		public override void OnStart() {
			base.OnStart();
			player = GameObject as Player;
		}

		public void Update() {
			// Do nothing if dead
			if (!player.Attributes.IsAlive) {
				return;
			}

			DetectShooting();
		}

		void DetectShooting() {
			// Detect keyboard first only for the first player
			if (player.PlayerIndex == 0) {
				var keyboardAxis = Vector2.Zero;
				if (Keyboard.GetState().IsKeyDown(Key.Up)) {
					keyboardAxis.Y++;
				}
				if (Keyboard.GetState().IsKeyDown(Key.Down)) {
					keyboardAxis.Y--;
				}
				if (Keyboard.GetState().IsKeyDown(Key.Left)) {
					keyboardAxis.X--;
				}
				if (Keyboard.GetState().IsKeyDown(Key.Right)) {
					keyboardAxis.X++;
				}
				if (keyboardAxis != Vector2.Zero) {
					MaySpawnShot(keyboardAxis);
				}
			}

			// Detect gamepad and skip if no inputs are given by using the correct float comparison
			var gamepadAxis = GamePad.GetState(player.PlayerIndex).ThumbSticks.Right;
			if (gamepadAxis.Length >= Options.CONTROLLER_THRESHOLD) {
				MaySpawnShot(gamepadAxis);
			}
		}

		void MaySpawnShot(Vector2 axis) {
			shotTimer.DoEvery(ShotRate, () => SpawnShot(axis), MyTimer.When.Start);
		}

		void SpawnShot(Vector2 axis) {
			var direction = (float) Math.Atan2(axis.Y, axis.X);
			Scene.Current.Spawn(new Shot.Shot(
				direction + MathHelper.DegreesToRadians(3),
				GameObject.Transform.WorldPosition,
				player));
			Scene.Current.Spawn(new Shot.Shot(
				direction + MathHelper.DegreesToRadians(-3),
				GameObject.Transform.WorldPosition,
				player));
		}
	}

}
