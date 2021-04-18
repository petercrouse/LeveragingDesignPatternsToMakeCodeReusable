using App.Common.Abstractions.State;
using App.Common.Abstractions.StrategyAbstractions;
using App.Common.Exceptions;
using App.Execute.InputModels;
using App.Execute.State;
using Domain.Interfaces;

namespace App.Execute.StrategyImplementations
{
    public class ExecuteCompleteStrategyHandler : CompleteStrategyHandler<ExecuteCommandParameters>
    {
        private readonly IDeploymentSystem _deploymentSystem;

        public ExecuteCompleteStrategyHandler(IDeploymentSystem deploymentSystem)
        {
            _deploymentSystem = deploymentSystem;
        }

        public override bool FinalizeDeployment(StateModel<ExecuteCommandParameters> stateModel, bool success)
        {
            if (!(stateModel is ExecuteStateModel model))
            {
                throw new InvalidStateModelException(nameof(ExecuteStateModel));
            }

            return _deploymentSystem.WasDeploymentSuccessful(model.ExecuteParameters.Server);
        }
    }
}
