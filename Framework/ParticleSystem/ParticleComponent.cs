namespace Framework.ParticleSystem.Components {

	public class ParticleComponent : Component {

		private Particle particle;
		public Particle Particle => particle ?? (particle = GameObject as Particle);
	}

}
