using System;
using System.Timers;

namespace Framework.Utilities {

	public class MyTimer {

		public Timer Timer { get; }

		private bool onceDone;

		public MyTimer() {
			Timer = new Timer();
		}

		public void DoEvery(float seconds, Action action, When when) {
			// If the timer is running, there is no need to do something
			if (Timer.Enabled) {
				return;
			}

			// May perform the action on start
			if (when == When.Start) {
				action?.Invoke();
			}
			
			// Create the callback
			void TimerElapsed(object sender, ElapsedEventArgs args) {
				// May perform the action on end
				if (when == When.End) {
					action?.Invoke();
				}

				// Remove the callback, because else it would still get called everytime and
				// everywhere else in the timer
				Timer.Elapsed -= TimerElapsed;
				// Stop the timer for re-use
				Timer.Stop();
			}

			// Set data an start
			Timer.Interval = seconds * 1000f;
			Timer.Elapsed += TimerElapsed;
			Timer.Start();
		}

		public void DoOnce(float seconds, Action action) {
			if (Timer.Enabled || onceDone) {
				return;
			}
			
			// Create the callback
			void TimerElapsed(object sender, ElapsedEventArgs args) {
				onceDone = true;

				// Perform the action on end
				action?.Invoke();

				// Remove the callback, because else it would still get called everytime and
				// everywhere else in the timer
				Timer.Elapsed -= TimerElapsed;
				// Stop the timer for re-use
				Timer.Stop();
			}

			// Set data an start
			Timer.Interval = seconds * 1000f;
			Timer.Elapsed += TimerElapsed;
			Timer.Start();
		}

		public enum When {

			Start,
			End
		}
	}

}
