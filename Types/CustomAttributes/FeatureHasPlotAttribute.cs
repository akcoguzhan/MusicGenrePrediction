namespace Types.Attributes;

[AttributeUsage(AttributeTargets.Property)]
public class FeatureHasPlotAttribute : Attribute
{
    #region Properties
    private byte r;
    private byte g;
    private byte b;

    public byte R { get => r; set => r = value; }
    public byte G { get => g; set => g = value; }
    public byte B { get => b; set => b = value; }
    #endregion Properties

    #region Constructors
    public FeatureHasPlotAttribute(byte R, byte G, byte B)
    {
        this.R = R;
        this.G = G;
        this.B = B;
    }
    #endregion Constructors
}
