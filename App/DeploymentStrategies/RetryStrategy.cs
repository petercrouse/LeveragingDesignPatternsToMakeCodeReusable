using App.Common.Abstractions.DeploymentCommandParams;
using App.Common.Abstractions.Factory;
using App.Common.Abstractions.State;
using App.Common.Abstractions.StrategyAbstractions;
using Domain.Interfaces;

namespace App.DeploymentStrategies
{
    public class RetryStrategy<TCommandParams> : DeploymentStrategy<TCommandParams>
        where TCommandParams : ICommandParams
    {
        private readonly RetryStrategyHandler<TCommandParams> _handler;

        public RetryStrategy(IDeploymentSystem system, AbstractStrategyImplementationFactory<TCommandParams> factory, StateModel<TCommandParams> stateModel)
            : base(system, factory, stateModel)
        {
            _handler = factory.CreateRetryStrategyHandler();
        }

        protected override void ExecuteInternal()
        {
            if (_handler.CanPerformRetry(StateModel))
            {
                StateModel.Start = true;
                StateModel.RetryAttempts--;

                StateModel.ChangeState();
                return;
            }

            StateModel.RetryAttempts = 0;
            StateModel.ChangeState();
        }
    }
}
