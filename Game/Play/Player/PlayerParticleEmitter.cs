using System;
using System.Drawing;
using Framework;
using Framework.Object;
using Framework.ParticleSystem;
using Framework.Render;
using Framework.Utilities;
using OpenTK;

namespace SpaceWar.Game.Play.Player {

	public class PlayerParticleEmitter : ParticleEmitter {

		private static readonly Random POSITION_RANDOM = new Random();
		private static readonly Random VELOCITY_RANDOM = new Random();
		private const float WIDTH_FACTOR = 0.3f * Player.PLAYER_SIZE;
		private const float BEHIND_FACTOR = 0.35f * Player.PLAYER_SIZE;

		private readonly Player player;

		public PlayerParticleEmitter(Player player) {
			this.player = player;
		}

		public float SpawnRate => 0.001f;
		public int TotalCount => 100;
		public Action<Particle> OnStart => particle => {
			// Get the direction. Instead of adding + 90 degress, we assign x to a and y to b!
			var direction = MathHelper.DegreesToRadians(player.Transform.WorldRotation);
			var a = Math.Cos(direction);
			var b = Math.Sin(direction);
			var random = POSITION_RANDOM.NextFloat(-WIDTH_FACTOR, WIDTH_FACTOR);

			particle.Transform.WorldPosition += new Vector2(
				(float) (random * b - a * BEHIND_FACTOR),
				(float) (random * -a - b * BEHIND_FACTOR));
			// Quadric variant before:
			//particle.Transform.WorldPosition += new Vector2(
			//	POSITION_RANDOM.NextFloat(-Player.PLAYER_SIZE * 0.3f, Player.PLAYER_SIZE * 0.3f),
			//	POSITION_RANDOM.NextFloat(-Player.PLAYER_SIZE * 0.3f, Player.PLAYER_SIZE * 0.3f));
		};
		public float Lifetime => 0.4f;
		public Vector2 Acceleration => Vector2.Zero;
		public Func<Vector2, float> AccelerationOverTimeFunc => null;
		public Vector2 Velocity {
			get {
				var angle = VELOCITY_RANDOM.NextDouble(0, 360);
				return new Vector2((float) Math.Cos(angle), (float) Math.Sin(angle)) *
				       VELOCITY_RANDOM.NextFloat(0.0001f, 0.0005f);
			}
		}
		public Func<Vector2, float> VelocityOverTimeFuncOverride => null;
		public Func<RenderComponent> InitializeVisualComponent => () => new RenderBoxComponent(0.02f, 0.02f).Fill(Color.Red);
		public Func<Color, float> ColorOverTimeFunc => null;


		public void LifetimeCallback(Particle particle, float duration) {
			// Color the visual component by the remaining time
			var v = (RenderBoxComponent) particle.VisualComponent;
			var alpha = (int) (duration / Lifetime * 5f);
			if (alpha > 0) {
				v.Fill(Color.FromArgb(alpha, player.PlayerColor));
			}
		}
	}

}
