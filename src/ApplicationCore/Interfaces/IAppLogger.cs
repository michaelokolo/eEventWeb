namespace ApplicationCore.Interfaces;

public interface IAppLogger<T>
{
    void LogInFormation(string message, params object[] args);
    void LogWarning(string message, params object[] args);
}
