using App.Common.Abstractions.DeploymentCommandParams;
using App.Common.Abstractions.State;

namespace App.Common.Abstractions.StrategyAbstractions
{
    public class CompleteStrategyHandler<TCommandParams>
        where TCommandParams : ICommandParams
    {
        public virtual bool FinalizeDeployment(StateModel<TCommandParams> stateModel, bool success)
        {
            return success;
        }
    }
}
