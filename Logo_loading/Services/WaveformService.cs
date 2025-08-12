using System;
using System.Windows;
using System.Windows.Shapes;
using Logo_loading.Constants;

namespace Logo_loading.Services
{
    /// <summary>
    /// Service responsible for dynamically generating and updating the waveform line
    /// based on the text length. Longer text results in longer waveform lines.
    /// </summary>
    public class WaveformService
    {
        public event EventHandler<string> WaveformUpdated;
        public event EventHandler<string> WaveformError;

        /// <summary>
        /// Gets the last calculated line length for the current text.
        /// </summary>
        public double LastCalculatedLineLength { get; private set; } = ApplicationConstants.BASE_LINE_LENGTH;

        /// <summary>
        /// Updates the waveform path geometry based on the text length.
        /// </summary>
        /// <param name="target">The target element containing the WaveformPath</param>
        /// <param name="textLength">The length of the text to accommodate</param>
        public void UpdateWaveformForText(FrameworkElement target, int textLength)
        {
            try
            {
                if (target == null)
                    throw new ArgumentNullException(nameof(target));

                var waveformPath = target.FindName("WaveformPath") as Path;
                if (waveformPath == null)
                    throw new InvalidOperationException("WaveformPath not found.");

                // Calculate the required line length
                var calculatedLength = ApplicationConstants.CalculateLineLength(textLength);
                LastCalculatedLineLength = calculatedLength;

                // Generate new geometry
                var newGeometry = ApplicationConstants.GenerateWaveformGeometry(calculatedLength);
                waveformPath.Data = newGeometry;

                WaveformUpdated?.Invoke(this, 
                    $"Waveform updated for text length {textLength}: line length {calculatedLength:F0}px");
            }
            catch (Exception ex)
            {
                WaveformError?.Invoke(this, ex.Message);
            }
        }

        /// <summary>
        /// Updates the waveform for a specific text string.
        /// </summary>
        /// <param name="target">The target element containing the WaveformPath</param>
        /// <param name="text">The text to accommodate</param>
        public void UpdateWaveformForText(FrameworkElement target, string text)
        {
            if (string.IsNullOrEmpty(text))
            {
                UpdateWaveformForText(target, 0);
                return;
            }

            UpdateWaveformForText(target, text.Length);
        }
    }
}