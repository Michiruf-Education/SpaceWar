using System.Drawing;
using System.Windows.Forms;
using Framework.Object;
using Framework.Render;

namespace Framework.ParticleSystem {

	public class ParticleSystemComponent : Component, UpdateComponent {

		public ParticleEmitter Emitter { get; set; }

		private GameObject particleSystemGameObject;

		public ParticleSystemComponent(ParticleEmitter emitter) {
			Emitter = emitter;
		}

		public override void OnDestroy() {
			base.OnDestroy();
			// Remove the particle system container
			if (particleSystemGameObject != null) {
				Scene.Current.Destroy(particleSystemGameObject);
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

			var p = Particle.FromEmitter(Emitter);
			p.Transform.WorldPosition = GameObject.Transform.WorldPosition;
			particleSystemGameObject.AddChild(p);
		}

		private class ParticleSystemContainer : GameObject {
		}
	}

}
