using System;
using osu.Framework.Allocation;
using osu.Framework.Configuration;
using osu.Framework.Localisation;
using osu.Framework.Platform;
using osu.Game.Database;
using SQLite.Net;

namespace osu.we.DatabaseView
{
	public class DatabaseController : IDisposable
	{
		public DependencyContainer Dependencies { get; }

		public SQLiteConnection Connection { get; private set; }

		public FrameworkDebugConfigManager debugConfig { get; private set; }

		public FrameworkConfigManager config { get; private set; }

		public LocalisationEngine Localisation { get; private set; }

		public RulesetDatabase RulesetDatabase { get; private set; }
		/// <summary>
		/// Gets the beatmap database.
		/// Connection.Query<BeatmapMetadata>();
        /// Connection.Query<BeatmapDifficulty>();
        /// Connection.Query<BeatmapSetInfo>();
        /// Connection.Query<BeatmapInfo>();
		/// </summary>
		/// <value>The beatmap database.</value>
		public BeatmapDatabase BeatmapDatabase { get; private set; }

		public ScoreDatabase ScoreDatabase { get; private set; }

		public DatabaseController(GameHost host, DependencyContainer dependencies)
		{
			Dependencies = dependencies;
			Connection = host.Storage.GetDatabase(@"client");

			Dependencies.Cache(debugConfig = new FrameworkDebugConfigManager());
			Dependencies.Cache(config = new FrameworkConfigManager(host.Storage));
			Dependencies.Cache(Localisation = new LocalisationEngine(config));

			Dependencies.Cache(RulesetDatabase = new RulesetDatabase(host.Storage, Connection));
			Dependencies.Cache(BeatmapDatabase = new BeatmapDatabase(host.Storage, Connection, RulesetDatabase, host));
			Dependencies.Cache(ScoreDatabase = new ScoreDatabase(host.Storage, Connection, host, BeatmapDatabase));
		}

		public void Dispose()
		{
			Connection?.Dispose();
		}
	}
}
