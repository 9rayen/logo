using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Animation;
using Logo_loading.Constants;
using Logo_loading.ViewModels;

namespace Logo_loading.Views
{
    /// <summary>
    /// Main window for the Custom Logo Loading application.
    /// Handles the animated logo with tunnel wave effects and synchronized letter animations.
    /// Implements MVVM pattern with proper separation of concerns.
    /// Features smooth continuous animations with professional timing and effects.
    /// </summary>
    public partial class MainWindow : Window
    {
        #region Private Fields
        private Storyboard _letterFadeStoryboard;
        private Storyboard _loadingDotsStoryboard;
        private Storyboard _colorWaveStoryboard;
        private LogoViewModel _viewModel;
        #endregion

        #region Constructor
        /// <summary>
        /// Initializes a new instance of the MainWindow class.
        /// Sets up the MVVM pattern, loads animations, and configures the loading text.
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
            InitializeViewModel();
            InitializeAnimations();
            SetupLoadingText();
        }
        #endregion

        #region Initialization Methods
        /// <summary>
        /// Initializes the ViewModel and establishes the data binding context.
        /// </summary>
        private void InitializeViewModel()
        {
            _viewModel = new LogoViewModel();
            this.DataContext = _viewModel;
            
            // Set window title from constants
            this.Title = ApplicationConstants.WINDOW_TITLE;
        }

        /// <summary>
        /// Initializes and configures all animation storyboards.
        /// Retrieves storyboards from window resources and sets up event handlers.
        /// </summary>
        private void InitializeAnimations()
        {
            try
            {
                // Retrieve storyboards from window resources
                _letterFadeStoryboard = (Storyboard)this.Resources["LetterFadeAnimation"];
                _loadingDotsStoryboard = (Storyboard)this.Resources["LoadingDotsAnimation"];
                _colorWaveStoryboard = (Storyboard)this.Resources["ColorWaveAnimation"];

                // Validate that all storyboards were found
                ValidateStoryboards();

                // Start animations when window is loaded
                this.Loaded += MainWindow_Loaded;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to initialize animations: {ex.Message}", 
                              "Animation Error", 
                              MessageBoxButton.OK, 
                              MessageBoxImage.Warning);
            }
        }

        /// <summary>
        /// Sets up the loading text using the ViewModel's text management capabilities.
        /// </summary>
        private void SetupLoadingText()
        {
            try
            {
                _viewModel?.SetupLoadingText(this);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to setup loading text: {ex.Message}", 
                              "Text Setup Error", 
                              MessageBoxButton.OK, 
                              MessageBoxImage.Warning);
            }
        }

        /// <summary>
        /// Validates that all required storyboards were successfully loaded.
        /// </summary>
        private void ValidateStoryboards()
        {
            if (_letterFadeStoryboard == null)
                throw new InvalidOperationException("LetterFadeAnimation storyboard not found in resources");
            
            if (_loadingDotsStoryboard == null)
                throw new InvalidOperationException("LoadingDotsAnimation storyboard not found in resources");
            
            if (_colorWaveStoryboard == null)
                throw new InvalidOperationException("ColorWaveAnimation storyboard not found in resources");
        }
        #endregion

        #region Event Handlers
        /// <summary>
        /// Handles the window loaded event and starts all animations.
        /// </summary>
        /// <param name="sender">The source of the event</param>
        /// <param name="e">The event data</param>
        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            StartAnimations();
        }

        /// <summary>
        /// Handles key press events for animation control.
        /// Space: Restart animations | Escape: Stop animations
        /// </summary>
        /// <param name="sender">The source of the event</param>
        /// <param name="e">The key event data</param>
        private void MainWindow_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                switch (e.Key)
                {
                    case Key.Space:
                        RestartAnimations();
                        e.Handled = true;
                        break;
                    
                    case Key.Escape:
                        StopAnimations();
                        e.Handled = true;
                        break;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error handling key press: {ex.Message}", 
                              "Input Error", 
                              MessageBoxButton.OK, 
                              MessageBoxImage.Warning);
            }
        }
        #endregion

        #region Animation Control Methods
        /// <summary>
        /// Starts all logo animations using the ViewModel.
        /// </summary>
        private void StartAnimations()
        {
            try
            {
                _viewModel?.StartAnimations(this, _letterFadeStoryboard, _loadingDotsStoryboard, _colorWaveStoryboard);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to start animations: {ex.Message}", 
                              "Animation Error", 
                              MessageBoxButton.OK, 
                              MessageBoxImage.Error);
            }
        }

        /// <summary>
        /// Stops all animations using the ViewModel.
        /// </summary>
        private void StopAnimations()
        {
            try
            {
                _viewModel?.StopAnimations(this, _letterFadeStoryboard, _loadingDotsStoryboard, _colorWaveStoryboard);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to stop animations: {ex.Message}", 
                              "Animation Error", 
                              MessageBoxButton.OK, 
                              MessageBoxImage.Error);
            }
        }

        /// <summary>
        /// Restarts all animations using the ViewModel.
        /// </summary>
        private void RestartAnimations()
        {
            try
            {
                _viewModel?.RestartAnimations(this, _letterFadeStoryboard, _loadingDotsStoryboard, _colorWaveStoryboard);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to restart animations: {ex.Message}", 
                              "Animation Error", 
                              MessageBoxButton.OK, 
                              MessageBoxImage.Error);
            }
        }
        #endregion
    }
}