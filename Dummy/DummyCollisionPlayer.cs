using System;
using Framework;
using Framework.Collision;
using Framework.Input;
using Framework.Object;
using Framework.Render;
using SpaceWar.Resources;
using Zenseless.Geometry;

namespace SpaceWar.Game {

	public class DummyCollisionPlayer : GameObject {

		public DummyCollisionPlayer() {
			AddComponent(new DummyCollisionPlayerController());
			AddComponent(new RenderTextureComponent(Resource.background, 0.4f, 0.4f));
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
