using System.Reflection;

namespace Managers.PythonBase;
public static class ScriptPaths
{
    #region Paths
    public static readonly string TrackManagerScript = PathCombine(@"PythonBase\PythonScripts\", "trackmanager.py");
    public static readonly string FeatureManagerScript = PathCombine(@"PythonBase\PythonScripts\", "featuremanager.py");
    public static readonly string PredictManagerScript = PathCombine(@"PythonBase\PythonScripts\", "predictmanager.py");
    public static readonly string ResourceBase = @"PythonBase\PythonScripts\Resources\";
    #endregion

    #region Methods
    private static string PathCombine(string str1, string str2)
    {
        var assemblyLocation = Assembly.GetExecutingAssembly().Location;
        if (assemblyLocation is null)
        {
            throw new Exception("Assembly konumu okunamadı");
        }

        var assemblyDirectoryName = Path.GetDirectoryName(assemblyLocation);
        if (assemblyDirectoryName is null)
        {
            throw new Exception("Assembly dizini okunamadı");
        }

        return Path.Combine(assemblyDirectoryName, str1, str2);
    }
    #endregion Methods
}
