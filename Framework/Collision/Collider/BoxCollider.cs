﻿using Zenseless.Geometry;

namespace Framework.Collision.Collider {

	public class BoxCollider : ColliderComponent {

		public BoxCollider(Box2D rect) : base(new BoxShape(rect)) {
		}

		public BoxCollider(float width, float height) :
			this(new Box2D(-width / 2, -height / 2,
				width, height)) {
		}
	}

}
