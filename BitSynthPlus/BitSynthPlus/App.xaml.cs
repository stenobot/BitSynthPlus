using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Activation;
using Windows.ApplicationModel.Store;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

namespace BitSynthPlus
{
    /// <summary>
    /// Provides application-specific behavior to supplement the default Application class.
    /// </summary>
    sealed partial class App : Application
    {
        /// <summary>
        /// Initializes the singleton application object.  This is the first line of authored code
        /// executed, and as such is the logical equivalent of main() or WinMain().
        /// </summary>
        public App()
        {
            Microsoft.ApplicationInsights.WindowsAppInitializer.InitializeAsync(
                Microsoft.ApplicationInsights.WindowsCollectors.Metadata |
                Microsoft.ApplicationInsights.WindowsCollectors.Session);
            this.InitializeComponent();
            this.Suspending += OnSuspending;
        }

        /// <summary>
        /// Invoked when the application is launched normally by the end user.  Other entry points
        /// will be used such as when the application is launched to open a specific file.
        /// </summary>
        /// <param name="e">Details about the launch request and process.</param>
        protected override void OnLaunched(LaunchActivatedEventArgs e)
        {
            int launchWidth = (int)(double)Current.Resources["LargeWidthBreakpoint"];
            int launchHeight = (int)Current.Resources["AppDefaultHeight"];
            int minHeight = (int)Current.Resources["AppMinHeight"];
            int minWidth = (int)Current.Resources["AppMinWidth"];

            // override the minimum height of the app window
            ApplicationView.GetForCurrentView().SetPreferredMinSize(new Size(minWidth, minHeight));
            
            // try to force app to launch at a set height and width
            ApplicationView.PreferredLaunchViewSize = new Size(launchWidth, launchHeight);
            ApplicationView.PreferredLaunchWindowingMode = ApplicationViewWindowingMode.PreferredLaunchViewSize;

#if DEBUG
            if (System.Diagnostics.Debugger.IsAttached)
            {
                this.DebugSettings.EnableFrameRateCounter = true;
            }
#endif

            Frame rootFrame = Window.Current.Content as Frame;

            // Do not repeat app initialization when the Window already has content,
            // just ensure that the window is active
            if (rootFrame == null)
            {
                // Create a Frame to act as the navigation context and navigate to the first page
                rootFrame = new Frame();

                rootFrame.NavigationFailed += OnNavigationFailed;

                if (e.PreviousExecutionState == ApplicationExecutionState.Terminated)
                {
                    //TODO: Load state from previously suspended application
                }

                // Place the frame in the current Window
                Window.Current.Content = rootFrame;
            }

            if (rootFrame.Content == null)
            {
                // When the navigation stack isn't restored navigate to the first page,
                // configuring the new page by passing required information as a navigation
                // parameter
                rootFrame.Navigate(typeof(KeyboardShell), e.Arguments);
            }

            ApplicationViewTitleBar titleBar = ApplicationView.GetForCurrentView().TitleBar;

            // get brushes
            SolidColorBrush bkgColor = Current.Resources["DarkAltBackgroundBrush"] as SolidColorBrush;
            SolidColorBrush btnHoverColor = Current.Resources["BitSynthAccentColorBrush"] as SolidColorBrush;
            SolidColorBrush btnPressedColor = Current.Resources["BitSynthAccentColorBrush"] as SolidColorBrush;

            // override colors!
            titleBar.BackgroundColor = bkgColor.Color;
            titleBar.ForegroundColor = Windows.UI.Colors.White;
            titleBar.ButtonBackgroundColor = bkgColor.Color;
            titleBar.ButtonForegroundColor = Windows.UI.Colors.White;
            titleBar.ButtonHoverBackgroundColor = btnHoverColor.Color;
            titleBar.ButtonHoverForegroundColor = Windows.UI.Colors.White;
            titleBar.ButtonPressedBackgroundColor = btnPressedColor.Color;
            titleBar.ButtonPressedForegroundColor = Windows.UI.Colors.White;
            titleBar.InactiveBackgroundColor = bkgColor.Color;
            titleBar.InactiveForegroundColor = Windows.UI.Colors.White;
            titleBar.ButtonInactiveBackgroundColor = bkgColor.Color;
            titleBar.ButtonInactiveForegroundColor = Windows.UI.Colors.White;

            // set the frame background to match our titlebar
            rootFrame.Background = bkgColor;


            // Ensure the current window is active
            Window.Current.Activate();



        }

        /// <summary>
        /// Invoked when Navigation to a certain page fails
        /// </summary>
        /// <param name="sender">The Frame which failed navigation</param>
        /// <param name="e">Details about the navigation failure</param>
        void OnNavigationFailed(object sender, NavigationFailedEventArgs e)
        {
            throw new Exception("Failed to load Page " + e.SourcePageType.FullName);
        }

        /// <summary>
        /// Invoked when application execution is being suspended.  Application state is saved
        /// without knowing whether the application will be terminated or resumed with the contents
        /// of memory still intact.
        /// </summary>
        /// <param name="sender">The source of the suspend request.</param>
        /// <param name="e">Details about the suspend request.</param>
        private void OnSuspending(object sender, SuspendingEventArgs e)
        {
            var deferral = e.SuspendingOperation.GetDeferral();
            //TODO: Save application state and stop any background activity
            deferral.Complete();
        }
    }
}
