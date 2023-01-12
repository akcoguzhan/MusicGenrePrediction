using FileLockInfo;
using System.IO;
using System.Windows;
using Types;

namespace MainApplication
{
    public partial class App : Application
    {
        public App()
        {
            if (!Directory.Exists(Path.Combine(Constants.AppDataPath, Constants.ContentPath)))
            {
                Directory.CreateDirectory(Path.Combine(Constants.AppDataPath, Constants.ContentPath));
            }


            if (Directory.Exists(Path.Join(Constants.AppDataPath, Constants.DistributionPath)))
            {
                Directory.Delete(Path.Join(Constants.AppDataPath, Constants.DistributionPath), true);
                Directory.CreateDirectory(Path.Join(Constants.AppDataPath, Constants.DistributionPath));
            }
            else
            {
                Directory.CreateDirectory(Path.Join(Constants.AppDataPath, Constants.DistributionPath));
            }
        }
    }
}
