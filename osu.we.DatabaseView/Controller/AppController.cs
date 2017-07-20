using System;
using osu.Framework.Allocation;
using osu.Framework.Platform;

namespace osu.we.DatabaseView
{
	public class AppController : IDisposable
	{
		public GameHost Host { get; }
		public DependencyContainer Dependencies { get; } = new DependencyContainer();
		public DatabaseController DB { get; }

		public AppController(string[] args, GameHost host)
		{
			Host = Dependencies.Cache(host);
			DB = Dependencies.Cache(new DatabaseController(host, Dependencies));

			Dependencies.Cache(this);
			Dependencies.Initialize(this);
		}

		public void Dispose()
		{
			DB?.Dispose();
		}
	}
}
