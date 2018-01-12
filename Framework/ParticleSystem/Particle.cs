using System;
using Framework.Object;
using Framework.ParticleSystem.Components;
using OpenTK;

namespace Framework.ParticleSystem {

	public class Particle : GameObject {

		public static Particle FromEmitter(ParticleEmitter emitter) {
			return new Particle(emitter.InitializeVisualComponent) {
				Lifetime = emitter.Lifetime,
				LifetimeCallback = emitter.LifetimeCallback,
				Acceleration = emitter.Acceleration,
				Velocity = emitter.Velocity
			};
		}

		// Duration
		public float Lifetime { get; internal set; }
		public Action<Particle, float> LifetimeCallback { get; internal set; }
		public bool IsAlive => Lifetime > 0;

		// Position
		public Vector2 Acceleration { get; internal set; }
		public Vector2 Velocity { get; internal set; }

		// Visual
		public Component VisualComponent { get; }

		private Particle(Func<RenderComponent> emitterInitializeVisualComponent) {
			if (!(emitterInitializeVisualComponent?.Invoke() is Component visualComponentObject)) {
				throw new ArgumentException("Visual component must be a render component!");
			}
			VisualComponent = visualComponentObject;
		}

		public override void OnStart() {
			base.OnStart();

			AddComponent(new ParticleLifetimeComponent());
			AddComponent(new ParticlePositionComponent());
			AddComponent(VisualComponent);
		}
	}

}
