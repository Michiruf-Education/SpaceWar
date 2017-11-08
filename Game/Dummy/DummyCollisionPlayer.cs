using System;
using SpaceWar.Framework;
using SpaceWar.Framework.Collision;
using SpaceWar.Framework.Input;
using SpaceWar.Framework.Object;
using SpaceWar.Framework.Render;
using Zenseless.Geometry;

namespace SpaceWar.Game {

	public class DummyCollisionPlayer : GameObject {

		public DummyCollisionPlayer() {
			AddComponent(new DummyCollisionPlayerController());
			AddComponent(new RenderTextureComponent(Resources.background, 0.4f, 0.4f));
			AddComponent(new UnrotateableBoxCollider(new Box2D(-0.2f, -0.2f, 0.4f, 0.4f)));
		}

		public class DummyCollisionPlayerController : Component, UpdateComponent, CollisionComponent {

			public void Update() {
				if (InputHandler.KeyDown(InputActions.MoveUp)) {
					GameObject.Transform.Translate(0, 0.6f * Time.DeltaTime, Space.World);
				}
				if (InputHandler.KeyDown(InputActions.MoveDown)) {
					GameObject.Transform.Translate(0, -0.6f * Time.DeltaTime, Space.World);
				}
				if (InputHandler.KeyDown(InputActions.MoveLeft)) {
					GameObject.Transform.Translate(-0.6f * Time.DeltaTime, 0, Space.World);
				}
				if (InputHandler.KeyDown(InputActions.MoveRight)) {
					GameObject.Transform.Translate(0.6f * Time.DeltaTime, 0, Space.World);
				}
			}

			public void OnCollide(GameObject other) {
				Console.WriteLine(DateTime.Now + ":" + DateTime.Now.Millisecond + " Player collision with " + other.GetType().Name);
			}
		}
	}

}
