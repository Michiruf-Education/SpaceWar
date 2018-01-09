using Framework.Object;

namespace Framework.ParticleSystem.Components {

	public class ParticleLifetimeComponent : ParticleComponent, UpdateComponent {

		public void Update() {
			Particle.Lifetime -= Time.DeltaTime;

			// If the object is not alive anymore, destroy it
			if (!Particle.IsAlive) {
				Scene.Current.Destroy(GameObject);
			}
		}
	}

}
