using System;
using System.IO;
using osu.Framework.Allocation;
using osu.Framework.Audio.Track;
using osu.Game.Database;
using Xwt;

namespace osu.we.DatabaseView
{
	public class BeatmapView : HBox
	{
		public BeatmapInfo Beatmap { get; private set; }
		public BeatmapDatabase Database { get; private set; }
		public AudioController Player { get; private set; }

		private Button PlayButton;

		public BeatmapView(DependencyContainer dependencies, BeatmapInfo beatmap)
		{
			Beatmap = beatmap;
			// FIXME: use dependencies.Initialize(this) and [BackgroundDependencyLoader]
			Database = dependencies.Get<BeatmapDatabase>();
			Player = dependencies.Get<AudioController>();

			var diff = beatmap.Difficulty ?? new BeatmapDifficulty();
			var metadata = beatmap.Metadata ?? beatmap.BeatmapSet.Metadata;

			PackStart(new Label($@"{metadata.ArtistUnicode ?? metadata.Artist} - {metadata.TitleUnicode ?? metadata.Title} [{metadata.Author}]"));
			PackEnd(PlayButton = new Button("play"));
			PlayButton.ButtonPressed += onPlay;
		}

		void onPlay(object sender, ButtonEventArgs e)
		{
			Player.play(Beatmap, Database);
		}
	}
}
