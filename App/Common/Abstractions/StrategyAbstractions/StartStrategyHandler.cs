using App.Common.Abstractions.DeploymentCommandParams;
using App.Common.Abstractions.State;
using Domain.Models;

namespace App.Common.Abstractions.StrategyAbstractions
{
    public abstract class StartStrategyHandler<TCommandParams>
        where TCommandParams : ICommandParams
    {
        public abstract DeploymentProcess RequestDeploymentProcesses(StateModel<TCommandParams> stateModel);
    }
}
