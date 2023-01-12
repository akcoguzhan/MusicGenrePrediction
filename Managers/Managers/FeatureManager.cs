using FileLockInfo;
using JSONParser;
using Managers.PythonBase;
using System.Windows;
using Types;

namespace Managers.Managers;

public class FeatureManager : ManagerBase
{
    #region Constructors
    public FeatureManager() : base(ScriptPaths.FeatureManagerScript)
    {
    }
    #endregion Constructors

    #region Methods
    public List<FeatureEntity>? ExtractFeatures(string? absoluteTrackPath, bool createFigures, int counter, bool clearFigureDirectory, string originalTrackName, int cropStart, int duration)
    {
        if (clearFigureDirectory)
        {
            if (Directory.Exists(Path.Join(Constants.AppDataPath, Constants.FigurePath)))
            {
                foreach (var item in Directory.GetFiles(Path.Join(Constants.AppDataPath, Constants.FigurePath)))
                {
                    var processes = ProcessManager.GetProcessesLockingFile(item);
                    if (processes is not null && processes.Count > 0)
                    {
                        MessageBox.Show("Açık olan figürleri kapatınız. Figürler başka bir process tarafından kullanılıyor olabilir.");
                        return null;
                    }
                }
                Directory.Delete(Path.Join(Constants.AppDataPath, Constants.FigurePath), true);
                Directory.CreateDirectory(Path.Join(Constants.AppDataPath, Constants.FigurePath));
            }
            else
            {
                Directory.CreateDirectory(Path.Join(Constants.AppDataPath, Constants.FigurePath));
            }
        }

        if (absoluteTrackPath is null)
        {
            MessageBox.Show("Özniteliklerin çıkarılacağı parça bulunamadı. Öznitelikleri çıkarılacak parça dizini doğru değil veya eksik");
            return null;
        }

        string trackName = Path.GetFileName(absoluteTrackPath);
        string trackPath = absoluteTrackPath.Substring(0, absoluteTrackPath.Length - trackName.Length);
        string engineResponse = Run("extract_features", trackPath, trackName, createFigures == true ? "True" : "False", counter.ToString());

        if (engineResponse is null)
        {
            MessageBox.Show("Öznitelikler çıkarılamadı.");
            return null;
        }

        Parser<FeatureEntity> featureParser = new();
        var response = featureParser.DeserializeFromString(engineResponse);

        if (response is null)
        {
            MessageBox.Show("Elde edilen öznitelik listesi JSON formatına çevrilemedi.");
            return null;
        }
        response[0].HasFigures = createFigures;
        response[0].Filename = originalTrackName;
        response[0].IntervalStart = cropStart;
        response[0].IntervalDuration = duration.ToString();
        return response;
    }
    #endregion Methods
}
