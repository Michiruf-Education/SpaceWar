using System;
using Framework.Object;
using Framework.ParticleSystem.Components;
using OpenTK;

namespace Framework.ParticleSystem {

	public class Particle : GameObject {

		// Duration
		public float Lifetime { get; internal set; }
		public bool IsAlive => Lifetime > 0;

		// Position
		public Vector2 Acceleration { get; internal set; }
		public Vector2 Velocity { get; internal set; }

		// Visual
		public Component VisualComponent { get; }
		
		public Particle(Func<RenderComponent> emitterInitializeVisualComponent) {
			if (!(emitterInitializeVisualComponent?.Invoke() is Component visualComponentObject)) {
				throw new ArgumentException("Visual component must be a render component!");
			}
			VisualComponent = visualComponentObject;
		}

		public override void OnStart() {
			base.OnStart();

//			AddComponent(new ParticlePositionComponent());
//			AddComponent(new ParticleLifetimeComponent());
			AddComponent(VisualComponent);
		}
	}

}
