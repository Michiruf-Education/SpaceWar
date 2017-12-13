﻿using Framework;
using Framework.Collision;
using Zenseless.Geometry;

namespace SpaceWar.Game.Play.Enemy {

	public class EnemyCollisionController : Component, CollisionComponent {

		public void OnCollide(GameObject other) {
			if (!(other is Enemy otherEnemy)) {
				return;
			}

			// Enemies should not overlap
			var previousBox = GetComponent<BoxCollider>().GetTransformedRectCached();
			var thisBox = new Box2D(previousBox);
			var otherBox = otherEnemy.GetComponent<BoxCollider>().GetTransformedRectCached();

			thisBox.UndoOverlap(otherBox);
			var diffX = thisBox.MinX - previousBox.MinX;
			var diffY = thisBox.MinY - previousBox.MinY;
			// TODO See comment in PlayerCollisionController

			GameObject.Transform.Translate(diffX, diffY, Space.World);
		}
	}

}
