namespace Framework.ParticleSystem {

	public class ParticleComponent : Component {

		private Particle particle;
		public Particle Particle => particle ?? (particle = GameObject as Particle);
	}

}
