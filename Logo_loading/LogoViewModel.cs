using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Media.Animation;

namespace Logo_loading
{
    /// <summary>
    /// ViewModel for the main window logo animations
    /// Implements INotifyPropertyChanged for future data binding scenarios
    /// </summary>
    public class LogoViewModel : INotifyPropertyChanged
    {
        #region Private Fields
        private bool _isAnimating;
        private string _statusMessage;
        #endregion

        #region Public Properties
        
        /// <summary>
        /// Gets or sets whether animations are currently running
        /// </summary>
        public bool IsAnimating
        {
            get => _isAnimating;
            set
            {
                _isAnimating = value;
                OnPropertyChanged();
                StatusMessage = value ? "Animations running..." : "Animations stopped";
            }
        }

        /// <summary>
        /// Gets or sets the current status message
        /// </summary>
        public string StatusMessage
        {
            get => _statusMessage;
            set
            {
                _statusMessage = value;
                OnPropertyChanged();
            }
        }

        #endregion

        #region Constructor

        public LogoViewModel()
        {
            StatusMessage = "Logo ready";
        }

        #endregion

        #region Animation Control Methods

        /// <summary>
        /// Start all logo animations
        /// </summary>
        public void StartAnimations(FrameworkElement target, Storyboard lFadingStoryboard, Storyboard loadingDotsStoryboard, Storyboard colorWaveStoryboard)
        {
            try
            {
                lFadingStoryboard?.Begin(target);
                loadingDotsStoryboard?.Begin(target);
                colorWaveStoryboard?.Begin(target);
                IsAnimating = true;
            }
            catch (Exception ex)
            {
                StatusMessage = $"Animation error: {ex.Message}";
                IsAnimating = false;
            }
        }

        /// <summary>
        /// Stop all logo animations
        /// </summary>
        public void StopAnimations(FrameworkElement target, Storyboard lFadingStoryboard, Storyboard loadingDotsStoryboard, Storyboard colorWaveStoryboard)
        {
            try
            {
                lFadingStoryboard?.Stop(target);
                loadingDotsStoryboard?.Stop(target);
                colorWaveStoryboard?.Stop(target);
                IsAnimating = false;
            }
            catch (Exception ex)
            {
                StatusMessage = $"Stop animation error: {ex.Message}";
            }
        }

        /// <summary>
        /// Restart all animations
        /// </summary>
        public void RestartAnimations(FrameworkElement target, Storyboard lFadingStoryboard, Storyboard loadingDotsStoryboard, Storyboard colorWaveStoryboard)
        {
            StopAnimations(target, lFadingStoryboard, loadingDotsStoryboard, colorWaveStoryboard);
            StartAnimations(target, lFadingStoryboard, loadingDotsStoryboard, colorWaveStoryboard);
        }

        #endregion

        #region INotifyPropertyChanged Implementation

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }
}