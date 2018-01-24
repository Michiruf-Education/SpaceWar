using System;
using System.Drawing;
using Framework;
using Framework.Object;
using Framework.ParticleSystem;
using Framework.Render;
using Framework.Utilities;
using OpenTK;
using SpaceWar.Resources;

namespace SpaceWar.Game.Play.Player {

	public class PlayerParticleEmitter : ParticleEmitter {

		private static readonly Random RANDOM = new Random();
		private const float WIDTH_FACTOR = 0.35f * Player.PLAYER_SIZE;
		private const float BEHIND_FACTOR = 0.35f * Player.PLAYER_SIZE;

		private readonly Player player;

		public PlayerParticleEmitter(Player player) {
			this.player = player;
		}

		public float SpawnRate => 0.001f;
		public int TotalCount => 100;
		public Action<Particle> OnStart => particle => {
			// Do no draw particles if not moving
			if (!player.MovementController.IsMoving) {
				particle.Parent.RemoveChild(particle);
				return;
			}

			var random = RANDOM.NextFloat(-1f, 1f);
			var positionRandom = random * WIDTH_FACTOR;
			var lifetimeRandom = Math.Abs(random);

			// Get the direction. Instead of adding + 90 degress, we assign x to a and y to b!
			var direction = MathHelper.DegreesToRadians(player.Transform.WorldRotation);
			var a = Math.Cos(direction);
			var b = Math.Sin(direction);
			particle.Transform.WorldPosition += new Vector2(
				(float) (positionRandom * b - a * BEHIND_FACTOR),
				(float) (positionRandom * -a - b * BEHIND_FACTOR));

			// Change the lifetime depending on the position
			particle.Lifetime *= lifetimeRandom;
		};
		public float Lifetime => 0.2f;
		public Vector2 Acceleration => Vector2.Zero;
		public Vector2 Velocity => Vector2.Zero;
		public Func<RenderComponent> InitializeVisualComponent => () => new RenderTextureComponent("PlayerParticle",
			() => Resource.PlayerParticle, 0.02f, 0.02f);


		public void LifetimeCallback(Particle particle, float duration) {
			// Color the visual component by the remaining time
			var v = (RenderTextureComponent) particle.VisualComponent;
			var alpha = (int) (duration / particle.Lifetime * 100f);
			if (alpha > 0) {
				v.SetColorFilter(Color.FromArgb(alpha, player.PlayerColor));
			}
		}
	}

}
