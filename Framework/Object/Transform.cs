using OpenTK;

namespace SpaceWar.Framework.Object {

	public class Transform {

		// TODO Implement a transform that holds the position of the GO, ...
		
		public GameObject GameObject { get; internal set; }

		public Vector2 Position { get; set; }
		public float Rotation { get; set; }
		public Vector2 Scaling { get; set; }

		public void Translate(Vector2 translation, Space space = Space.Local) {
			Translate(translation.X, translation.Y);
		}

		public void Translate(float x, float y, Space space = Space.Local) {
			if (space == Space.Local) {
				// NOTE Create a field for the position and use property as getter and setter for the field
				var position = Position;
				position.X += x;
				position.Y += y;
				return;
			}
			// TODO
		}

		public void Rotate(float angle) {
			Rotate(Position, angle);
		}

		public void Rotate(Vector2 axis, float angle, Space space = Space.Local) {
			Rotate(axis.X, axis.Y, angle, space);
		}

		public void Rotate(float xAxis, float yAxis, float angle, Space space = Space.Local) {
			// TODO
		}

		public void Scale(Vector2 scaling, Space space = Space.Local) {
			Scale(scaling.X, scaling.Y, space);
		}

		public void Scale(float xScaling, float yScaling, Space space = Space.Local) {
			// TODO
		}

		public Vector2 CalculatePointPosition(float x, float y) {
			return Vector2.Zero;
			// TODO!!!
		}
	}

}
