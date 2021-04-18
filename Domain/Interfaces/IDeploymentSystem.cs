using Domain.Models;

namespace Domain.Interfaces
{
    public interface IDeploymentSystem
    {
        DeploymentProcess StartDeploymentCommand(string commandParameters);
        void StartDeploymentExecutionMonitoring(string server);
        bool WasDeploymentSuccessful(string server);
        bool IsExecuteCommandRunning(string server);
    }
}
