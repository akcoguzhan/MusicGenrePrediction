using Types.Attributes;
using Types.CustomAttributes;

namespace Types;

public class FeatureEntity
{
    #region Properties
    #region Counter
    private string counter;

    [Newtonsoft.Json.JsonIgnore]
    [ColumnName("ID")]
    public string Counter
    {
        get
        {
            return counter;
        }
        set
        {
            if (counter != value)
            {
                counter = value;
            }
        }
    }
    #endregion Counter

    #region TrackName
    private string filename;

    [Newtonsoft.Json.JsonIgnore]
    [Newtonsoft.Json.JsonProperty("filename")]
    [ColumnName("Parça Adı")]
    public string Filename
    {
        get
        {
            return filename;
        }
        set
        {
            if (filename != value)
            {
                filename = value;
            }
        }
    }
    #endregion TrackName

    #region IntervalStart
    private int intervalStart;

    [Newtonsoft.Json.JsonIgnore]
    [ColumnName("Başlangıç Saniyesi")]
    public int IntervalStart
    {
        get
        {
            return intervalStart;
        }
        set
        { 
            if (intervalStart != value)
            {
                intervalStart = value;
            }
        }
    }
    #endregion IntervalStart

    #region IntervalEnd
    private string intervalDuration;

    [Newtonsoft.Json.JsonIgnore]
    [ColumnName("Süre")]
    public string IntervalDuration
    {
        get
        {
            return intervalDuration == "0" ? "Sona Kadar" : intervalDuration;
        }
        set
        {
            if (intervalDuration != value)
            {
                intervalDuration = value;
            }
        }
    }
    #endregion IntervalEnd

    #region Length
    private string length;

    [Newtonsoft.Json.JsonIgnore]
    [Newtonsoft.Json.JsonProperty("length")]
    [ColumnName("Parça Uzunluğu")]
    public string Length
    {
        get
        {
            return length;
        }
        set
        {
            if (length != value)
            {
                length = value;
            }
        }
    }
    #endregion Length

    #region Chroma Stft Mean
    private string chroma_stft_mean;

    [Newtonsoft.Json.JsonProperty("chroma_stft_mean")]
    [FeatureHasPlotAttribute(R: 145, G: 209, B: 209)]
    [ColumnName("Chroma STFT")]
    public string Chroma_stft_mean
    {
        get
        {
            return chroma_stft_mean;
        }
        set
        {
            if (chroma_stft_mean != value)
            {
                chroma_stft_mean = value;
            }
        }
    }
    #endregion Chroma Stft Mean

    #region Rms Mean
    private string rms_mean;

    [Newtonsoft.Json.JsonProperty("rms_mean")]
    [FeatureHasPlotAttribute(R: 145, G: 209, B: 209)]
    [ColumnName("RMS")]
    public string Rms_mean
    {
        get
        {
            return rms_mean;
        }
        set
        {
            if (rms_mean != value)
            {
                rms_mean = value;
            }
        }
    }
    #endregion Rms Mean

    #region Spectral Centroid Mean
    private string spectral_centroid_mean;

    [ColumnVisibility(Vis.Collapsed)]
    [Newtonsoft.Json.JsonIgnore]
    [Newtonsoft.Json.JsonProperty("spectral_centroid_mean")]
    [FeatureHasPlotAttribute(R: 145, G: 209, B: 209)]
    [ColumnName("Spectral Centroid")]
    public string Spectral_centroid_mean
    {
        get
        {
            return spectral_centroid_mean;
        }
        set
        {
            if (spectral_centroid_mean != value)
            {
                spectral_centroid_mean = value;
            }
        }
    }
    #endregion Spectral Centroid Mean

    #region Spectral Bandwidth Mean
    private string spectral_bandwidth_mean;

    [ColumnVisibility(Vis.Collapsed)]
    [Newtonsoft.Json.JsonIgnore]
    [Newtonsoft.Json.JsonProperty("spectral_bandwidth_mean")]
    [FeatureHasPlotAttribute(R: 145, G: 209, B: 209)]
    [ColumnName("Spectral Bandwidth")]
    public string Spectral_bandwidth_mean
    {
        get
        {
            return spectral_bandwidth_mean;
        }
        set
        {
            if (spectral_bandwidth_mean != value)
            {
                spectral_bandwidth_mean = value;
            }
        }
    }
    #endregion Spectral Bandwidth Mean

    #region Rolloff Mean
    private string rolloff_mean;

    [ColumnVisibility(Vis.Collapsed)]
    [Newtonsoft.Json.JsonIgnore]
    [Newtonsoft.Json.JsonProperty("rolloff_mean")]
    [FeatureHasPlotAttribute(R: 145, G: 209, B: 209)]
    [ColumnName("Spectral Rolloff")]
    public string Rolloff_mean
    {
        get
        {
            return rolloff_mean;
        }
        set
        {
            if (rolloff_mean != value)
            {
                rolloff_mean = value;
            }
        }
    }
    #endregion Rolloff Mean

    #region Zero Crossing Rate Mean
    private string zero_crossing_rate_mean;

    [ColumnVisibility(Vis.Collapsed)]
    [Newtonsoft.Json.JsonIgnore]
    [Newtonsoft.Json.JsonProperty("Zero_crossing_rate_mean")]
    [FeatureHasPlotAttribute(R: 145, G: 209, B: 209)]
    [ColumnName("Zero Crossing Rate")]
    public string Zero_crossing_rate_mean
    {
        get
        {
            return zero_crossing_rate_mean;
        }
        set
        {
            if (zero_crossing_rate_mean != value)
            {
                zero_crossing_rate_mean = value;
            }
        }
    }
    #endregion Zero Crossing Rate Mean

    #region Harmony Mean
    private string harmony_mean;

    [Newtonsoft.Json.JsonProperty("harmony_mean")]
    [ColumnName("Harmony")]
    public string Harmony_mean
    {
        get
        {
            return harmony_mean;
        }
        set
        {
            if (harmony_mean != value)
            {
                harmony_mean = value;
            }
        }
    }
    #endregion Harmony Mean

    #region Perceptr Mean
    private string perceptr_mean;

    [ColumnVisibility(Vis.Collapsed)]
    [Newtonsoft.Json.JsonIgnore]
    [Newtonsoft.Json.JsonProperty("perceptr_mean")]
    [ColumnName("Percussive")]
    public string Perceptr_mean
    {
        get
        {
            return perceptr_mean;
        }
        set
        {
            if (perceptr_mean != value)
            {
                perceptr_mean = value;
            }
        }
    }
    #endregion Perceptr Mean

    #region Tempo
    private string tempo;

    [Newtonsoft.Json.JsonProperty("tempo")]
    [ColumnName("Tempo")]
    public string Tempo
    {
        get
        {
            return tempo;
        }
        set
        {
            if (tempo != value)
            {
                tempo = value;
            }
        }
    }
    #endregion Tempo

    #region Mffc1 Mean
    private string mfcc1_mean;

    [Newtonsoft.Json.JsonProperty("mfcc1_mean")]
    [FeatureHasPlotAttribute(R: 145, G: 209, B: 209)]
    [ColumnName("MFCC_1")]
    public string Mfcc1_mean
    {
        get
        {
            return mfcc1_mean;
        }
        set
        {
            if (mfcc1_mean != value)
            {
                mfcc1_mean = value;
            }
        }
    }
    #endregion Mfcc1 Mean

    #region Mfcc2 Mean
    private string mfcc2_mean;

    [Newtonsoft.Json.JsonProperty("mfcc2_mean")]
    [FeatureHasPlotAttribute(R: 145, G: 209, B: 209)]
    [ColumnName("MFCC_2")]
    public string Mfcc2_mean
    {
        get
        {
            return mfcc2_mean;
        }
        set
        {
            if (mfcc2_mean != value)
            {
                mfcc2_mean = value;
            }
        }
    }
    #endregion Mfcc2 Mean

    #region Mfcc3 Mean
    private string mfcc3_mean;

    [Newtonsoft.Json.JsonProperty("mfcc3_mean")]
    [FeatureHasPlotAttribute(R: 145, G: 209, B: 209)]
    [ColumnName("MFCC_3")]
    public string Mfcc3_mean
    {
        get
        {
            return mfcc3_mean;
        }
        set
        {
            if (mfcc3_mean != value)
            {
                mfcc3_mean = value;
            }
        }
    }
    #endregion Mfcc3 Mean

    #region Mfcc4 Mean
    private string mfcc4_mean;

    [Newtonsoft.Json.JsonProperty("mfcc4_mean")]
    [FeatureHasPlotAttribute(R: 145, G: 209, B: 209)]
    [ColumnName("MFCC_4")]
    public string Mfcc4_mean
    {
        get
        {
            return mfcc4_mean;
        }
        set
        {
            if (mfcc4_mean != value)
            {
                mfcc4_mean = value;
            }
        }
    }
    #endregion Mfcc4 Mean

    #region Mfcc5 Mean
    private string mfcc5_mean;

    [Newtonsoft.Json.JsonProperty("mfcc5_mean")]
    [FeatureHasPlotAttribute(R: 145, G: 209, B: 209)]
    [ColumnName("MFCC_5")]
    public string Mfcc5_mean
    {
        get
        {
            return mfcc5_mean;
        }
        set
        {
            if (mfcc5_mean != value)
            {
                mfcc5_mean = value;
            }
        }
    }
    #endregion Mfcc5 Mean

    #region Mfcc6 Mean
    private string mfcc6_mean;

    [Newtonsoft.Json.JsonProperty("mfcc6_mean")]
    [FeatureHasPlotAttribute(R: 145, G: 209, B: 209)]
    [ColumnName("MFCC_6")]
    public string Mfcc6_mean
    {
        get
        {
            return mfcc6_mean;
        }
        set
        {
            if (mfcc6_mean != value)
            {
                mfcc6_mean = value;
            }
        }
    }
    #endregion Mfcc6 Mean

    #region Mfcc7 Mean
    private string mfcc7_mean;

    [Newtonsoft.Json.JsonProperty("mfcc7_mean")]
    [FeatureHasPlotAttribute(R: 145, G: 209, B: 209)]
    [ColumnName("MFCC_7")]
    public string Mfcc7_mean
    {
        get
        {
            return mfcc7_mean;
        }
        set
        {
            if (mfcc7_mean != value)
            {
                mfcc7_mean = value;
            }
        }
    }
    #endregion Mfcc7 Mean

    #region Mfcc8 Mean
    private string mfcc8_mean;

    [Newtonsoft.Json.JsonProperty("mfcc8_mean")]
    [FeatureHasPlotAttribute(R: 145, G: 209, B: 209)]
    [ColumnName("MFCC_8")]
    public string Mfcc8_mean
    {
        get
        {
            return mfcc8_mean;
        }
        set
        {
            if (mfcc8_mean != value)
            {
                mfcc8_mean = value;
            }
        }
    }
    #endregion Mfcc8 Mean

    #region Mfcc9 Mean
    private string mfcc9_mean;

    [Newtonsoft.Json.JsonProperty("mfcc9_mean")]
    [FeatureHasPlotAttribute(R: 145, G: 209, B: 209)]
    [ColumnName("MFCC_9")]
    public string Mfcc9_mean
    {
        get
        {
            return mfcc9_mean;
        }
        set
        {
            if (mfcc9_mean != value)
            {
                mfcc9_mean = value;
            }
        }
    }
    #endregion Mfcc9 Mean

    #region Mfcc10 Mean
    private string mfcc10_mean;

    [Newtonsoft.Json.JsonProperty("mfcc10_mean")]
    [FeatureHasPlotAttribute(R: 145, G: 209, B: 209)]
    [ColumnName("MFCC_10")]
    public string Mfcc10_mean
    {
        get
        {
            return mfcc10_mean;
        }
        set
        {
            if (mfcc10_mean != value)
            {
                mfcc10_mean = value;
            }
        }
    }
    #endregion Mfcc10 Mean

    #region Mfcc11 Mean
    private string mfcc11_mean;

    [Newtonsoft.Json.JsonProperty("mfcc11_mean")]
    [FeatureHasPlotAttribute(R: 145, G: 209, B: 209)]
    [ColumnName("MFCC_11")]
    public string Mfcc11_mean
    {
        get
        {
            return mfcc11_mean;
        }
        set
        {
            if (mfcc11_mean != value)
            {
                mfcc11_mean = value;
            }
        }
    }
    #endregion Mfcc11 Mean

    #region Mfcc12 Mean
    private string mfcc12_mean;

    [Newtonsoft.Json.JsonProperty("mfcc12_mean")]
    [FeatureHasPlotAttribute(R: 145, G: 209, B: 209)]
    [ColumnName("MFCC_12")]
    public string Mfcc12_mean
    {
        get
        {
            return mfcc12_mean;
        }
        set
        {
            if (mfcc12_mean != value)
            {
                mfcc12_mean = value;
            }
        }
    }
    #endregion Mfcc12 Mean

    #region Mfcc13
    private string mfcc13_mean;

    [Newtonsoft.Json.JsonProperty("mfcc13_mean")]
    [FeatureHasPlotAttribute(R: 145, G: 209, B: 209)]
    [ColumnName("MFCC_13")]
    public string Mfcc13_mean
    {
        get
        {
            return mfcc13_mean;
        }
        set
        {
            if (mfcc13_mean != value)
            {
                mfcc13_mean = value;
            }
        }
    }
    #endregion Mfcc13

    #region Mfcc14
    private string mfcc14_mean;

    [Newtonsoft.Json.JsonProperty("mfcc14_mean")]
    [FeatureHasPlotAttribute(R: 145, G: 209, B: 209)]
    [ColumnName("MFCC_14")]
    public string Mfcc14_mean
    {
        get
        {
            return mfcc14_mean;
        }
        set
        {
            if (mfcc14_mean != value)
            {
                mfcc14_mean = value;
            }
        }
    }
    #endregion Mfcc14

    #region Mfcc15
    private string mfcc15_mean;

    [Newtonsoft.Json.JsonProperty("mfcc15_mean")]
    [FeatureHasPlotAttribute(R: 145, G: 209, B: 209)]
    [ColumnName("MFCC_15")]
    public string Mfcc15_mean
    {
        get
        {
            return mfcc15_mean;
        }
        set
        {
            if (mfcc15_mean != value)
            {
                mfcc15_mean = value;
            }
        }
    }
    #endregion Mfcc15

    #region Mfcc16
    private string mfcc16_mean;

    [Newtonsoft.Json.JsonProperty("mfcc16_mean")]
    [FeatureHasPlotAttribute(R: 145, G: 209, B: 209)]
    [ColumnName("MFCC_16")]
    public string Mfcc16_mean
    {
        get
        {
            return mfcc16_mean;
        }
        set
        {
            if (mfcc16_mean != value)
            {
                mfcc16_mean = value;
            }
        }
    }
    #endregion Mfcc16

    #region Mfcc17
    private string mfcc17_mean;

    [Newtonsoft.Json.JsonProperty("mfcc17_mean")]
    [FeatureHasPlotAttribute(R: 145, G: 209, B: 209)]
    [ColumnName("MFCC_17")]
    public string Mfcc17_mean
    {
        get
        {
            return mfcc17_mean;
        }
        set
        {
            if (mfcc17_mean != value)
            {
                mfcc17_mean = value;
            }
        }
    }
    #endregion Mfcc17

    #region Mfcc18
    private string mfcc18_mean;

    [Newtonsoft.Json.JsonProperty("mfcc18_mean")]
    [FeatureHasPlotAttribute(R: 145, G: 209, B: 209)]
    [ColumnName("MFCC_18")]
    public string Mfcc18_mean
    {
        get
        {
            return mfcc18_mean;
        }
        set
        {
            if (mfcc18_mean != value)
            {
                mfcc18_mean = value;
            }
        }
    }
    #endregion Mfcc18

    #region Mfcc19
    private string mfcc19_mean;

    [Newtonsoft.Json.JsonProperty("mfcc19_mean")]
    [FeatureHasPlotAttribute(R: 145, G: 209, B: 209)]
    [ColumnName("MFCC_19")]
    public string Mfcc19_mean
    {
        get
        {
            return mfcc19_mean;
        }
        set
        {
            if (mfcc19_mean != value)
            {
                mfcc19_mean = value;
            }
        }
    }
    #endregion Mfcc19

    #region Mfcc20
    private string mfcc20_mean;

    [Newtonsoft.Json.JsonProperty("mfcc20_mean")]
    [FeatureHasPlotAttribute(R: 145, G: 209, B: 209)]
    [ColumnName("MFCC_20")]
    public string Mfcc20_mean
    {
        get
        {
            return mfcc20_mean;
        }
        set
        {
            if (mfcc20_mean != value)
            {
                mfcc20_mean = value;
            }
        }
    }
    #endregion Mfcc20

    #region HasFigures
    [Newtonsoft.Json.JsonIgnore]
    public bool HasFigures { get; set; }
    #endregion HasFigures
    #endregion Properties

    #region Constructors
    public FeatureEntity(string counter, string filename, string length, string chroma_stft_mean, string rms_mean, string spectral_centroid_mean, string spectral_bandwidth_mean, string rolloff_mean, string zero_crossing_rate_mean, string harmony_mean, string perceptr_mean, string tempo, string mfcc1_mean, string mfcc2_mean, string mfcc3_mean, string mfcc4_mean, string mfcc5_mean, string mfcc6_mean, string mfcc7_mean, string mfcc8_mean, string mfcc9_mean, string mfcc10_mean, string mfcc11_mean, string mfcc12_mean, string mfcc13_mean, string mfcc14_mean, string mfcc15_mean, string mfcc16_mean, string mfcc17_mean, string mfcc18_mean, string mfcc19_mean, string mfcc20_mean)
    {
        this.counter = counter;
        this.filename = filename;
        this.length = length;
        this.chroma_stft_mean = chroma_stft_mean;
        this.rms_mean = rms_mean;
        this.spectral_centroid_mean = spectral_centroid_mean;
        this.spectral_bandwidth_mean = spectral_bandwidth_mean;
        this.rolloff_mean = rolloff_mean;
        this.zero_crossing_rate_mean = zero_crossing_rate_mean;
        this.harmony_mean = harmony_mean;
        this.perceptr_mean = perceptr_mean;
        this.tempo = tempo;
        this.mfcc1_mean = mfcc1_mean;
        this.mfcc2_mean = mfcc2_mean;
        this.mfcc3_mean = mfcc3_mean;
        this.mfcc4_mean = mfcc4_mean;
        this.mfcc5_mean = mfcc5_mean;
        this.mfcc6_mean = mfcc6_mean;
        this.mfcc7_mean = mfcc7_mean;
        this.mfcc8_mean = mfcc8_mean;
        this.mfcc9_mean = mfcc9_mean;
        this.mfcc10_mean = mfcc10_mean;
        this.mfcc11_mean = mfcc11_mean;
        this.mfcc12_mean = mfcc12_mean;
        this.mfcc13_mean = mfcc13_mean;
        this.mfcc14_mean = mfcc14_mean;
        this.mfcc15_mean = mfcc15_mean;
        this.mfcc16_mean = mfcc16_mean;
        this.mfcc17_mean = mfcc17_mean;
        this.mfcc18_mean = mfcc18_mean;
        this.mfcc19_mean = mfcc19_mean;
        this.mfcc20_mean = mfcc20_mean;
    }
    #endregion Constructors
}