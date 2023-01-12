namespace Managers.Interfaces
{
    public interface IPythonManager
    {
        public string Run(string function, params string[] parameters);

        public void Run(string function);
    }
}
