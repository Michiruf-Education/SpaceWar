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

		private readonly Player player;

		public PlayerParticleEmitter(Player player) {
			this.player = player;
		}

		public float SpawnRate { get; } = 0.001f;
		public int TotalCount { get; } = 100;
		public float Lifetime { get; } = 1f;
		public Vector2 Acceleration { get; } = Vector2.Zero;
		public Func<Vector2, float> AccelerationOverTimeFunc { get; } = null;
		public Vector2 Velocity {
			get {
				var velocityRandom = new Random();
				return new Vector2(velocityRandom.NextFloat(-0.001f, 0.001f), velocityRandom.NextFloat(-0.001f, 0.001f));
			}
		}
		public Func<Vector2, float> VelocityOverTimeFuncOverride { get; } = null;
		public Func<RenderComponent> InitializeVisualComponent { get; } = () => new RenderBoxComponent(0.005f, 0.005f).Fill(Color.Red);
		public Func<Color, float> ColorOverTimeFunc { get; } = null;

		
		public void LifetimeCallback(Particle particle, float duration) {
			// Color the visual component by the remaining time
			var v = (RenderBoxComponent) particle.VisualComponent;
			var alpha = (int) (duration / Lifetime * 255f);
			if (alpha > 0) {
				v.Fill(Color.FromArgb(alpha, player.PlayerColor));
			}
		}
	}

}
