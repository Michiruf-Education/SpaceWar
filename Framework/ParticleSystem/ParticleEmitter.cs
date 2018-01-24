using System;
using System.Drawing;
using Framework.Object;
using OpenTK;

namespace Framework.ParticleSystem {

	public interface ParticleEmitter {

		// Duration
		float SpawnRate { get; }
		int TotalCount { get; }
		Action<Particle> OnStart { get; }
		float Lifetime { get; }

		void LifetimeCallback(Particle particle, float duration);

		// Position
		Vector2 Acceleration { get; }
		Vector2 Velocity { get; }

		// Visual
		Func<RenderComponent> InitializeVisualComponent { get; }
	}

}
