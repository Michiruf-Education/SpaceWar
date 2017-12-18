using System;
using Framework.Object;

namespace Framework.Utilities {

	[Obsolete]
	public class DelayTimer {

		private float firstCallTime = -1f;

		public void DoOnlyOnce(float delay, Action action) {
			var currentTime = Time.ElapsedTime;
			if (Math.Abs(firstCallTime + 1f) < 0.001) {
				firstCallTime = currentTime;
				return;
			}

			if (Math.Abs(firstCallTime) + delay < currentTime) {
				return;
			}

			action?.Invoke();
		}
	}

}
