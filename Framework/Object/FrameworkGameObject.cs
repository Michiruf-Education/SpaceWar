using SpaceWar.Framework.Algorithms.CollisionDetection;

namespace SpaceWar.Framework.Object {

	public class FrameworkGameObject : GameObject {
		
		private readonly CollisionDetection collisionDetection = new SimpleBox2DCollisionDetection();

		public override void Update() {
			base.Update();
			collisionDetection.DetectCollisions();
		}
	}

}
