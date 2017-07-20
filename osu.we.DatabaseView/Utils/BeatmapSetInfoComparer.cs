using System;
using System.Collections.Generic;
using osu.Game.Database;

namespace osu.we.DatabaseView
{
	public class BeatmapSetInfoComparer : IEqualityComparer<BeatmapSetInfo>
	{
		public bool Equals(BeatmapSetInfo x, BeatmapSetInfo y)
		{
			return x.ID == y.ID;
		}

		public int GetHashCode(BeatmapSetInfo obj)
		{
			return obj.Hash.GetHashCode();
		}
	}
}
