using System;
using Framework.Object;

namespace Framework.Utilities {

	[Obsolete]
	public class LimitedRateTimer {

		private float lastCallTime = -1f;

		public void DoOnlyEvery(float actionInterval, Action action) {
			var currentTime = Time.ElapsedTime;
			if (Math.Abs(lastCallTime + 1f) >= 0.001 && currentTime - lastCallTime < actionInterval) {
				return;
			}

			lastCallTime = currentTime;
			action?.Invoke();
		}
	}

}
