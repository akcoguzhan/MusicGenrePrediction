using Managers.Managers;
using Microsoft.Win32;
using System;
using System.IO;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Threading;
using Types;

namespace MainApplication.UserControls
{
    public partial class AudioPlayer : UserControl
    {
        #region Properties
        #region DependencyProperties
        #region TrackPath
        public static readonly DependencyProperty TrackPathProperty = DependencyProperty.Register("TrackPath", typeof(String), typeof(AudioPlayer), new UIPropertyMetadata(null));

        public String TrackPath
        {
            get
            {
                return (String)GetValue(TrackPathProperty);
            }
            set
            {
                SetValue(TrackPathProperty, value);
            }
        }
        #endregion TrackPath

        #region OriginalFileName
        public static readonly DependencyProperty OriginalFileProperty = DependencyProperty.Register("OriginalFile", typeof(String), typeof(AudioPlayer), new UIPropertyMetadata(null));

        public String OriginalFile
        {
            get
            {
                return (String)GetValue(OriginalFileProperty);
            }
            set
            {
                SetValue(OriginalFileProperty, value);
            }
        }
        #endregion OriginalFileName

        #region CurrentOperation
        public String CurrentOperation
        {
            get
            {
                return (String)GetValue(CurrentOperationProperty);
            }
            set
            {
                SetValue(CurrentOperationProperty, value);
            }
        }

        public static readonly DependencyProperty CurrentOperationProperty = DependencyProperty.Register("CurrentOperation", typeof(String), typeof(AudioPlayer), new UIPropertyMetadata(null));
        #endregion CurrentOperation
        #endregion DependencyProperties

        #region TrackIsPlaying
        private bool trackPlayerIsPlaying = false;

        public bool TrackPlayerIsPlaying
        {
            get
            {
                return trackPlayerIsPlaying;
            }
            set
            {
                if (trackPlayerIsPlaying != value)
                    trackPlayerIsPlaying = value;
            }
        }
        #endregion TrackIsPlaying

        #region SliderInUse
        private bool sliderInUse = false;

        public bool SliderInUse
        {
            get
            {
                return sliderInUse;
            }
            set
            {
                if (sliderInUse != value)
                    sliderInUse = value;
            }
        }
        #endregion SliderInUse

        #region DurationSeconds
        private double durationSeconds;
        public double DurationSeconds
        {
            get => durationSeconds;
            set => durationSeconds = value;
        }
        
        #endregion DurationSeconds
        #endregion Properties

        #region Constructors
        public AudioPlayer()
        {
            InitializeComponent();

            DispatcherTimer timer = new(DispatcherPriority.Normal)
            {
                Interval = TimeSpan.FromMilliseconds(10),
            };

            timer.Tick += Timer_Tick;
            timer.Start();

            CurrentOperation = "İşlemlere başlayabilmek için ses dosyası seçiniz";
        }
        #endregion Constructors

        #region Commands
        #region Open
        private void CanOpenExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void OpenExecute(object sender, ExecutedRoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new()
            {
                Filter = "Medya Dosyaları (*.mp4;*.mp3;*.mpg;*.mpeg;*.wav)|*.mp4;*.mp3;*.mpg;*.mpeg;*.wav|Tüm Dosyalar (*.*)|*.*"
            };

            if (openFileDialog.ShowDialog() == true)
            {
                var trackPath = Path.Combine(Constants.ContentPath, Path.GetFileNameWithoutExtension(openFileDialog.FileName) + ".wav");
                MainWindow.trackRecords.Add(Path.GetFileName(openFileDialog.FileName));

                CurrentOperation = "Dosya " + Path.GetExtension(openFileDialog.FileName) + " formatından .wav formatına çevriliyor";
                loadingGif.Visibility = Visibility.Visible;

                Task<string>.Factory.StartNew(() =>
                {
                    TrackManager trackManager = new();
                    var wavFile = trackManager.ConvertToWav(Path.GetDirectoryName(openFileDialog.FileName) ?? "", Path.GetFileName(openFileDialog.FileName) ?? "").Replace("\r\n", string.Empty);

                    return wavFile;
                }).ContinueWith(t =>
                {
                    Dispatcher.Invoke(() =>
                    {
                        mediaPlayer.Source = null; //uygulamayı kapatıp açmadan, yeni ses dosyalarının seçiminin etki göstermesi için null ataması yapılıyor
                        mediaPlayer.Source = new Uri(t.Result);
                        CurrentOperation = "Wav formatına dönüştürme tamamlandı\nAçılan Ses Dosyası: " + t.Result;
                        TrackPath = t.Result;
                        OriginalFile = Path.Join(Path.GetDirectoryName(t.Result), "original.wav");
                        loadingGif.Visibility = Visibility.Collapsed;
                    });
                });
            }
            mediaPlayer.Pause();
        }
        #endregion Open

        #region Play
        private void CanPlayExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = (mediaPlayer != null) && (mediaPlayer.Source != null);
        }

        private void PlayExecute(object sender, ExecutedRoutedEventArgs e)
        {
            mediaPlayer.Play();
            TrackPlayerIsPlaying = true;
        }
        #endregion Play

        #region Pause
        private void CanPauseExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = TrackPlayerIsPlaying;
        }

        private void PauseExecute(object sender, ExecutedRoutedEventArgs e)
        {
            mediaPlayer.Pause();
        }
        #endregion Pause

        #region Stop
        private void CanStopExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = TrackPlayerIsPlaying;
        }

        private void StopExecute(object sender, ExecutedRoutedEventArgs e)
        {
            mediaPlayer.Stop();
            TrackPlayerIsPlaying = false;
        }
        #endregion Stop
        #endregion Commands

        #region Events
        private void Timer_Tick(object? sender, EventArgs e)
        {
            if ((mediaPlayer.Source != null) && (mediaPlayer.NaturalDuration.HasTimeSpan) && (!SliderInUse))
            {
                sliderProgress.Minimum = 0;
                sliderProgress.Maximum = mediaPlayer.NaturalDuration.TimeSpan.TotalSeconds;
                sliderProgress.Value = mediaPlayer.Position.TotalSeconds;
            }
        }

        private void Slider_DragStarted(object sender, DragStartedEventArgs e)
        {
            SliderInUse = true;
        }

        private void Slider_DragEnd(object sender, DragCompletedEventArgs e)
        {
            SliderInUse = false;
            mediaPlayer.Position = TimeSpan.FromSeconds(sliderProgress.Value);
        }

        private void Slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            lblDurationSeconds.Text = TimeSpan.FromSeconds(sliderProgress.Value).TotalSeconds.ToString("#");
            lblProgressStatus.Text = TimeSpan.FromSeconds(sliderProgress.Value).ToString(@"mm\:ss\:ff");
        }

        private void Grid_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            mediaPlayer.Volume += (e.Delta > 0) ? 0.1 : -0.1;
        }
        #endregion Events
    }
}
