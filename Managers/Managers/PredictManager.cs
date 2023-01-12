using FileLockInfo;
using Managers.PythonBase;
using System.Windows;
using Types;

namespace Managers.Managers;

public class PredictManager : ManagerBase
{
    #region Constructors
    public PredictManager() : base(ScriptPaths.PredictManagerScript)
    {
    }
    #endregion Constructors

    #region Methods
    public List<PredictionResult> Predict(string jsonString, int id, string name, string counter)
    {
        string replaceWith = PythonRunner.ConvertToPythonAcceptable(jsonString);
        string engineResponse = Run("getpredictionresult", replaceWith, counter);

        return new List<PredictionResult>()
        {
            new PredictionResult(id, engineResponse.Split('/')[3][4..].Replace("\r\n", "").Replace("\r", "").Replace("\n", ""), name),
        };
    }
    #endregion Methods
}
