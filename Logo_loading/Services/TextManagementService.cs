using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Logo_loading.Constants;
using Logo_loading.Models;

namespace Logo_loading.Services
{
    /// <summary>
    /// Populates the ItemsControl named "LettersRepeater" with one item per character from the text.
    /// Simple service that sets up text without complex dynamic animations.
    /// </summary>
    public class TextManagementService
    {
        public event EventHandler<string> TextSetupCompleted;
        public event EventHandler<string> TextSetupError;

        private readonly WaveformService _waveformService;

        /// <summary>
        /// Initializes a new instance of the TextManagementService.
        /// </summary>
        public TextManagementService()
        {
            _waveformService = new WaveformService();
            _waveformService.WaveformError += (s, e) => TextSetupError?.Invoke(this, $"Waveform error: {e}");
        }

        /// <summary>
        /// Gets the last calculated line length for the current text.
        /// </summary>
        public double LastCalculatedLineLength => _waveformService.LastCalculatedLineLength;

        public void SetupLoadingText(FrameworkElement target, string customText = null)
        {
            try
            {
                if (target == null)
                    throw new ArgumentNullException(nameof(target));

                var repeater = target.FindName("LettersRepeater") as ItemsControl;
                if (repeater == null)
                    throw new InvalidOperationException("LettersRepeater ItemsControl not found.");

                var text = string.IsNullOrWhiteSpace(customText)
                    ? ApplicationConstants.LOADING_TEXT
                    : customText;

                // Create LetterModel objects
                repeater.ItemsSource = text.ToCharArray()
                                           .Select(c => new LetterModel(c.ToString()))
                                           .ToList();

                // Update waveform line length based on text length
                _waveformService.UpdateWaveformForText(target, text);

                // Update loading dots with fixed font size
                UpdateLoadingDotsFontSize(target, ApplicationConstants.FONT_SIZE);

                TextSetupCompleted?.Invoke(this, 
                    $"Loading text set to \"{text}\" with line length {LastCalculatedLineLength:F0}px - using FIXED timing.");
            }
            catch (Exception ex)
            {
                TextSetupError?.Invoke(this, ex.Message);
            }
        }

        /// <summary>
        /// Updates the font size of the loading dots to the fixed font size.
        /// </summary>
        /// <param name="target">The target element containing the dots</param>
        /// <param name="fontSize">The font size to apply</param>
        private void UpdateLoadingDotsFontSize(FrameworkElement target, double fontSize)
        {
            try
            {
                var dot1 = target.FindName("Dot1") as TextBlock;
                var dot2 = target.FindName("Dot2") as TextBlock;
                var dot3 = target.FindName("Dot3") as TextBlock;

                if (dot1 != null) dot1.FontSize = fontSize;
                if (dot2 != null) dot2.FontSize = fontSize;
                if (dot3 != null) dot3.FontSize = fontSize;
            }
            catch (Exception ex)
            {
                // Log error but don't throw - dots are not critical
                System.Diagnostics.Debug.WriteLine($"Failed to update dots font size: {ex.Message}");
            }
        }
    }
}