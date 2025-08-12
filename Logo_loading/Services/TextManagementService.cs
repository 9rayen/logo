using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Logo_loading.Constants;

namespace Logo_loading.Services
{
    /// <summary>
    /// Populates the ItemsControl named "LettersRepeater" with one item per character from the text.
    /// </summary>
    public class TextManagementService
    {
        public event EventHandler<string> TextSetupCompleted;
        public event EventHandler<string> TextSetupError;

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

                repeater.ItemsSource = text.ToCharArray()
                                           .Select(c => c.ToString())
                                           .ToList();

                TextSetupCompleted?.Invoke(this, $"Loading text set to \"{text}\".");
            }
            catch (Exception ex)
            {
                TextSetupError?.Invoke(this, ex.Message);
            }
        }
    }
}