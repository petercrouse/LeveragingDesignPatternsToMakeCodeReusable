using App.Common.Abstractions.DeploymentCommandParams;
using App.Common.Abstractions.State;

namespace App.Common.Abstractions.StrategyAbstractions
{
    public class RetryStrategyHandler<TCommandParams>
        where TCommandParams : ICommandParams
    {
        public virtual bool CanPerformRetry(StateModel<TCommandParams> stateModel)
        {
            return true;
        }
    }
}
