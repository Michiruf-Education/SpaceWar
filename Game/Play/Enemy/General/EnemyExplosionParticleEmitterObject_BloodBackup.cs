using System;
using System.Drawing;
using Framework;
using Framework.Object;
using Framework.ParticleSystem;
using Framework.Render;
using Framework.Utilities;
using OpenTK;

namespace SpaceWar.Game.Play.Enemy.General {

	public class EnemyExplosionParticleEmitterObject_BloodBackup : GameObject {

		public const float EXPLOSION_DURATION = 1f;
		public const float EXPLOSION_PARTICLE_LIFETIME = 1f;
		public const float PARTICLE_INITIAL_SPEED_MIN = 0.001f; // NOTE DeltaTime was added
		public const float PARTICLE_INITIAL_SPEED_MAX = 0.005f; // NOTE DeltaTime was added

		private readonly MyTimer destroyTimer = new MyTimer();

		public EnemyExplosionParticleEmitterObject_BloodBackup(AbstractEnemy enemy) {
			Transform.WorldPosition = enemy.Transform.WorldPosition;
		}

		public override void OnStart() {
			base.OnStart();
			AddComponent(new ParticleSystemComponent(new ExplosionParticleEmitter()));
		}

		public override void Update() {
			base.Update();
			destroyTimer.DoEvery(EXPLOSION_DURATION, () => Scene.Current.Destroy(this), MyTimer.When.End);
		}

		public override void OnDestroy() {
			destroyTimer.Cancel();
			base.OnDestroy();
		}

		private class ExplosionParticleEmitter : ParticleEmitter {

			private static readonly Random VELOCITY_RANDOM = new Random();

			public float SpawnRate => 0.001f;
			public int TotalCount { get; } = 100;
			public Action<Particle> OnStart => particle => {
				var angle = VELOCITY_RANDOM.NextDouble(0, 360);
				var velocity = new Vector2((float) Math.Cos(angle), (float) Math.Sin(angle));
				velocity *= VELOCITY_RANDOM.NextFloat(PARTICLE_INITIAL_SPEED_MIN, PARTICLE_INITIAL_SPEED_MAX);
				Velocity = velocity;
			};
			public float Lifetime => EXPLOSION_PARTICLE_LIFETIME;

			public void LifetimeCallback(Particle particle, float duration) {
				Velocity = Velocity * 0.95f;
			}

			public Vector2 Acceleration => Vector2.Zero;
			public Vector2 Velocity { get; private set; }
			public Func<RenderComponent> InitializeVisualComponent => () =>
				new RenderBoxComponent(0.005f, 0.005f).Fill(Color.Red);
			public Func<Color, float> ColorOverTimeFunc { get; }
		}
	}

}
