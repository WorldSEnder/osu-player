using System;
using System.Collections.Generic;
using Xwt;
using osu.Game.Database;
using osu.Framework.Desktop;
using osu.Desktop.Beatmaps.IO;
using osu.Game.Beatmaps.IO;
using osu.Framework.Platform;
using osu.Framework.Allocation;

namespace osu.we.DatabaseView
{
	public static class DesktopApplication
	{
		private static bool TryInitToolkit(ToolkitType type)
		{
			try
			{
				Application.Initialize(type);
				return true;
			}
			catch (System.Exception ex)
			{
				System.Diagnostics.Debug.Print($@"Failed to load {type} due to {ex}");
				return false;
			}
		}

		private static void Initialize()
		{
			if (TryInitToolkit(ToolkitType.Wpf))
			{
				return;
			}
			if (TryInitToolkit(ToolkitType.Gtk3))
			{
				return;
			}
			if (TryInitToolkit(ToolkitType.Gtk))
			{
				return;
			}
			if (TryInitToolkit(ToolkitType.Cocoa))
			{
				return;
			}
			if (TryInitToolkit(ToolkitType.XamMac))
			{
				return;
			}
			System.Diagnostics.Debug.Print("Failed to initialize, abort");
			throw new Exception();
		}

		public static void RunWindow(string[] args, GameHost host, DependencyContainer dependencies)
		{
			Initialize();
			using (var desktopController = dependencies.Cache(new DesktopController(dependencies)))
			using (var mainWindow = new WindowView(dependencies, host)
			{
				Title = "osu!AudioPlayer"
			})
			{
				mainWindow.Show();

				Application.Run();
			}
		}
	}
}
