using System;
using System.Collections.Generic;
using System.Linq;

using MonoTouch.Foundation;
using MonoTouch.UIKit;

namespace BrainThud.IPad
{
	// The UIApplicationDelegate for the application. This class is responsible for launching the 
	// User Interface of the application, as well as listening (and optionally responding) to 
	// application events from iOS.
	[Register ("AppDelegate")]
	public partial class AppDelegate : UIApplicationDelegate
	{
		// class-level declarations
		UISplitViewController splitViewController;
		UIWindow window;

		//
		// This method is invoked when the application has loaded and is ready to run. In this 
		// method you should instantiate the window, load the UI into it and then make the window
		// visible.
		//
		// You have 17 seconds to return from this method, or iOS will terminate your application.
		//
		public override bool FinishedLaunching (UIApplication app, NSDictionary options)
		{
			// create a new window instance based on the screen size
			window = new UIWindow (UIScreen.MainScreen.Bounds);
			
			var masterViewController = new RootViewController ();
			var masterNavigationController = new UINavigationController (masterViewController);
			var detailViewController = new DetailViewController ();
			var detailNavigationController = new UINavigationController (detailViewController);
			
			splitViewController = new UISplitViewController ();
			splitViewController.WeakDelegate = detailViewController;
			splitViewController.ViewControllers = new UIViewController[] {
				masterNavigationController,
				detailNavigationController
			};
			
			window.RootViewController = splitViewController;

			// make the window visible
			window.MakeKeyAndVisible ();
			
			return true;
		}
	}
}

