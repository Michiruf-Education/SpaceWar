using Zenseless.Geometry;

namespace Framework.Collision.Collider {

	public class CircleCollider : ColliderComponent {

		public CircleCollider(Circle circle) : base(new CircleShape(circle)) {
		}

		public CircleCollider(float radius) : this(
			new Circle(0.0f, 0.0f, radius)) {
		}
	}

}
