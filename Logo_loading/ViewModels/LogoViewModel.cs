using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Media.Animation;
using Logo_loading.Constants;
using Logo_loading.Services;

namespace Logo_loading.ViewModels
{
    /// <summary>
    /// ViewModel for the main window logo animations following MVVM pattern.
    /// Implements INotifyPropertyChanged for data binding scenarios.
    /// Manages animation state, status messages, and coordinates with animation services.
    /// Features smooth tunnel wave effects with perfectly synchronized letter animations.
    /// </summary>
    public class LogoViewModel : INotifyPropertyChanged
    {
        #region Private Fields
        private bool _isAnimating;
        private string _statusMessage;
        private readonly AnimationService _animationService;
        private readonly TextManagementService _textManagementService;
        #endregion

        #region Public Properties
        /// <summary>
        /// Gets or sets whether animations are currently running.
        /// Automatically updates the status message when changed.
        /// </summary>
        public bool IsAnimating
        {
            get => _isAnimating;
            private set
            {
                if (_isAnimating != value)
                {
                    _isAnimating = value;
                    OnPropertyChanged();
                    UpdateStatusForAnimationState(value);
                }
            }
        }

        /// <summary>
        /// Gets or sets the current status message displayed to the user.
        /// </summary>
        public string StatusMessage
        {
            get => _statusMessage;
            set
            {
                if (_statusMessage != value)
                {
                    _statusMessage = value;
                    OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Gets the current loading text from constants.
        /// </summary>
        public string LoadingText => ApplicationConstants.LOADING_TEXT;

        /// <summary>
        /// Gets the user instructions for controlling animations.
        /// </summary>
        public string UserInstructions => ApplicationConstants.USER_INSTRUCTIONS;
        #endregion

        #region Constructor
        /// <summary>
        /// Initializes a new instance of the LogoViewModel class.
        /// Sets up services and initializes the status message.
        /// </summary>
        public LogoViewModel()
        {
            // Initialize readonly services in constructor
            _animationService = new AnimationService();
            _textManagementService = new TextManagementService();
            
            InitializeServices();
            StatusMessage = ApplicationConstants.DEFAULT_STATUS_MESSAGE;
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Starts all logo animations using the animation service.
        /// </summary>
        /// <param name="target">The target FrameworkElement for animations</param>
        /// <param name="letterFadeStoryboard">Storyboard for letter fade animations</param>
        /// <param name="loadingDotsStoryboard">Storyboard for loading dots animations</param>
        /// <param name="colorWaveStoryboard">Storyboard for color wave animations</param>
        public void StartAnimations(FrameworkElement target, 
                                  Storyboard letterFadeStoryboard, 
                                  Storyboard loadingDotsStoryboard, 
                                  Storyboard colorWaveStoryboard)
        {
            var success = _animationService.StartAnimations(target, letterFadeStoryboard, loadingDotsStoryboard, colorWaveStoryboard);
            if (success)
            {
                IsAnimating = true;
            }
        }

        /// <summary>
        /// Stops all logo animations using the animation service.
        /// </summary>
        /// <param name="target">The target FrameworkElement for animations</param>
        /// <param name="letterFadeStoryboard">Storyboard for letter fade animations</param>
        /// <param name="loadingDotsStoryboard">Storyboard for loading dots animations</param>
        /// <param name="colorWaveStoryboard">Storyboard for color wave animations</param>
        public void StopAnimations(FrameworkElement target, 
                                 Storyboard letterFadeStoryboard, 
                                 Storyboard loadingDotsStoryboard, 
                                 Storyboard colorWaveStoryboard)
        {
            var success = _animationService.StopAnimations(target, letterFadeStoryboard, loadingDotsStoryboard, colorWaveStoryboard);
            if (success)
            {
                IsAnimating = false;
            }
        }

        /// <summary>
        /// Restarts all animations using the animation service.
        /// </summary>
        /// <param name="target">The target FrameworkElement for animations</param>
        /// <param name="letterFadeStoryboard">Storyboard for letter fade animations</param>
        /// <param name="loadingDotsStoryboard">Storyboard for loading dots animations</param>
        /// <param name="colorWaveStoryboard">Storyboard for color wave animations</param>
        public void RestartAnimations(FrameworkElement target, 
                                    Storyboard letterFadeStoryboard, 
                                    Storyboard loadingDotsStoryboard, 
                                    Storyboard colorWaveStoryboard)
        {
            _animationService.RestartAnimations(target, letterFadeStoryboard, loadingDotsStoryboard, colorWaveStoryboard);
        }

        /// <summary>
        /// Sets up the loading text using the text management service.
        /// </summary>
        /// <param name="target">The target element containing letter TextBlocks</param>
        /// <param name="customText">Optional custom text to display</param>
        public void SetupLoadingText(FrameworkElement target, string customText = null)
        {
            _textManagementService.SetupLoadingText(target, customText);
        }
        #endregion

        #region Private Methods
        /// <summary>
        /// Initializes the required services and sets up event handlers.
        /// </summary>
        private void InitializeServices()
        {
            // Subscribe to service events
            _animationService.AnimationError += OnAnimationError;
            _animationService.AnimationStateChanged += OnAnimationStateChanged;
            _textManagementService.TextSetupError += OnTextSetupError;
            _textManagementService.TextSetupCompleted += OnTextSetupCompleted;
        }

        /// <summary>
        /// Updates the status message based on animation state.
        /// </summary>
        /// <param name="isAnimating">Whether animations are currently running</param>
        private void UpdateStatusForAnimationState(bool isAnimating)
        {
            StatusMessage = isAnimating 
                ? "Perfect animation: tunnel wave + synchronized letters + loading dots!" 
                : "Animations stopped - Press SPACE to restart";
        }
        #endregion

        #region Event Handlers
        /// <summary>
        /// Handles animation errors from the animation service.
        /// </summary>
        private void OnAnimationError(object sender, string errorMessage)
        {
            StatusMessage = $"Animation error: {errorMessage}";
            IsAnimating = false;
        }

        /// <summary>
        /// Handles animation state changes from the animation service.
        /// </summary>
        private void OnAnimationStateChanged(object sender, bool isAnimating)
        {
            IsAnimating = isAnimating;
        }

        /// <summary>
        /// Handles text setup errors from the text management service.
        /// </summary>
        private void OnTextSetupError(object sender, string errorMessage)
        {
            StatusMessage = $"Text setup error: {errorMessage}";
        }

        /// <summary>
        /// Handles successful text setup completion from the text management service.
        /// </summary>
        private void OnTextSetupCompleted(object sender, string message)
        {
            StatusMessage = message;
        }
        #endregion

        #region INotifyPropertyChanged Implementation
        /// <summary>
        /// Occurs when a property value changes.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Raises the PropertyChanged event for the specified property.
        /// </summary>
        /// <param name="propertyName">The name of the property that changed</param>
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}