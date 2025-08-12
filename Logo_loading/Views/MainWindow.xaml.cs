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

            // Build letter animations after loaded and start
            Loaded += OnLoadedBuildAndStart;
        }

        private void InitializeViewModel()
        {
            _viewModel = new LogoViewModel();
            DataContext = _viewModel;
            Title = ApplicationConstants.WINDOW_TITLE;
        }

        // Load fixed animations from XAML resources
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

        // Simple text setup without complex animations
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
            // Build letter animations after layout is complete, then start
            Dispatcher.InvokeAsync(() =>
            {
                BuildFixedLetterAnimations();
                StartAnimations();
            }, DispatcherPriority.Loaded);
        }

        // Build simple letter animations with FIXED timing for the actual generated letters
        private void BuildFixedLetterAnimations()
        {
            var lettersRepeater = FindName("LettersRepeater") as ItemsControl;
            if (lettersRepeater == null || lettersRepeater.Items.Count == 0)
            {
                _letterFadeStoryboard = new Storyboard(); // empty storyboard
                return;
            }

            var storyboard = new Storyboard
            {
                RepeatBehavior = RepeatBehavior.Forever,
                Duration = TimeSpan.FromSeconds(ApplicationConstants.TOTAL_CYCLE_DURATION)
            };

            // Fixed timing - simple and predictable
            double startTime = ApplicationConstants.LETTER_START_TIME;
            
            for (int i = 0; i < lettersRepeater.Items.Count; i++)
            {
                var container = lettersRepeater.ItemContainerGenerator.ContainerFromIndex(i) as ContentPresenter;
                if (container == null) continue;

                container.ApplyTemplate();
                var letterText = container.ContentTemplate?.FindName("LetterText", container) as FrameworkElement;
                if (letterText == null) continue;

                // Calculate FIXED timing for this letter
                var letterTriggerTime = startTime + (i * ApplicationConstants.LETTER_INTERVAL);
                var restoreTime = letterTriggerTime + 0.2 + (i * 0.05); // Simple fixed restore timing

                var keyFrameAnimation = new DoubleAnimationUsingKeyFrames();

                // Fade to dim
                keyFrameAnimation.KeyFrames.Add(new EasingDoubleKeyFrame(
                    ApplicationConstants.LETTER_ACTIVE_OPACITY,
                    KeyTime.FromTimeSpan(TimeSpan.FromSeconds(letterTriggerTime))
                ));

                // Restore to full
                keyFrameAnimation.KeyFrames.Add(new EasingDoubleKeyFrame(
                    ApplicationConstants.LETTER_INACTIVE_OPACITY,
                    KeyTime.FromTimeSpan(TimeSpan.FromSeconds(restoreTime))
                ));

                Storyboard.SetTarget(keyFrameAnimation, letterText);
                Storyboard.SetTargetProperty(keyFrameAnimation, new PropertyPath(UIElement.OpacityProperty));
                storyboard.Children.Add(keyFrameAnimation);
            }

            _letterFadeStoryboard = storyboard;
        }

        private void StartAnimations()
        {
            try
            {
                // Start all animations using FIXED timing
                _viewModel?.StartAnimations(this, _letterFadeStoryboard, _loadingDotsStoryboard, _colorWaveStoryboard);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to start animations: {ex.Message}",
                    "Animation Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        protected override void OnKeyDown(System.Windows.Input.KeyEventArgs e)
        {
            base.OnKeyDown(e);

            switch (e.Key)
            {
                case System.Windows.Input.Key.Space:
                    RestartAnimations();
                    break;
                case System.Windows.Input.Key.Escape:
                    StopAnimations();
                    break;
            }
        }

        private void RestartAnimations()
        {
            try
            {
                _viewModel?.RestartAnimations(this, _letterFadeStoryboard, _loadingDotsStoryboard, _colorWaveStoryboard);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to restart animations: {ex.Message}",
                    "Animation Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void StopAnimations()
        {
            try
            {
                _viewModel?.StopAnimations(this, _letterFadeStoryboard, _loadingDotsStoryboard, _colorWaveStoryboard);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to stop animations: {ex.Message}",
                    "Animation Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
