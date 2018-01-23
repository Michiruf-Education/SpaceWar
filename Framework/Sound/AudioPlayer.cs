using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;
using Framework.Utilities;
using NAudio.Wave;
using NAudio.Wave.SampleProviders;

namespace Framework.Sound {

	public class AudioPlayer {

		private static readonly AudioPlayer INSTANCE = new AudioPlayer();
		private static float GLOBAL_VOLUME = 0.1f;

		public static void SetGlobalVolume(float volume) {
			GLOBAL_VOLUME = volume;
		}

		// TODO Convert to Singleton Property
		public static AudioPlayer Get() {
			return INSTANCE;
		}

		private readonly Dictionary<string, Sound> soundCache = new Dictionary<string, Sound>();
		private readonly Dictionary<Sound, IWavePlayer> currentPlayers;

		private AudioPlayer() {
			currentPlayers = new Dictionary<Sound, IWavePlayer>();
		}

		public void Play(Sound sound) {
			if (sound == null) {
				throw new ArgumentNullException(nameof(sound));
			}

			// If the sound was not played before, init and add it
			var soundPlayer = currentPlayers.GetOrDefault(sound);
			if (soundPlayer == null) {
				soundPlayer = InitPlayerForSound(sound);
				currentPlayers.Add(sound, soundPlayer);
			}

			// Play the sound
//			soundPlayer.Stop(); // TODO Test -> Alle Sounds richtig abgespielt, kein Lag zwischendrin
			sound.ReaderStream.CurrentTime = TimeSpan.FromSeconds(sound.StartSeekP);
			soundPlayer.Play();
		}

		public void Play(string cacheName, Func<Sound> soundCreator, bool invalidateCache = false) {
			if (invalidateCache || !soundCache.ContainsKey(cacheName)) {
				soundCache.Add(cacheName, soundCreator.Invoke());
			}
			Play(soundCache.GetOrDefault(cacheName));
		}

		private IWavePlayer InitPlayerForSound(Sound sound) {
			// Wrap the stream into a volume sample provider to have a volume per sound
			var volumeSampleProvider = new VolumeSampleProvider(sound.ReaderStream.ToSampleProvider()) {
				Volume = sound.VolumeP * GLOBAL_VOLUME
			};

			// Init the sound out
			var soundOut = new WaveOut();
			soundOut.Init(volumeSampleProvider);

			// Handle restart if repeating
			void WaveOutOnPlaybackStopped(object o, StoppedEventArgs stoppedEventArgs) {
				if (!sound.RepeatP) {
					return;
				}
				sound.ReaderStream.CurrentTime = TimeSpan.FromSeconds(sound.RepeatSeekP);
				soundOut.Play();
			}
			soundOut.PlaybackStopped += WaveOutOnPlaybackStopped;

			return soundOut;
		}

		public void StopAll() {
			foreach (var c in currentPlayers) {
				c.Value.Stop();
				c.Value.Dispose();
				c.Key.ReaderStream.Dispose();
			}
			currentPlayers.Clear();
		}
	}

}
