using System;
using System.Linq;
using osu.Framework.Allocation;
using osu.Framework.Extensions.IEnumerableExtensions;
using osu.Framework.Platform;
using osu.Framework.Threading;
using Xwt;

namespace osu.we.DatabaseView
{
	public class WindowView : Window
	{
		private GameHost Host;

		public WindowView(DependencyContainer dependencies, GameHost host) : base()
		{
			Host = host;
			Content = new BeatmapCollectionView(dependencies);

			var menu = new Menu();
			var fileMenu = new Menu();
			fileMenu.Items.Add(new MenuItem(Command.Copy) { Label = "Copy" });
			menu.Items.Add(new MenuItem() { Label = "File", SubMenu = fileMenu });
			MainMenu = menu;
		}

		protected override void OnClosed()
		{
			Host.Exit();
			// Notice: no InputThread, etc... has been created, because we don't want to actually launch the game. we thus
			// dispose of the threads ourselves...
			var threads = Host.Threads;
			threads.ForEach(t => t.Exit());
			threads.Where(t => t.Running).ForEach(t => t.Thread.Join());

			Application.Exit();
		}
	}
}
