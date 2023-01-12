using System.Diagnostics;

namespace Managers.PythonBase
{
    public class PythonRunner
    {
        #region Properties
        #region Process
        private Process? p;
        public Process? P
        {
            get => p;
            set => p = value;
        }
        #endregion Process
        #region PythonFile
        private string? pythonFile;
        public string? PythonFile
        {
            get => pythonFile;
            set => pythonFile = value;
        }
        #endregion PythonFile
        #region InterpreterLocation
        private string? interpreterLocation;
        public string? InterpreterLocation
        {
            get => interpreterLocation;
            set => interpreterLocation = value;
        }
        #endregion InterpreterLocation
        #endregion Properties

        #region Constructors
        public PythonRunner(string interpreterLocation, string pythonFile)
        {
            PythonFile = pythonFile;
            InterpreterLocation = interpreterLocation;
            P = new Process();
        }
        #endregion Constructors

        #region Methods
        /// <summary>
        /// Dönüş değerine sahip Python fonksiyonlarını çalıştırır
        /// </summary>
        /// <param name="function">Çalıştırılacak Python fonksiyon ismi</param>
        /// <param name="parameters">Fonksiyon argümanları</param>
        /// <returns>Python Fonksiyon Çıktısı</returns>
        public string Run(string function, params string[] parameters)
        {
            if (P == null)
                throw new Exception("PYTHON PROCESS ENGINE HATASI. ENGINE BAŞLATILAMADI." +
                    "\nINTERPRETER DİZİNİNİ KONTROL EDİNİZ");

            P.StartInfo = new ProcessStartInfo()
            {
                RedirectStandardOutput = true,
                FileName = InterpreterLocation,
                UseShellExecute = false,
                CreateNoWindow = true,
                Arguments = pythonFile + ' ' + function + ' ' + string.Join(' ', parameters)
            };

            P.Start();

            return P.StandardOutput.ReadToEnd();
        }

        /// <summary>
        /// Herhangi bir değer döndürmeyen Python fonksiyonlarını çalıştırmak için
        /// </summary>
        /// <param name="function">Çalıştırılacak Python fonksiyonu</param>
        public void Run(string function)
        {
            if (P == null)
                throw new Exception("PYTHON ENGINE RUN HATASI");

            P.StartInfo = new ProcessStartInfo()
            {
                RedirectStandardOutput = true,
                FileName = InterpreterLocation,
                UseShellExecute = false,
                CreateNoWindow = true,
                Arguments = pythonFile + ' ' + function
            };

            P.Start();
        }

        public static string ConvertToPythonAcceptable(string blockOfText)
        {
            return RemoveNewLineRegex(blockOfText).Replace(" ", "").Replace("\"", "\\\"");
        }

        public static string RemoveNewLineRegex(string str)
        {
            return str.Replace("\r\n", "").Replace("\n", "").Replace("\r", "");
        }
        #endregion Methods
    }
}
