using Framework.Algorithms.CollisionDetection;

namespace Framework.Engine {

	public class FrameworkEngineGameObject : GameObject {

		private readonly CollisionDetection collisionDetection = new NonOptimizedCollisionDetection();

		public override void Update() {
			// NOTE No need for the base call here?
			base.Update();
			collisionDetection.DetectCollisions();
		}
	}

}
