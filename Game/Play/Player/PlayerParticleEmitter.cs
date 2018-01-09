using System;
using System.Drawing;
using Framework;
using Framework.Object;
using Framework.ParticleSystem;
using Framework.Render;
using OpenTK;

namespace SpaceWar.Game.Play.Player {

	public class PlayerParticleEmitter : ParticleEmitter {

		public float SpawnRate { get; }
		public int TotalCount { get; }
		public float Lifetime { get; } = 5f;
		public Vector2 Acceleration { get; } = Vector2.Zero;
		public Vector2 Velocity { get; } = Vector2.One;
		public Func<RenderComponent> InitializeVisualComponent { get; } = () => new RenderBoxComponent(0.1f, 0.1f).Fill(Color.Red);
	}

}
