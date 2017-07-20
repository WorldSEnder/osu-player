using System;
using osu.Framework.Allocation;

namespace osu.we.DatabaseView
{
	public class DesktopController : IDisposable
	{
		public AudioController AudioPlayer { get; }

		public DesktopController(DependencyContainer dependencies)
		{
			AudioPlayer = dependencies.Cache(new AudioController(dependencies));
		}

		public void Dispose()
		{
			AudioPlayer?.Dispose();
		}
	}
}
