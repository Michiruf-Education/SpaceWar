using Framework.Algorithms.CollisionDetection;

namespace Framework.Object {

	public class FrameworkGameObject : GameObject {

		private readonly CollisionDetection collisionDetection = new SimpleBox2DCollisionDetection();

		public override void Update() {
			// NOTE No need for the base call here?
			base.Update();
			collisionDetection.DetectCollisions();
		}
	}

}
