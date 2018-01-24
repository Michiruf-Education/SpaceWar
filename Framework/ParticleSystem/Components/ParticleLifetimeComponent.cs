using Framework.Object;

namespace Framework.ParticleSystem.Components {

	public class ParticleLifetimeComponent : ParticleComponent, UpdateComponent {

		public void Update() {
			Particle.Lifetime -= Time.DeltaTime;

			Particle.LifetimeCallback?.Invoke(Particle, Particle.Lifetime);

			// If the object is not alive anymore, destroy it
			if (!Particle.IsAlive) {
				// NOTE For now we need to tell the parent that this game object shell get removed
				GameObject.Parent.RemoveChild(GameObject);
				//Scene.Current.Destroy(GameObject);
			}
		}
	}

}
