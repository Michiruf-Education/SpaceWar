using System;
using System.Drawing;
using Framework;
using Framework.Object;
using Framework.ParticleSystem;
using Framework.Render;
using Framework.Utilities;
using OpenTK;

namespace SpaceWar.Game.Play.Enemy.General {

	public class EnemyExplosionParticleEmitterObject : GameObject {

		public const float EXPLOSION_DURATION = 0.1f;
		public const float EXPLOSION_PARTICLE_LIFETIME = 0.5f;
		public const float PARTICLE_INITIAL_SPEED_MIN = 0.03f;
		public const float PARTICLE_INITIAL_SPEED_MAX = 0.3f;
		public const int PARTICLE_ALPHA = 120;
		public const float PARTICLE_SPEED_LOSS_FACTOR = 0.95f;

		private readonly MyTimer destroyTimer = new MyTimer();


		public EnemyExplosionParticleEmitterObject(AbstractEnemy enemy) {
			Transform.WorldPosition = enemy.Transform.WorldPosition;
			AddComponent(new ParticleSystemComponent(new ExplosionParticleEmitter(enemy.ExplosionColor),
				EXPLOSION_PARTICLE_LIFETIME));
		}

		public override void OnStart() {
			base.OnStart();
			destroyTimer.DoOnce(EXPLOSION_DURATION, () => Scene.Current.Destroy(this));
		}

		public override void OnDestroy() {
			destroyTimer.Cancel();
			base.OnDestroy();
		}

		private class ExplosionParticleEmitter : ParticleEmitter {

			private static readonly Random VELOCITY_RANDOM = new Random();
			private static readonly Random BLEND_RANDOM = new Random();

			private readonly Color enemyExplosionColor;

			public ExplosionParticleEmitter(Color enemyExplosionColor) {
				this.enemyExplosionColor = enemyExplosionColor;
			}

			public float SpawnRate => 0.001f;
			public int TotalCount { get; } = 100;
			public Action<Particle> OnStart => particle => {
				var angle = VELOCITY_RANDOM.NextDouble(0, 360);
				var velocity = new Vector2((float) Math.Cos(angle), (float) Math.Sin(angle));
				velocity *= VELOCITY_RANDOM.NextFloat(PARTICLE_INITIAL_SPEED_MIN, PARTICLE_INITIAL_SPEED_MAX);
				particle.Velocity = velocity;
			};
			public float Lifetime => EXPLOSION_PARTICLE_LIFETIME;

			public void LifetimeCallback(Particle particle, float duration) {
				particle.Velocity *= PARTICLE_SPEED_LOSS_FACTOR;
			}

			public Vector2 Acceleration => Vector2.Zero;
			public Vector2 Velocity => Vector2.Zero;
			public Func<RenderComponent> InitializeVisualComponent => () =>
				new RenderBoxComponent(0.003f, 0.003f).Fill(Color.FromArgb(PARTICLE_ALPHA,
					Color.White.Blend(enemyExplosionColor, BLEND_RANDOM.NextFloat(0f, 1f))));
		}
	}

}
