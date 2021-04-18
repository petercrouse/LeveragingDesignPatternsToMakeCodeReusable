using Domain.Interfaces;
using Domain.Models;
using Domain.Wrappers;

namespace Domain.Services
{
    class DeploymentSystem : IDeploymentSystem
    {
        public bool IsExecuteCommandRunning(string server)
        {
            throw new System.NotImplementedException();
        }

        public DeploymentProcess StartDeploymentCommand(string commandParameters)
        {
            return DeploymentProcessWrapper.CreateRunningProcess(commandParameters, new DeploymentProcess());          
        }

        public bool WasDeploymentSuccessful(string server)
        {
            return true;
        }

        void IDeploymentSystem.StartDeploymentExecutionMonitoring(string server)
        {
            return;
        }
    }
}
