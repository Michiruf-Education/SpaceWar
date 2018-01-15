﻿using System;
using System.Timers;
using OpenTK;

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
					// Note that we need to perform the action on the next Update() call
					// because may created game object would not be initialized correctly!
					void UpdateAction(object o, FrameEventArgs eventArgs) {
						Game.Instance.Window.UpdateFrame -= UpdateAction;
						action?.Invoke();
					}
					Game.Instance.Window.UpdateFrame += UpdateAction;
				}

				// Remove the callback, because else it would still get called everytime and
				// everywhere else in the timer
				Timer.Elapsed -= TimerElapsed;
				// Stop the timer for re-use
				Timer.Stop();
			}

			// Set data at start
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

		public void Cancel() {
			Timer.Stop();
			Timer.Close();
		}

		public enum When {

			Start,
			End
		}
	}

}
