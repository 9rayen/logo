using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;
using System.Windows.Threading;
using Logo_loading.Constants;
using Logo_loading.ViewModels;

namespace Logo_loading.Views
{
    public partial class MainWindow : Window
    {
        private Storyboard _letterFadeStoryboard;
        private Storyboard _loadingDotsStoryboard;
        private Storyboard _colorWaveStoryboard;
        private LogoViewModel _viewModel;

        public MainWindow()
        {
            InitializeComponent();
            InitializeViewModel();
            InitializeAnimations();
            SetupLoadingText();

            // After the visual tree is ready, build the dynamic letter storyboard and start animations
            Loaded += OnLoadedBuildAndStart;
        }

        private void InitializeViewModel()
        {
            _viewModel = new LogoViewModel();
            DataContext = _viewModel;
            Title = ApplicationConstants.WINDOW_TITLE;
        }

        // Load only dots and wave storyboards from resources; letters are animated dynamically
        private void InitializeAnimations()
        {
            try
            {
                _loadingDotsStoryboard = (Storyboard)Resources["LoadingDotsAnimation"];
                _colorWaveStoryboard = (Storyboard)Resources["ColorWaveAnimation"];

                if (_loadingDotsStoryboard == null)
                    throw new InvalidOperationException("LoadingDotsAnimation storyboard not found in resources");
                if (_colorWaveStoryboard == null)
                    throw new InvalidOperationException("ColorWaveAnimation storyboard not found in resources");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to initialize animations: {ex.Message}",
                    "Animation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        // Populates the ItemsControl "LettersRepeater" with characters (no hard-coded letters)
        private void SetupLoadingText()
        {
            try
            {
                _viewModel?.SetupLoadingText(this);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to setup loading text: {ex.Message}",
                    "Text Setup Error", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void OnLoadedBuildAndStart(object sender, RoutedEventArgs e)
        {
            // Ensure item containers are generated before building animations
            Dispatcher.InvokeAsync(() =>
            {
                BuildDynamicLetterFadeStoryboard();
                StartAnimations();
            }, DispatcherPriority.Loaded);
        }

        private void StartAnimations()
        {
            try
            {
                // Start dynamic letters + resource-based dots and wave via the ViewModel/service
                _viewModel?.StartAnimations(this, _letterFadeStoryboard, _loadingDotsStoryboard, _colorWaveStoryboard);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to start animations: {ex.Message}",
                    "Animation Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        // Dynamically create per-letter opacity animations for each generated TextBlock in LettersRepeater
        private void BuildDynamicLetterFadeStoryboard()
        {
            var lettersRepeater = FindName("LettersRepeater") as ItemsControl;
            if (lettersRepeater == null || lettersRepeater.Items.Count == 0)
            {
                _letterFadeStoryboard = new Storyboard(); // no-op if no letters
                return;
            }

            var sb = new Storyboard
            {
                RepeatBehavior = RepeatBehavior.Forever,
                Duration = TimeSpan.FromSeconds(ApplicationConstants.TOTAL_CYCLE_DURATION)
            };

            // Align with wave: previous code offset start by -0.3 from LETTER_START_TIME (~2.5)
            double start = ApplicationConstants.LETTER_START_TIME - 0.3;

            // How long it takes to fade DOWN to the dim state (increase for smoother/longer dim)
            double fadeDownDuration = 0.8; // seconds (try 0.25–0.40)

            // Quick restore and stagger so letters relight one-by-one right after the wave passes
            double restoreDelay = 1.5; // seconds after the "hit" when restore begins
            double restoreStagger = 0.2; // extra per-letter delay for cascading effect

            for (int i = 0; i < lettersRepeater.Items.Count; i++)
            {
                var container = lettersRepeater.ItemContainerGenerator.ContainerFromIndex(i) as ContentPresenter;
                if (container == null) continue;

                container.ApplyTemplate();
                var letterText = container.ContentTemplate.FindName("LetterText", container) as FrameworkElement;
                if (letterText == null) continue;

                var kf = new DoubleAnimationUsingKeyFrames();

                // Start each track at full opacity implicitly (current value).
                // Fade DOWN to dim (0.2) over fadeDownDuration after the wave "hit" moment
                var dimKey = new EasingDoubleKeyFrame(
                    ApplicationConstants.LETTER_ACTIVE_OPACITY, // 0.2
                    KeyTime.FromTimeSpan(TimeSpan.FromSeconds(
                        start + i * ApplicationConstants.LETTER_INTERVAL + fadeDownDuration
                    ))
                )
                {
                    EasingFunction = new QuadraticEase { EasingMode = EasingMode.EaseIn }
                };
                kf.KeyFrames.Add(dimKey);

                // Fade UP (restore) to full (1.0) after a short delay + stagger
                var restoreKey = new EasingDoubleKeyFrame(
                    ApplicationConstants.LETTER_INACTIVE_OPACITY, // 1.0
                    KeyTime.FromTimeSpan(TimeSpan.FromSeconds(
                        start + i * ApplicationConstants.LETTER_INTERVAL + fadeDownDuration + restoreDelay + i * restoreStagger
                    ))
                )
                {
                    EasingFunction = new QuadraticEase { EasingMode = EasingMode.EaseOut }
                };
                kf.KeyFrames.Add(restoreKey);

                Storyboard.SetTarget(kf, letterText);
                Storyboard.SetTargetProperty(kf, new PropertyPath(UIElement.OpacityProperty));
                sb.Children.Add(kf);
            }

            // No global To=1.0 reset — each letter restores itself within the cycle
            _letterFadeStoryboard = sb;
        }
    }
}
