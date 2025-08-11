using System;
using System.Windows;
using System.Windows.Controls;
using Logo_loading.Constants;

namespace Logo_loading.Services
{
    /// <summary>
    /// Service responsible for managing text setup and letter visibility in the Logo Loading application.
    /// Handles dynamic text assignment to TextBlocks and manages their visibility based on text length.
    /// </summary>
    public class TextManagementService
    {
        #region Events
        /// <summary>
        /// Raised when there's an error during text setup operations.
        /// </summary>
        public event EventHandler<string> TextSetupError;

        /// <summary>
        /// Raised when text setup is completed successfully.
        /// </summary>
        public event EventHandler<string> TextSetupCompleted;
        #endregion

        #region Public Methods
        /// <summary>
        /// Sets up the loading text by updating existing TextBlocks with new text content.
        /// Automatically handles visibility for unused TextBlocks when text is shorter than maximum capacity.
        /// </summary>
        /// <param name="parent">The parent element containing the letter TextBlocks</param>
        /// <param name="text">The text to display (uses ApplicationConstants.LOADING_TEXT if null)</param>
        /// <returns>True if setup was successful, false otherwise</returns>
        public bool SetupLoadingText(FrameworkElement parent, string text = null)
        {
            try
            {
                // Use provided text or default from constants
                string textToDisplay = text ?? ApplicationConstants.LOADING_TEXT;
                ValidateText(textToDisplay);

                var letters = textToDisplay.ToCharArray();
                
                // Update existing TextBlocks with the new text
                for (int i = 0; i < Math.Min(letters.Length, ApplicationConstants.MAX_LETTER_COUNT); i++)
                {
                    var textBlock = FindLetterTextBlock(parent, i);
                    if (textBlock != null)
                    {
                        textBlock.Text = letters[i].ToString();
                        textBlock.Visibility = Visibility.Visible;
                    }
                }
                
                // Hide unused TextBlocks if the new text is shorter than maximum capacity
                HideUnusedTextBlocks(parent, letters.Length);

                OnTextSetupCompleted($"Text setup completed for: '{textToDisplay}'");
                return true;
            }
            catch (Exception ex)
            {
                OnTextSetupError($"Failed to setup text: {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// Resets all letter TextBlocks to their initial state with low opacity.
        /// </summary>
        /// <param name="parent">The parent element containing the letter TextBlocks</param>
        /// <returns>True if reset was successful, false otherwise</returns>
        public bool ResetLetterOpacity(FrameworkElement parent)
        {
            try
            {
                for (int i = 0; i < ApplicationConstants.MAX_LETTER_COUNT; i++)
                {
                    var textBlock = FindLetterTextBlock(parent, i);
                    if (textBlock != null && textBlock.Visibility == Visibility.Visible)
                    {
                        textBlock.Opacity = ApplicationConstants.LETTER_INACTIVE_OPACITY;
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                OnTextSetupError($"Failed to reset letter opacity: {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// Gets the current text from the TextBlocks.
        /// </summary>
        /// <param name="parent">The parent element containing the letter TextBlocks</param>
        /// <returns>The current text or empty string if failed</returns>
        public string GetCurrentText(FrameworkElement parent)
        {
            try
            {
                string result = string.Empty;
                
                for (int i = 0; i < ApplicationConstants.MAX_LETTER_COUNT; i++)
                {
                    var textBlock = FindLetterTextBlock(parent, i);
                    if (textBlock?.Visibility == Visibility.Visible)
                    {
                        result += textBlock.Text;
                    }
                    else
                    {
                        break; // Stop at first hidden TextBlock
                    }
                }

                return result;
            }
            catch (Exception ex)
            {
                OnTextSetupError($"Failed to get current text: {ex.Message}");
                return string.Empty;
            }
        }
        #endregion

        #region Private Methods
        /// <summary>
        /// Finds a letter TextBlock by its index using the naming convention "Letter{index}".
        /// </summary>
        /// <param name="parent">The parent element to search in</param>
        /// <param name="index">The index of the letter TextBlock</param>
        /// <returns>The TextBlock if found, null otherwise</returns>
        private TextBlock FindLetterTextBlock(FrameworkElement parent, int index)
        {
            return parent.FindName($"Letter{index}") as TextBlock;
        }

        /// <summary>
        /// Hides all TextBlocks that are not needed for the current text.
        /// </summary>
        /// <param name="parent">The parent element containing the TextBlocks</param>
        /// <param name="textLength">The length of the current text</param>
        private void HideUnusedTextBlocks(FrameworkElement parent, int textLength)
        {
            for (int i = textLength; i < ApplicationConstants.MAX_LETTER_COUNT; i++)
            {
                var textBlock = FindLetterTextBlock(parent, i);
                if (textBlock != null)
                {
                    textBlock.Visibility = Visibility.Collapsed;
                }
            }
        }

        /// <summary>
        /// Validates that the provided text is suitable for display.
        /// </summary>
        /// <param name="text">The text to validate</param>
        private void ValidateText(string text)
        {
            if (string.IsNullOrEmpty(text))
                throw new ArgumentException("Text cannot be null or empty", nameof(text));

            if (text.Length > ApplicationConstants.MAX_LETTER_COUNT)
                throw new ArgumentException($"Text length ({text.Length}) exceeds maximum supported length ({ApplicationConstants.MAX_LETTER_COUNT})", nameof(text));
        }

        /// <summary>
        /// Raises the TextSetupError event.
        /// </summary>
        private void OnTextSetupError(string errorMessage)
        {
            TextSetupError?.Invoke(this, errorMessage);
        }

        /// <summary>
        /// Raises the TextSetupCompleted event.
        /// </summary>
        private void OnTextSetupCompleted(string message)
        {
            TextSetupCompleted?.Invoke(this, message);
        }
        #endregion
    }
}