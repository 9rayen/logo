using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Media.Animation;

namespace Logo_loading
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// MainWindow handles the custom logo with animated waveform and loading text
    /// Uses MVVM pattern with LogoViewModel for better code organization
    /// </summary>
    public partial class MainWindow : Window
    {
        #region Private Fields
        private Storyboard lFadingStoryboard;
        private Storyboard loadingDotsStoryboard;
        private Storyboard colorWaveStoryboard;
        private LogoViewModel viewModel;
        #endregion

        #region Constructor

        public MainWindow()
        {
            InitializeComponent();
            InitializeViewModel();
            InitializeAnimations();
        }

        #endregion

        #region Initialization Methods

        /// <summary>
        /// Initialize the ViewModel and set DataContext
        /// </summary>
        private void InitializeViewModel()
        {
            viewModel = new LogoViewModel();
            this.DataContext = viewModel;
        }

        /// <summary>
        /// Initialize and configure all animations for the logo
        /// </summary>
        private void InitializeAnimations()
        {
            // Get storyboards from window resources
            lFadingStoryboard = (Storyboard)this.Resources["LFadingAnimation"];
            loadingDotsStoryboard = (Storyboard)this.Resources["LoadingDotsAnimation"];
            colorWaveStoryboard = (Storyboard)this.Resources["ColorWaveAnimation"];

            // Start animations when window is loaded
            this.Loaded += MainWindow_Loaded;
        }

        #endregion

        #region Event Handlers

        /// <summary>
        /// Event handler for window loaded - starts all animations
        /// </summary>
        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            StartAnimations();
        }

        /// <summary>
        /// Handle key press events for animation control
        /// Space: Restart animations
        /// Escape: Stop animations
        /// </summary>
        private void MainWindow_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.Space:
                    RestartAnimations();
                    break;
                case Key.Escape:
                    StopAnimations();
                    break;
            }
        }

        #endregion

        #region Animation Control Methods

        /// <summary>
        /// Start all logo animations using the ViewModel
        /// </summary>
        private void StartAnimations()
        {
            viewModel?.StartAnimations(this, lFadingStoryboard, loadingDotsStoryboard, colorWaveStoryboard);
        }

        /// <summary>
        /// Stop all animations using the ViewModel
        /// </summary>
        private void StopAnimations()
        {
            viewModel?.StopAnimations(this, lFadingStoryboard, loadingDotsStoryboard, colorWaveStoryboard);
        }

        /// <summary>
        /// Restart animations using the ViewModel
        /// </summary>
        public void RestartAnimations()
        {
            viewModel?.RestartAnimations(this, lFadingStoryboard, loadingDotsStoryboard, colorWaveStoryboard);
        }

        #endregion

        #region Cleanup

        /// <summary>
        /// Override to clean up animations when window is closing
        /// </summary>
        protected override void OnClosing(System.ComponentModel.CancelEventArgs e)
        {
            StopAnimations();
            base.OnClosing(e);
        }

        #endregion
    }
}
