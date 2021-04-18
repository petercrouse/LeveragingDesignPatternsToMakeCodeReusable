using App.Common.Abstractions.State;
using App.Common.Abstractions.StrategyAbstractions;
using App.Common.Exceptions;
using App.Execute.InputModels;
using App.Execute.State;
using Domain.Interfaces;

namespace App.Execute.StrategyImplementations
{
    public class ExecuteHealthStrategyHandler : HealthStrategyHandler<ExecuteCommandParameters>
    {
        private readonly IDeploymentSystem _deploymentSystem;

        public ExecuteHealthStrategyHandler(IDeploymentSystem deploymentSystem)
        {
            _deploymentSystem = deploymentSystem;
        }

        public override bool IsDeploymentHealthy(StateModel<ExecuteCommandParameters> stateModel)
        {
            if (!(stateModel is ExecuteStateModel model))
            {
                throw new InvalidStateModelException(nameof(ExecuteStateModel));
            }

            return _deploymentSystem.IsExecuteCommandRunning(model.ExecuteParameters.Server);
        }
    }
}
