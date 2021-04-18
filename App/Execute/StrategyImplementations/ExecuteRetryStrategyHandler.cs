using App.Common.Abstractions.State;
using App.Common.Abstractions.StrategyAbstractions;
using App.Common.Exceptions;
using App.Execute.InputModels;
using App.Execute.State;
using Domain.Interfaces;

namespace App.Execute.StrategyImplementations
{
    public class ExecuteRetryStrategyHandler : RetryStrategyHandler<ExecuteCommandParameters>
    {
        private readonly IDeploymentSystem _deploymentSystem;

        public ExecuteRetryStrategyHandler(IDeploymentSystem deploymentSystem)
        {
            _deploymentSystem = deploymentSystem;
        }

        public override bool CanPerformRetry(StateModel<ExecuteCommandParameters> stateModel)
        {
            if (!(stateModel is ExecuteStateModel model))
            {
                throw new InvalidStateModelException(nameof(ExecuteStateModel));
            }

            bool successfulDeployment = _deploymentSystem.WasDeploymentSuccessful(model.ExecuteParameters.Server);

            return !successfulDeployment;
        }
    }
}
