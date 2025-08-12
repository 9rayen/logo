using System;

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
        public const string LOADING_TEXT = "Rayen";

        /// <summary>
        /// Maximum number of letters supported by the current XAML structure.
        /// If you need more letters, increase this and add corresponding TextBlocks in XAML.
        /// </summary>
        public const int MAX_LETTER_COUNT = 13;
        #endregion

        #region Animation Timing (in seconds)
        /// <summary>
        /// Total duration of one complete animation cycle.
        /// All other timings are calculated relative to this value.
        /// </summary>
        public const double TOTAL_CYCLE_DURATION = 6.0;

        /// <summary>
        /// Duration for each letter fade-in animation.
        /// </summary>
        public const double LETTER_FADE_DURATION = 0.4;

        /// <summary>
        /// Time interval between each letter animation.
        /// Smaller values = faster letter sequence.
        /// </summary>
        public const double LETTER_INTERVAL = 0.15;

        /// <summary>
        /// When the first letter starts animating (relative to wave position).
        /// Letters now start fading 1 second earlier for better synchronization.
        /// </summary>
        public const double LETTER_START_TIME = 2.5;

        /// <summary>
        /// Duration for each loading dot animation.
        /// </summary>
        public const double DOT_ANIMATION_DURATION = 0.3;

        /// <summary>
        /// When the loading dots start appearing (independent of wave).
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

        #region Wave Animation
        /// <summary>
        /// Starting offset for the wave gradient (extends beyond visible area for tunnel effect).
        /// </summary>
        public const double WAVE_START_OFFSET = -0.3;

        /// <summary>
        /// Ending offset for the wave gradient (extends beyond visible area for tunnel effect).
        /// </summary>
        public const double WAVE_END_OFFSET = 1.5;

        /// <summary>
        /// Opacity for letters when they start (before wave highlights them).
        /// Letters now start with full opacity (black) and fade to lighter.
        /// </summary>
        public const double LETTER_INACTIVE_OPACITY = 1.0;

        /// <summary>
        /// Opacity for letters after they are highlighted by the wave.
        /// Letters fade to this lighter opacity when the wave passes.
        /// </summary>
        public const double LETTER_ACTIVE_OPACITY = 0.2;
        #endregion

        #region User Interface
        /// <summary>
        /// Window title displayed in the title bar.
        /// </summary>
        public const string WINDOW_TITLE = "Custom Logo Loading";

        /// <summary>
        /// Default status message shown when the application is ready.
        /// </summary>
        public const string DEFAULT_STATUS_MESSAGE = "Ready - perfect fade + blue line + tunnel wave effect!";
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

        // How long a letter stays dim after the wave reaches it
        public const double LETTER_RESTORE_DELAY = 0.20;   // make shorter/longer as you like

        // Extra stagger so letters relight one-by-one (0.0 = all at once)
        public const double LETTER_RESTORE_STAGGER = 0.03; // increase for more “cascading” feel


        #endregion
    }
}