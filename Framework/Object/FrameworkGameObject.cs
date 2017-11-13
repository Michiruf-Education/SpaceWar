using Framework.Algorithms.CollisionDetection;

namespace Framework.Object {

	public class FrameworkGameObject : GameObject {
		
		private readonly CollisionDetection collisionDetection = new SimpleBox2DCollisionDetection();

		public override void Update() {
			base.Update();
			collisionDetection.DetectCollisions();
		}
	}

}
