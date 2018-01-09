using System;
using Framework.Object;
using OpenTK;

namespace Framework.ParticleSystem {

	public interface ParticleEmitter {

		float SpawnRate { get; }
		int TotalCount { get; }

		float Lifetime { get; }

		Vector2 Acceleration { get; }
		Vector2 Velocity { get; }

		Func<RenderComponent> InitializeVisualComponent { get; }
	}

}
