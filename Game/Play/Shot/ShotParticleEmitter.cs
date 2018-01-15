using System;
using System.Drawing;
using Framework.Object;
using Framework.ParticleSystem;
using Framework.Render;
using Framework.Utilities;
using OpenTK;

namespace SpaceWar.Game.Play.Shot {

	public class ShotParticleEmitter : ParticleEmitter {

		public float SpawnRate => 0.001f;
		public int TotalCount => 100;
		public Action<Particle> OnStart => null;
		public float Lifetime => 0.1f;
		public Vector2 Acceleration => Vector2.Zero;
		public Func<Vector2, float> AccelerationOverTimeFunc => null;
		public Vector2 Velocity => Vector2.Zero;
		public Func<Vector2, float> VelocityOverTimeFuncOverride => null;
		public Func<RenderComponent> InitializeVisualComponent => () =>
			new RenderBoxComponent(0.01f, 0.01f).Fill(Shot.SHOT_COLOR);
		public Func<Color, float> ColorOverTimeFunc => null;

		public void LifetimeCallback(Particle particle, float duration) {
			// Color the visual component by the remaining time
			var visual = (RenderBoxComponent) particle.VisualComponent;
			var factor = duration / Lifetime;

			// Set the alpha
			var alpha = (int) (factor * 10f);
			if (alpha > 0) {
				visual.Fill(Color.FromArgb(alpha, Shot.SHOT_COLOR));
			}

			// Set the size // TODO TODO TODO
			var size = (1 - factor) * Shot.SHOT_SIZE;
			particle.Transform.LocalScaling = new Vector2(size);
		}
	}

}
