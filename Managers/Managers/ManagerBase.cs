using Managers.Interfaces;
using Managers.PythonBase;
using System.Diagnostics;
using System.Text;
using System.Windows;

namespace Managers.Managers;
public class ManagerBase : IPythonManager
{
    #region Properties
    #region PythonFile
    private string? pythonFile;

    public string? PythonFile
    {
        get
        {
            return pythonFile;
        }
        set
        {
            if (pythonFile != value)
            {
                pythonFile = value;
            }
        }
    }
    #endregion PythonFile

    #region PythonRunner
    private PythonRunner? runner;

    public PythonRunner? Runner
    {
        get
        {
            return runner;
        }
        set
        {
            if (runner != value)
            {
                runner = value;
            }
        }
    }
    #endregion PythonRunner
    #endregion Properties

    #region Constructors
    public ManagerBase() { }

    public ManagerBase(string? pythonFile)
    {
        PythonFile = pythonFile;

        if (PythonFile is null)
        {
            MessageBox.Show("Çalıştırılacak Python dosyası belirtilmemiş");
            throw new Exception("Çalıştırılacak Python dosyası belirtilmemiş");
        }

        #region Getting Python Interpreter Location
        Process process = new();
        process.StartInfo.FileName = "cmd.exe";
        process.StartInfo.Arguments = @"/C python -c ""import sys; print(sys.executable)""";
        process.StartInfo.CreateNoWindow = true;
        process.StartInfo.RedirectStandardError = true;
        process.StartInfo.RedirectStandardOutput = true;
        process.StartInfo.RedirectStandardInput = false;
        process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
        process.StartInfo.UseShellExecute = false;

        var stdOutput = new StringBuilder();
        process.OutputDataReceived += (sender, args) => stdOutput.AppendLine(args.Data);

        string stdError = null;
        try
        {
            process.Start();
            process.BeginOutputReadLine();
            stdError = process.StandardError.ReadToEnd();
            process.WaitForExit();
        }
        catch (Exception e)
        {
            throw new Exception("OS error while executing ");
        }
        #endregion Getting Python Interpreter Location
        string python39Path = PythonRunner.RemoveNewLineRegex(stdOutput.ToString());

        Runner = new PythonRunner(new Uri(python39Path).AbsolutePath, PythonFile);
    }
    #endregion Constructors

    #region Methods
    /// <summary>
    /// Dönüş değerine sahip Python fonksiyonlarını çalıştırır
    /// </summary>
    /// <param name="function">Çalıştırılacak Python fonksiyon ismi</param>
    /// <param name="parameters">Fonksiyon argümanları</param>
    /// <returns>Python Fonksiyon Çıktısı</returns>
    public string Run(string? function, params string[] parameters)
    {
        if (Runner is null)
        {
            MessageBox.Show("Python Script Runner Engine Hatası");
            throw new Exception("Python Script Runner Engine Hatası");
        }
        if (function is null)
        {
            MessageBox.Show("Çalıştırılacak Python fonksiyonu belirtilmemiş");
            throw new Exception("Çalıştırılacak Python fonksiyonu belirtilmemiş");
        }

        //dönüş değeri olan parametresiz bir fonksiyon olabilir
        parameters ??= Array.Empty<string>();

        return Runner.Run(function, parameters);
    }

    /// <summary>
    /// Herhangi bir değer döndürmeyen Python fonksiyonlarını çalıştırmak için
    /// </summary>
    /// <param name="function">Çalıştırılacak Python fonksiyonu</param>
    public void Run(string function)
    {
        if (Runner is null)
        {
            MessageBox.Show("Python Script Runner Engine Hatatası");
            throw new Exception("Python Script Runner Engine Hatatası");
        }
        if (function is null)
        {
            MessageBox.Show("Çalıştırılacak Python fonksiyonu belirtilmemiş");
            throw new Exception("Çalıştırılacak Python fonksiyonu belirtilmemiş");
        }

        Runner.Run(function);
    }
    #endregion Methods
}