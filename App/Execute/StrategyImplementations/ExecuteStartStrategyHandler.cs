using App.Common.Abstractions.State;
using App.Common.Abstractions.StrategyAbstractions;
using App.Common.Exceptions;
using App.Execute.InputModels;
using App.Execute.State;
using Domain.Interfaces;
using Domain.Models;

namespace App.Execute.StrategyImplementations
{

    public class ExecuteStartStrategyHandler : StartStrategyHandler<ExecuteCommandParameters>
    {
        private readonly IDeploymentSystem _deploymentSystem;

        public ExecuteStartStrategyHandler(IDeploymentSystem deploymentSystem)
        {
            _deploymentSystem = deploymentSystem;
        }

        public override DeploymentProcess RequestDeploymentProcesses(StateModel<ExecuteCommandParameters> stateModel)
        {
            if (!(stateModel is ExecuteStateModel model))
            {
                throw new InvalidStateModelException(nameof(ExecuteStateModel));
            }

            _deploymentSystem.StartDeploymentExecutionMonitoring(model.ExecuteParameters.Server);

            return _deploymentSystem.StartDeploymentCommand(model.ExecuteParameters.GetDeploymentCommandParameters());
        }
    }
}
