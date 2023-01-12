using Managers.PythonBase;
using System.Windows;

namespace Managers.Managers;

public class TrackManager : ManagerBase
{
    #region Constructors
    public TrackManager() : base(ScriptPaths.TrackManagerScript)
    {
    }
    #endregion Constructors

    #region Methods
    public string ConvertToWav(string trackPath, string trackName)
    {
        if (trackPath is null)
            throw new ArgumentNullException(nameof(trackPath));

        var returnObject = Run("convert_to_wav", trackPath, trackName);
        return returnObject.Replace("\r\n", "").Replace("\r", "").Replace("\n", "");
    }

    public string Crop(string trackPath, string trackName, int? start, int? duration)
    {
        if (trackPath is null || trackName is null || start is null || duration is null)
        {
            MessageBox.Show("Ses bölme işlemi için hatalı argümanlar verildi");
            throw new Exception("Ses bölme işlemi için hatalı argümanlar verildi");
        }

        var returnObject = Run("crop", trackPath, trackName, start.Value.ToString(), duration.Value.ToString());
        return returnObject.Replace("\r\n", "").Replace("\r", "").Replace("\n", "");
    }
    #endregion Methods
}