using System;
using System.Linq;
using osu.Framework.Allocation;
using osu.Game.Database;
using Xwt;

namespace osu.we.DatabaseView
{
	public class BeatmapSetView : VBox
	{
		public BeatmapSetView(IGrouping<BeatmapSetInfo, BeatmapInfo> info, DependencyContainer dependencies)
		{
			var setInfo = info.Key;
			var setMetadata = setInfo.Metadata;
			PackStart(new Label($@"{setMetadata.ArtistUnicode ?? setMetadata.Artist} - {setMetadata.TitleUnicode ?? setMetadata.Title} [{setMetadata.Author}]"));

			var beatmaps = new VBox();
			foreach (var mapInfo in info.OrderBy(bm => bm.Difficulty.OverallDifficulty))
			{
				var diff = mapInfo.Difficulty ?? new BeatmapDifficulty();
				var metadata = mapInfo.Metadata ?? setMetadata;
				beatmaps.PackStart(new BeatmapView(dependencies, mapInfo));
			}
			beatmaps.MarginLeft = 10;
			PackStart(beatmaps);
		}
	}
}
