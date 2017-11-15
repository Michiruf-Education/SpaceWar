using System;

namespace Framework.Object {

	public static class Time {

		public static float DeltaTime { get; internal set; }

		public static DateTime StartTimeDate { get; private set; } = DateTime.MinValue;
		
		public static float StartTime => (float) (StartTimeDate - DateTime.MinValue).TotalMilliseconds; 

		public static float ElapsedTime => (float) (StartTimeDate - DateTime.Now).TotalMilliseconds;

		public static void SetStartTimeIfNotSetYet() {
			if (StartTimeDate != DateTime.MinValue) {
				return;
			}
			StartTimeDate = DateTime.Now;
		}
	}

}
