using System;
using System.Drawing;
using Framework.Object;
using Framework.ParticleSystem;
using Framework.Render;
using Framework.Utilities;
using OpenTK;

namespace SpaceWar.Game.Play.Shot {

	public class ShotParticleEmitter : ParticleEmitter {

		private static readonly Random POSITION_RANDOM = new Random();

		public float SpawnRate => 0.001f;
		public int TotalCount => 100;
		public Action<Particle> OnStart => particle => {
			particle.Transform.WorldPosition += new Vector2(
				POSITION_RANDOM.NextFloat(-Shot.SHOT_SIZE * 0.3f, Shot.SHOT_SIZE * 0.3f),
				POSITION_RANDOM.NextFloat(-Shot.SHOT_SIZE * 0.3f, Shot.SHOT_SIZE * 0.3f));
		};
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
			var v = (RenderBoxComponent) particle.VisualComponent;
			var alpha = (int) (duration / Lifetime * 30f);
			if (alpha > 0) {
				v.Fill(Color.FromArgb(alpha, Shot.SHOT_COLOR));
			}
		}
	}

}
