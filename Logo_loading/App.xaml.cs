using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace Logo_loading
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// Application entry point for the Custom Logo Loading WPF application.
    /// Provides application-wide resources and manages application lifecycle.
    /// </summary>
    public partial class App : Application
    {
        #region Application Events
        /// <summary>
        /// Handles application startup events.
        /// Performs any necessary initialization before the main window is shown.
        /// </summary>
        /// <param name="e">Startup event arguments</param>
        protected override void OnStartup(StartupEventArgs e)
        {
            try
            {
                base.OnStartup(e);
                
                // Log application startup
                System.Diagnostics.Debug.WriteLine("Custom Logo Loading application started successfully.");
            }
            catch (Exception ex)
            {
                // Handle startup errors gracefully
                MessageBox.Show($"Application startup error: {ex.Message}", 
                              "Startup Error", 
                              MessageBoxButton.OK, 
                              MessageBoxImage.Error);
                
                // Shutdown the application if startup fails
                Shutdown(1);
            }
        }

        /// <summary>
        /// Handles application exit events.
        /// Performs cleanup operations before the application terminates.
        /// </summary>
        /// <param name="e">Exit event arguments</param>
        protected override void OnExit(ExitEventArgs e)
        {
            try
            {
                // Perform any necessary cleanup here
                System.Diagnostics.Debug.WriteLine("Custom Logo Loading application shutting down.");
                
                base.OnExit(e);
            }
            catch (Exception ex)
            {
                // Log exit errors but don't prevent shutdown
                System.Diagnostics.Debug.WriteLine($"Error during application exit: {ex.Message}");
            }
        }
        #endregion
    }
}
