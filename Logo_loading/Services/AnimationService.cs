using System;
using System.Windows;
using System.Windows.Media.Animation;
using Logo_loading.Constants;

namespace Logo_loading.Services
{
    /// <summary>
    /// Service responsible for managing all animation operations in the Logo Loading application.
    /// Provides centralized control over starting, stopping, and restarting animations.
    /// Implements proper error handling and logging for animation operations.
    /// </summary>
    public class AnimationService
    {
        #region Events
        /// <summary>
        /// Raised when an animation operation encounters an error.
        /// </summary>
        public event EventHandler<string> AnimationError;

        /// <summary>
        /// Raised when the animation state changes (started/stopped).
        /// </summary>
        public event EventHandler<bool> AnimationStateChanged;
        #endregion

        #region Public Methods
        /// <summary>
        /// Starts all logo animations with proper error handling.
        /// </summary>
        /// <param name="target">The target FrameworkElement for the animations</param>
        /// <param name="letterFadeStoryboard">Storyboard for letter fade animations</param>
        /// <param name="loadingDotsStoryboard">Storyboard for loading dots animations</param>
        /// <param name="colorWaveStoryboard">Storyboard for color wave animations</param>
        /// <returns>True if animations started successfully, false otherwise</returns>
        public bool StartAnimations(FrameworkElement target, 
                                  Storyboard letterFadeStoryboard, 
                                  Storyboard loadingDotsStoryboard, 
                                  Storyboard colorWaveStoryboard)
        {
            try
            {
                ValidateParameters(target, letterFadeStoryboard, loadingDotsStoryboard, colorWaveStoryboard);

                // Start all animations simultaneously for perfect synchronization
                letterFadeStoryboard?.Begin(target);
                loadingDotsStoryboard?.Begin(target);
                colorWaveStoryboard?.Begin(target);

                OnAnimationStateChanged(true);
                return true;
            }
            catch (Exception ex)
            {
                OnAnimationError($"Failed to start animations: {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// Stops all logo animations with proper cleanup.
        /// </summary>
        /// <param name="target">The target FrameworkElement for the animations</param>
        /// <param name="letterFadeStoryboard">Storyboard for letter fade animations</param>
        /// <param name="loadingDotsStoryboard">Storyboard for loading dots animations</param>
        /// <param name="colorWaveStoryboard">Storyboard for color wave animations</param>
        /// <returns>True if animations stopped successfully, false otherwise</returns>
        public bool StopAnimations(FrameworkElement target, 
                                 Storyboard letterFadeStoryboard, 
                                 Storyboard loadingDotsStoryboard, 
                                 Storyboard colorWaveStoryboard)
        {
            try
            {
                ValidateParameters(target, letterFadeStoryboard, loadingDotsStoryboard, colorWaveStoryboard);

                // Stop all animations
                letterFadeStoryboard?.Stop(target);
                loadingDotsStoryboard?.Stop(target);
                colorWaveStoryboard?.Stop(target);

                OnAnimationStateChanged(false);
                return true;
            }
            catch (Exception ex)
            {
                OnAnimationError($"Failed to stop animations: {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// Restarts all animations by stopping them first, then starting them again.
        /// </summary>
        /// <param name="target">The target FrameworkElement for the animations</param>
        /// <param name="letterFadeStoryboard">Storyboard for letter fade animations</param>
        /// <param name="loadingDotsStoryboard">Storyboard for loading dots animations</param>
        /// <param name="colorWaveStoryboard">Storyboard for color wave animations</param>
        /// <returns>True if animations restarted successfully, false otherwise</returns>
        public bool RestartAnimations(FrameworkElement target, 
                                    Storyboard letterFadeStoryboard, 
                                    Storyboard loadingDotsStoryboard, 
                                    Storyboard colorWaveStoryboard)
        {
            try
            {
                // Stop animations first
                if (!StopAnimations(target, letterFadeStoryboard, loadingDotsStoryboard, colorWaveStoryboard))
                {
                    return false;
                }

                // Start animations again
                return StartAnimations(target, letterFadeStoryboard, loadingDotsStoryboard, colorWaveStoryboard);
            }
            catch (Exception ex)
            {
                OnAnimationError($"Failed to restart animations: {ex.Message}");
                return false;
            }
        }
        #endregion

        #region Private Methods
        /// <summary>
        /// Validates that the target parameter is not null.
        /// Storyboards can be null and will be safely handled with null-conditional operators.
        /// </summary>
        private void ValidateParameters(FrameworkElement target, params Storyboard[] storyboards)
        {
            if (target == null)
                throw new ArgumentNullException(nameof(target), "Target element cannot be null");

            // Note: Storyboards can be null - they are handled with null-conditional operators in the calling methods
        }

        /// <summary>
        /// Raises the AnimationError event.
        /// </summary>
        private void OnAnimationError(string errorMessage)
        {
            AnimationError?.Invoke(this, errorMessage);
        }

        /// <summary>
        /// Raises the AnimationStateChanged event.
        /// </summary>
        private void OnAnimationStateChanged(bool isAnimating)
        {
            AnimationStateChanged?.Invoke(this, isAnimating);
        }
        #endregion
    }
}