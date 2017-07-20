using System;
using System.IO;
using System.Reflection;
using ManagedBass;
using osu.Framework.Allocation;
using osu.Framework.Audio;
using osu.Framework.Audio.Track;
using osu.Framework.IO.Stores;
using osu.Framework.Platform;
using osu.Framework.Threading;
using osu.Game.Database;
using osu.Game.Overlays.Settings.Sections.Audio;

namespace osu.we.DatabaseView
{
	public class AudioController : IDisposable
	{
        public ResourceStore<byte[]> Resources;

		public AudioDevicesSettings AudioSettings { get; private set; }
		public AudioManager Audio { get; private set; }

		public AudioController(DependencyContainer dependencies)
		{
			Resources = new ResourceStore<byte[]>();

			Audio = dependencies.Cache(new AudioManager(
				new NamespacedResourceStore<byte[]>(Resources, @"Tracks"),
				new NamespacedResourceStore<byte[]>(Resources, @"Samples"))
			{
				EventScheduler = new ThreadedScheduler()
			});
			// Since there is no game, we have to register the thread ourselves...
			var audioThread = typeof(AudioManager).GetField("Thread", BindingFlags.NonPublic | BindingFlags.Instance).GetValue(Audio) as AudioThread;
			dependencies.Get<GameHost>().RegisterThread( audioThread );
			Audio.OnNewDevice += (device) => System.Diagnostics.Debug.Print("Audio device {0} found.", device);
			Audio.OnLostDevice += (device) => System.Diagnostics.Debug.Print("Audio device {0} list", device);
		}

		public void play(BeatmapInfo Beatmap, BeatmapDatabase Database)
		{
			using (var reader = Database.GetReader(Beatmap.BeatmapSet))
			using (var file = reader.GetStream(Beatmap.Metadata?.AudioFile ?? Beatmap.BeatmapSet.Metadata.AudioFile))
			{
				MemoryStream ms = new MemoryStream();
				file.CopyTo(ms);
				System.Diagnostics.Debug.Print("Loaded file of size {0}", ms.Length);

				Track track = new TrackBass(ms);

				Audio.Track.SetExclusive(track);
				track.Start();
			}
		}

		public void Dispose()
		{
			Audio?.Dispose();
			AudioSettings?.Dispose();
		}
	}
}
