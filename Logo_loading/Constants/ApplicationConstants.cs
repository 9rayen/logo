using System;
using System.Windows;
using System.Windows.Media;

namespace Logo_loading.Constants
{
    /// <summary>
    /// Contains all application-wide constants used throughout the Logo Loading application.
    /// This centralized approach makes it easy to modify timing, text, and animation settings.
    /// </summary>
    public static class ApplicationConstants
    {
        #region Text Configuration
        /// <summary>
        /// The main loading text displayed in the animation.
        /// Change this value to customize the loading message.
        /// </summary>
        public const string LOADING_TEXT = "rayennnnnn ";

        /// <summary>
        /// Maximum number of letters supported by the current XAML structure.
        /// If you need more letters, increase this and add corresponding TextBlocks in XAML.
        /// </summary>
        public const int MAX_LETTER_COUNT = 30;
        #endregion

        #region Waveform Line Length Configuration
        /// <summary>
        /// Base line length for text of reference length.
        /// This is the total length of the waveform line for REFERENCE_TEXT_LENGTH characters.
        /// </summary>
        public const double BASE_LINE_LENGTH = 400.0;

        /// <summary>
        /// Reference text length for the base line length.
        /// Text of this length will use the BASE_LINE_LENGTH.
        /// </summary>
        public const int REFERENCE_TEXT_LENGTH = 8;

        /// <summary>
        /// Minimum line length (for very short text).
        /// </summary>
        public const double MIN_LINE_LENGTH = 300.0;

        /// <summary>
        /// Maximum line length (for very long text).
        /// </summary>
        public const double MAX_LINE_LENGTH = 800.0;

        /// <summary>
        /// Length per character factor. This determines how much the line extends per additional character.
        /// </summary>
        public const double LENGTH_PER_CHARACTER = 15.0;

        /// <summary>
        /// Fixed font size for all text.
        /// </summary>
        public const double FONT_SIZE = 24.0;
        #endregion

        #region Waveform Geometry Configuration
        /// <summary>
        /// Starting straight line length before the wave pattern begins.
        /// </summary>
        public const double WAVE_START_STRAIGHT = 100.0;

        /// <summary>
        /// Number of wave cycles in the middle section.
        /// </summary>
        public const int WAVE_CYCLE_COUNT = 4;

        /// <summary>
        /// Width of each wave cycle.
        /// </summary>
        public const double WAVE_CYCLE_WIDTH = 40.0;

        /// <summary>
        /// Maximum wave amplitude (height of the peaks).
        /// </summary>
        public const double WAVE_MAX_AMPLITUDE = 130.0;
        #endregion

        #region Animation Timing (in seconds) - FIXED TIMING
        /// <summary>
        /// Total duration of one complete animation cycle - FIXED at 6 seconds.
        /// </summary>
        public const double TOTAL_CYCLE_DURATION = 6.0;

        /// <summary>
        /// Duration for each letter fade-in animation.
        /// </summary>
        public const double LETTER_FADE_DURATION = 0.4;

        /// <summary>
        /// Time interval between each letter animation.
        /// </summary>
        public const double LETTER_INTERVAL = 0.15;

        /// <summary>
        /// When the first letter starts animating.
        /// </summary>
        public const double LETTER_START_TIME = 3.2;

        /// <summary>
        /// Duration for each loading dot animation.
        /// </summary>
        public const double DOT_ANIMATION_DURATION = 0.3;

        /// <summary>
        /// When the loading dots start appearing.
        /// </summary>
        public const double DOTS_START_TIME = 0.5;

        /// <summary>
        /// Interval between each dot animation.
        /// </summary>
        public const double DOT_INTERVAL = 0.3;

        /// <summary>
        /// When letters reset for the next cycle.
        /// </summary>
        public const double LETTER_RESET_TIME = 5.7;

        /// <summary>
        /// Duration for the letter reset animation.
        /// </summary>
        public const double LETTER_RESET_DURATION = 0.2;
        #endregion

        #region Independent Dot Animation Timing
        /// <summary>
        /// Independent cycle duration for dot animations only - separate from main cycle.
        /// This allows dots to animate at their own pace, independent of wave and letters.
        /// </summary>
        public const double DOT_CYCLE_DURATION = 2.0;

        /// <summary>
        /// When the first independent dot starts appearing in its own cycle.
        /// </summary>
        public const double INDEPENDENT_DOT1_START_TIME = 0.1;

        /// <summary>
        /// When the second independent dot starts appearing in its own cycle.
        /// </summary>
        public const double INDEPENDENT_DOT2_START_TIME = 0.4;

        /// <summary>
        /// When the third independent dot starts appearing in its own cycle.
        /// </summary>
        public const double INDEPENDENT_DOT3_START_TIME = 0.7;

        /// <summary>
        /// Duration for each independent dot fade-in animation.
        /// </summary>
        public const double INDEPENDENT_DOT_FADE_IN_DURATION = 0.2;

        /// <summary>
        /// Duration for each independent dot fade-out animation.
        /// </summary>
        public const double INDEPENDENT_DOT_FADE_OUT_DURATION = 0.2;

        /// <summary>
        /// When the first independent dot fades out in its own cycle.
        /// </summary>
        public const double INDEPENDENT_DOT1_FADE_OUT_TIME = 1.2;

        /// <summary>
        /// When the second independent dot fades out in its own cycle.
        /// </summary>
        public const double INDEPENDENT_DOT2_FADE_OUT_TIME = 1.5;

        /// <summary>
        /// When the third independent dot fades out in its own cycle.
        /// </summary>
        public const double INDEPENDENT_DOT3_FADE_OUT_TIME = 1.8;
        #endregion

        #region Wave Animation
        /// <summary>
        /// Starting offset for the wave gradient.
        /// </summary>
        public const double WAVE_START_OFFSET = -0.3;

        /// <summary>
        /// Ending offset for the wave gradient.
        /// </summary>
        public const double WAVE_END_OFFSET = 1.5;

        /// <summary>
        /// Opacity for letters when they start.
        /// </summary>
        public const double LETTER_INACTIVE_OPACITY = 1.0;

        /// <summary>
        /// Opacity for letters after they are highlighted by the wave.
        /// </summary>
        public const double LETTER_ACTIVE_OPACITY = 0.2;

        /// <summary>
        /// How long a letter stays dim after the wave reaches it.
        /// </summary>
        public const double LETTER_RESTORE_DELAY = 0.20;

        /// <summary>
        /// Extra stagger so letters relight one-by-one.
        /// </summary>
        public const double LETTER_RESTORE_STAGGER = 0.03;
        #endregion

        #region User Interface
        /// <summary>
        /// Window title displayed in the title bar.
        /// </summary>
        public const string WINDOW_TITLE = "Custom Logo Loading";

        /// <summary>
        /// Default status message shown when the application is ready.
        /// </summary>
        public const string DEFAULT_STATUS_MESSAGE = "Ready - with independent dot timing animation!";
        #endregion

        #region Colors (Hex Values)
        /// <summary>
        /// Primary wave color (dark blue).
        /// </summary>
        public const string PRIMARY_WAVE_COLOR = "#2a3aa0";

        /// <summary>
        /// Secondary wave color (medium blue).
        /// </summary>
        public const string SECONDARY_WAVE_COLOR = "#3941d4";

        /// <summary>
        /// Bright wave center color (sky blue).
        /// </summary>
        public const string BRIGHT_WAVE_COLOR = "#87ceeb";

        /// <summary>
        /// Wave edge color (deep sky blue).
        /// </summary>
        public const string WAVE_EDGE_COLOR = "#00bfff";

        /// <summary>
        /// Text color for letters and dots.
        /// </summary>
        public const string TEXT_COLOR = "#333333";

        /// <summary>
        /// Background color for the window.
        /// </summary>
        public const string BACKGROUND_COLOR = "#F5F5F5";
        #endregion

        #region Line Length Utility Methods
        /// <summary>
        /// Calculates the appropriate line length based on the text length.
        /// </summary>
        /// <param name="textLength">The length of the text to display</param>
        /// <returns>The calculated line length</returns>
        public static double CalculateLineLength(int textLength)
        {
            if (textLength <= 0) return BASE_LINE_LENGTH;

            // Calculate additional length needed beyond reference
            double additionalLength = (textLength - REFERENCE_TEXT_LENGTH) * LENGTH_PER_CHARACTER;
            
            // Calculate new line length
            double calculatedLength = BASE_LINE_LENGTH + additionalLength;
            
            // Clamp to min/max bounds
            return Math.Max(MIN_LINE_LENGTH, Math.Min(MAX_LINE_LENGTH, calculatedLength));
        }

        /// <summary>
        /// Generates a dynamic waveform geometry based on the desired line length.
        /// </summary>
        /// <param name="totalLength">The total desired length of the line</param>
        /// <returns>A PathGeometry representing the waveform</returns>
        public static PathGeometry GenerateWaveformGeometry(double totalLength)
        {
            var geometry = new PathGeometry();
            var figure = new PathFigure { StartPoint = new Point(0, 0), IsClosed = false };

            // Start with straight line
            figure.Segments.Add(new LineSegment(new Point(WAVE_START_STRAIGHT, 0), true));

            // Calculate remaining length for waves and final straight line
            double remainingLength = totalLength - WAVE_START_STRAIGHT;
            double waveLength = WAVE_CYCLE_COUNT * WAVE_CYCLE_WIDTH;
            double finalStraightLength = remainingLength - waveLength;

            // Ensure we have reasonable proportions
            if (finalStraightLength < 50)
            {
                finalStraightLength = 50;
                waveLength = remainingLength - finalStraightLength;
            }

            // Add wave cycles with increasing amplitude
            double currentX = WAVE_START_STRAIGHT;
            double cycleWidth = waveLength / WAVE_CYCLE_COUNT;

            for (int i = 0; i < WAVE_CYCLE_COUNT; i++)
            {
                double amplitude = WAVE_MAX_AMPLITUDE * (i + 1) / WAVE_CYCLE_COUNT;
                double nextX = currentX + cycleWidth;

                // Create Bezier curve for this wave cycle
                double cp1X = currentX + cycleWidth * 0.375;
                double cp1Y = amplitude;
                double cp2X = currentX + cycleWidth * 0.625;
                double cp2Y = -amplitude;

                // Special handling for the 4th (last) wave cycle to end at -5 instead of 0
                double endY = (i == WAVE_CYCLE_COUNT - 1) ? -5 : 0;

                figure.Segments.Add(new BezierSegment(
                    new Point(cp1X, cp1Y),
                    new Point(cp2X, cp2Y),
                    new Point(nextX, endY),
                    true));

                currentX = nextX;
            }

            // Add the custom transition segment as requested by the user
            // This corresponds to: <BezierSegment Point1="260,0" Point2="275,0" Point3="280,0"/>
            // The segment smoothly transitions from the -5 Y position back to 0
            double transitionStartX = currentX;
            double transitionEndX = currentX + 20; // Transition over 20 units
            
            figure.Segments.Add(new BezierSegment(
                new Point(transitionStartX, 0), // Point1: start from Y=0 (even though we're at -5)
                new Point(transitionStartX + 15, 0), // Point2: control point at Y=0
                new Point(transitionEndX, 0), // Point3: end at Y=0
                true));

            currentX = transitionEndX;

            // Final straight line (adjust remaining length to account for custom segment)
            double remainingFinalLength = totalLength - currentX;
            if (remainingFinalLength > 0)
            {
                figure.Segments.Add(new LineSegment(new Point(totalLength, 0), true));
            }

            geometry.Figures.Add(figure);
            return geometry;
        }
        #endregion
    }
}