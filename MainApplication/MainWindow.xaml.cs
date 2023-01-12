using JSONParser;
using Managers.Managers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Types;
using Types.Attributes;
using Types.CustomAttributes;

namespace MainApplication
{
    public partial class MainWindow : Window
    {
        #region Properties

        public FeatureEntity SelectedCellItem
        {
            get
            {
                return (FeatureEntity)grdFeatureList.SelectedCells[0].Item;
            }
        }

        private bool CanExtractFeatures { get; set; }
        public bool FirstTime { get; set; }
        private int ExtractCounter { get; set; }
        public List<FeatureEntity>? FeatureEntities { get; set; }
        
        public List<PredictionResult> PredictionResults { get; set; }
        public int CropDuration { get; set; }
        public int CropStart { get; set; }
        public bool SaveFigures { get; set; }

        public static List<string> trackRecords = new List<string>();
        #endregion Properties

        #region Constructors
        public MainWindow()
        {
            InitializeComponent();
            FirstTime = true;
            ExtractCounter = 1;
            CanExtractFeatures = true;
        }
        #endregion Constructors

        #region Events
        private void GridPredictResultList_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            PropertyDescriptor? desc = e.PropertyDescriptor as PropertyDescriptor;

            if (desc?.Attributes[typeof(ColumnNameAttribute)] is ColumnNameAttribute att)
            {
                e.Column.Header = att.ColumnName;
            }
        }

        private void GridFeatures_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            PropertyDescriptor? desc = e.PropertyDescriptor as PropertyDescriptor;

            if (desc?.Attributes[typeof(ColumnNameAttribute)] is ColumnNameAttribute att)
            {
                e.Column.Header = att.ColumnName;
            }
            if (desc?.Attributes[typeof(ColumnVisibilityAttribute)] is ColumnVisibilityAttribute att2)
            {
                e.Column.MaxWidth = 0;
                e.Column.Visibility = (Visibility)att2.Visibility;
            }
        }

        private void UpdateTextInputRegex(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void ExtractFeaturesClicked(object sender, RoutedEventArgs e)
        {
            if (CanExtractFeatures)
            {
                if (string.IsNullOrWhiteSpace(uc_AudioPlayer.TrackPath) || string.IsNullOrWhiteSpace(uc_AudioPlayer.OriginalFile))
                {
                    MessageBox.Show("İşlem yapılacak ses dosyası seçilmeden öznitelikler çıkartılamaz", "Öznitelik Çıkarma Hatası", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                if (CropStart != 0 && CropDuration == 0)
                {
                    //MessageBox.Show("Kırpılma uzunluğu verilmeden başlangıç saniyesi verilemez", "Kırpma Hatası", MessageBoxButton.OK, MessageBoxImage.Error);
                    //return;
                }

                string? trackPath = uc_AudioPlayer.TrackPath;
                if (string.IsNullOrWhiteSpace(trackPath))
                {
                    MessageBox.Show("Ses dosyasına erişim sağlanamadı", "Erişim Hatası", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                string originalFile = uc_AudioPlayer.OriginalFile;

                List<FeatureEntity> flgFeatures = new List<FeatureEntity>();

                Task<List<FeatureEntity>>.Factory.StartNew(() =>
                {
                    //kesme yoksa orijinal uzunluktaki dosya kullanılır
                    if (CropDuration == 0 && CropStart == 0)
                        trackPath = originalFile;

                    //kesme süresi verildiyse, dosya kırpılır ve yenisi kullanılır
                    
                    TrackManager trackManager = new();
                    trackPath = trackManager.Crop(trackPath: Path.GetDirectoryName(trackPath),
                                                    trackName: Path.GetFileName(trackPath),
                                                    start: CropStart,
                                                    duration: CropDuration);
                    if (trackPath == "hata")
                    {
                        MessageBox.Show($"{CropStart}. saniyeden başlanarak {CropDuration} saniyelik bir kırpma yapılamaz. Ses dosyasının sınırları aşılıyor.", "Ses kırpma hatası", MessageBoxButton.OK, MessageBoxImage.Error);
                        return null;
                    }

                    Dispatcher.Invoke(() =>
                    {
                        loadingGif.Visibility = Visibility.Visible;
                        CanExtractFeatures = false;
                        if (SaveFigures)
                        {
                            txtCurrentOperation.Text = "Ses Dosyasından Öznitelikler ve Figürler Çıkartılıyor...";
                        }
                        else
                        {
                            txtCurrentOperation.Text = "Ses Dosyasından Öznitelikler Çıkartılıyor...";
                        }
                    });
                    FeatureManager featureManager = new();
                    var features = featureManager.ExtractFeatures(trackPath, SaveFigures, ExtractCounter++, FirstTime, trackRecords.Last(), CropStart, CropDuration);
                    FirstTime = false;

                    return features;
                }).ContinueWith(t =>
                {
                    if (t.Result == null)
                    {
                        Dispatcher.Invoke(() =>
                        {
                            loadingGif.Visibility = Visibility.Collapsed;
                        });
                    }
                    if (t?.Result?.Count > 0)
                    {
                        Dispatcher.Invoke(() =>
                        {
                            loadingGif.Visibility = Visibility.Collapsed;
                            txtCurrentOperation.Text = "Öznitelikler Başarıyla Çıkarıldı";
                            CanExtractFeatures = true;
                            List<FeatureEntity> temp = new();
                            if (grdFeatureList.ItemsSource != null && ((List<FeatureEntity>)grdFeatureList.ItemsSource).Count > 0)
                            {
                                temp.AddRange(((List<FeatureEntity>)grdFeatureList.ItemsSource).ToList());
                            }
                            temp.AddRange(t.Result);
                            grdFeatureList.ItemsSource = temp;
                            grdFeatureList.FormatCellsIfHasFigure();
                            grdFeatureList.Columns[^1].Visibility = Visibility.Collapsed;
                            CropDuration = 0;
                            txtDuration.Text = "0";
                            CropStart = 0;
                            txtStart.Text = "0";
                        });
                    }
                }, TaskScheduler.FromCurrentSynchronizationContext());
            }
            else
            {
                MessageBox.Show("Geçerli işlem sonlanmadan yeni bir işlem başlatılamaz.", "Feature Extraction", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void PredictClicked(object sender, RoutedEventArgs e)
        {
            if (grdFeatureList.SelectedCells is null || grdFeatureList.SelectedCells.Count == 0)
            {
                MessageBox.Show("Tahminde bulunmak için öznitelik listesinden seçim yapınız.", "Uyarı", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            Task.Factory.StartNew(() =>
            {
                Dispatcher.Invoke(() =>
                {
                    loadingGif.Visibility = Visibility.Visible;
                    txtCurrentOperation.Text = $"{SelectedCellItem.Filename} ses parçası için tahmin yapılıyor...";
                });
                Parser<FeatureEntity> parser = new();
                var str = parser.SerializeFromObject(SelectedCellItem);
                PredictManager predictManager = new PredictManager();
                List<PredictionResult> resultResponse = predictManager.Predict(str, int.Parse(SelectedCellItem.Counter), SelectedCellItem.Filename, SelectedCellItem.Counter);
                List<PredictionResult> temp = new();
                if (grdPredictResultList.ItemsSource != null && ((List<PredictionResult>)grdPredictResultList.ItemsSource).Count > 0)
                {
                    temp.AddRange(((List<PredictionResult>)grdPredictResultList.ItemsSource).ToList());
                }
                temp.AddRange(resultResponse);

                Dispatcher.Invoke(() => {
                    grdPredictResultList.ItemsSource = temp;
                });

            }).ContinueWith(t =>
            {
                Dispatcher.Invoke(() =>
                {
                    loadingGif.Visibility = Visibility.Collapsed;
                    txtCurrentOperation.Text = $"Tahmin işlemi sonuçlandı";
                });
            }, TaskScheduler.FromCurrentSynchronizationContext());
        }

        /// <summary>
        /// DataGrid üzerine çift tıklanıldığında, şayet tıklanılan nesne hücre ise ve ilgili özniteliğe (hücreye) ait figür varsa onu açar 
        /// </summary>
        private void GridFeatureList_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (grdFeatureList.SelectedCells != null && grdFeatureList.SelectedCells.Count > 0)
            {
                var x = grdFeatureList.SelectedCells[0].Column.DisplayIndex;
                var featureName = grdFeatureList.Columns[x].Header;
                ShowImage((string)featureName, ((FeatureEntity)grdFeatureList.SelectedCells[0].Item).Counter);
            }
        }


        private void grdPredictResultList_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (grdPredictResultList.SelectedCells != null && grdPredictResultList.SelectedCells.Count > 0)
            {
                var x = grdPredictResultList.SelectedCells[0].Column.DisplayIndex;
                ShowDistribution(((PredictionResult)grdPredictResultList.SelectedCells[0].Item).ID.ToString());
            }
        }
        #endregion Events

        #region Methods
        private void ShowDistribution(string counter)
        {
            var filePath = Path.Join(Constants.AppDataPath, Constants.DistributionPath, counter + ".png");
            if (File.Exists(filePath))
            {
                Process.Start(new ProcessStartInfo
                {
                    FileName = filePath,
                    UseShellExecute = true,
                    WorkingDirectory = Path.Join(Constants.AppDataPath, Constants.FigurePath)
                });
            }
        }


        /// <summary>
        ///Tam dizini verilen png dosyalarını UI process'ini kilitlemeden varsayılan görüntüleyici ile açar
        /// </summary>
        /// <param name="figureName">Açılacak dosyanın ismi</param>
        /// <param name="subFolder">Dosyanın bulunduğu alt dizin</param>
        private static void ShowImage(string figureName, string subFolder)
        {
            if (File.Exists(Path.Join(Constants.AppDataPath, Constants.FigurePath, subFolder, figureName + ".png")))
            {
                Process.Start(new ProcessStartInfo { 
                    FileName = Path.Join(Constants.AppDataPath, Constants.FigurePath, subFolder, figureName + ".png"),
                    UseShellExecute = true,
                    WorkingDirectory = Path.Join(Constants.AppDataPath, Constants.FigurePath) 
                });
            }
        }
        #endregion Methods

        
    }
}
