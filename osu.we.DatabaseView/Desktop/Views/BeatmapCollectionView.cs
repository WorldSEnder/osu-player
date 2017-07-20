using System;
using System.Linq;
using osu.Framework.Allocation;
using osu.Game.Database;
using Xwt;

namespace osu.we.DatabaseView
{
	public class BeatmapCollectionView : ScrollView
	{
		public BeatmapDatabase BeatmapDatabase { get; private set; }

		public BeatmapCollectionView(DependencyContainer dependencies)
		{
			BeatmapDatabase = dependencies.Get<BeatmapDatabase>();

			var vbox = new VBox();
			var beatmapsBySet = BeatmapDatabase.GetAllWithChildren<BeatmapInfo>(recursive: true)
								  .GroupBy(bm => bm.BeatmapSet, new BeatmapSetInfoComparer());
			foreach (var item in beatmapsBySet)
			{
				vbox.PackStart(new BeatmapSetView(item, dependencies));
			}

			Content = vbox;
		}
	}
}
