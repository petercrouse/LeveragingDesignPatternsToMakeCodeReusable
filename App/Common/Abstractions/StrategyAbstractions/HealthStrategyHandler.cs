using App.Common.Abstractions.DeploymentCommandParams;
using App.Common.Abstractions.State;

namespace App.Common.Abstractions.StrategyAbstractions
{
    public class HealthStrategyHandler<TCommandParams>
        where TCommandParams : ICommandParams
    {
        public virtual bool IsDeploymentHealthy(StateModel<TCommandParams> stateModel)
        {
            return true;
        }
    }
}
