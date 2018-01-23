using Framework.Object;
using Framework.Utilities;

namespace Framework.ParticleSystem {

	public class ParticleSystemComponent : Component, UpdateComponent {

		public ParticleEmitter Emitter { get; set; }
		public readonly float containerDestroyDelay;

		private GameObject particleSystemGameObject;
		private readonly MyTimer destroyTimer = new MyTimer();

		public ParticleSystemComponent(ParticleEmitter emitter, float containerDestroyDelay = 0.0f) {
			this.containerDestroyDelay = containerDestroyDelay;
			Emitter = emitter;
		}

		public override void OnDestroy() {
			base.OnDestroy();
			// Remove the particle system container
			if (particleSystemGameObject != null) {
				if (containerDestroyDelay > 0.0f) {
					destroyTimer.DoOnce(containerDestroyDelay, () => Scene.Current.Destroy(particleSystemGameObject));
				} else {
					Scene.Current.Destroy(particleSystemGameObject);
				}
			}
		}

		public void Update() {
			// Create the container if it does not exist
			if (particleSystemGameObject == null) {
				Scene.Current.Spawn(particleSystemGameObject = new ParticleSystemContainer());
			}

			MaySpawn();
		}

		private void MaySpawn() {
			// TODO "MAY"Spawn

			for (var i = 0; i < 20; i++) {
				var p = Particle.FromEmitter(Emitter);
				p.Transform.WorldPosition = GameObject.Transform.WorldPosition;
				particleSystemGameObject.AddChild(p);
				Emitter.OnStart?.Invoke(p);
			}
		}

		private class ParticleSystemContainer : GameObject {
		}
	}

}
