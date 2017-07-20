using System;
using osu.Framework.Desktop;
using osu.Desktop.Beatmaps.IO;
using osu.Game.Beatmaps.IO;
using osu.Framework.Platform;
using osu.Framework.Allocation;

namespace osu.we.DatabaseView
{
	public static class ApplicationMain
	{
		private delegate void ApplicationRunner(string[] args, GameHost host, DependencyContainer deps);

		private static ApplicationRunner SelectApplication(string[] args)
		{
			// TODO: offer media server or console based application as well
			return DesktopApplication.RunWindow;
		}

		[STAThread]
		static void Main(string[] args)
		{
			LegacyFilesystemReader.Register();
			OszArchiveReader.Register();

			using (var host = Host.GetSuitableHost(@"osu", true))
			{
				if (!host.IsPrimaryInstance)
				{
					Console.WriteLine("Not launching, there is already an instance of the osu host running");
					return;
				}

				using (var appController = new AppController(args, host))
				{
					var Application = SelectApplication(args);

					Application(args, host, appController.Dependencies);
				}
			}
		}
	}
}
