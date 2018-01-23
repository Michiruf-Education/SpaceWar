using System;
using System.IO;
using NAudio.Wave;

namespace Framework.Sound {

	public class Sound {

		private Stream Stream { get; }
		private SoundFormat SoundFormat { get; }
		private WaveStream readerStream;
		public WaveStream ReaderStream => readerStream ?? (readerStream = CreateReaderStream());
		public float VolumeP { get; private set; } = 1f;
		public bool RepeatP { get; private set; }
		public float StartSeekP { get; private set; }
		public float RepeatSeekP { get; private set; }

		public Sound(Stream stream, SoundFormat format) {
			Stream = stream;
			SoundFormat = format;
		}

		private WaveStream CreateReaderStream() {
			switch (SoundFormat) {
				case SoundFormat.Mp3:
					return new Mp3FileReader(Stream);
				case SoundFormat.Wav:
					return new WaveFileReader(Stream);
				default:
					throw new ArgumentException("Invalid sound format to create stream");
			}
		}

		public Sound Volume(float volume) {
			VolumeP = volume;
			return this;
		}

		public Sound Repeat(bool repeat) {
			RepeatP = repeat;
			return this;
		}

		public Sound StartSeek(float startSeek, bool isAlsoRepeatSeek = true) {
			StartSeekP = startSeek;
			if (isAlsoRepeatSeek) {
				RepeatSeekP = startSeek;
			}
			return this;
		}

		public Sound RepeatSeek(float repeatSeek) {
			RepeatSeekP = repeatSeek;
			return this;
		}
	}

	public enum SoundFormat {

		Mp3,
		Wav
	}

}
