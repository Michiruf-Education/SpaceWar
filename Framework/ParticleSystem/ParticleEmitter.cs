using System;
using System.Drawing;
using Framework.Object;
using OpenTK;

namespace Framework.ParticleSystem {

	public interface ParticleEmitter {

		// Duration
		float SpawnRate { get; }
		int TotalCount { get; }
		float Lifetime { get; }
		void LifetimeCallback(Particle particle, float duration);

		// Position
		Vector2 Acceleration { get; }
		Func<Vector2, float> AccelerationOverTimeFunc { get; }
		Vector2 Velocity { get; }
		Func<Vector2, float> VelocityOverTimeFuncOverride { get; }

		// Visual
		Func<RenderComponent> InitializeVisualComponent { get; }
		Func<Color, float> ColorOverTimeFunc { get; }
	}

}
