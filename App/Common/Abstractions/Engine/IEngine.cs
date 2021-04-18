namespace App.Common.Abstractions.Engines
{
    public interface IEngine
    {
        void Initialise(string[] arguments);
        void Run();
    }
}
