using System;

namespace Framework.Object {

	public static class Time {

		public static float DeltaTime { get; internal set; }

		public static DateTime StartTimeDate { get; private set; } = DateTime.MinValue;

		[Obsolete("Irrelevant data?!")]
		public static float StartTime => (float) ((double) StartTimeDate.Ticks / TimeSpan.TicksPerMillisecond / 1000f);

		public static float ElapsedTime => (float) ((DateTime.Now - StartTimeDate).TotalMilliseconds / 1000f);

		public static void SetStartTimeIfNotSetYet() {
			if (StartTimeDate != DateTime.MinValue) {
				return;
			}
			StartTimeDate = DateTime.Now;
		}
	}

}
