using Framework.Object;

namespace Framework.ParticleSystem.Components {

	public class ParticlePositionComponent : ParticleComponent, UpdateComponent {

		public void Update() {
			// Move the object by velocity first
			GameObject.Transform.WorldPosition += Particle.Velocity * Time.DeltaTime;

			// Update the velocity by acceleration
			Particle.Velocity += Particle.Acceleration * Time.DeltaTime;
		}
	}

}
