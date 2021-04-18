using App.Common.Abstractions.DeploymentCommandParams;
using App.Common.Abstractions.StrategyAbstractions;
using Domain.Interfaces;

namespace App.Common.Abstractions.Factory
{
    public abstract class AbstractStrategyImplementationFactory<TCommandParams>
        where TCommandParams : ICommandParams
    {
        public IDeploymentSystem System { get; }

        protected AbstractStrategyImplementationFactory(IDeploymentSystem system)
        {
            System = system;
        }

        public abstract StartStrategyHandler<TCommandParams> CreateStartStrategyHandler();

        public virtual HealthStrategyHandler<TCommandParams> CreateHealthStrategyHandler()
        {
            return new HealthStrategyHandler<TCommandParams>();
        }

        public virtual RetryStrategyHandler<TCommandParams> CreateRetryStrategyHandler()
        {
            return new RetryStrategyHandler<TCommandParams>();
        }

        public virtual CompleteStrategyHandler<TCommandParams> CreateCompleteStrategyHandler()
        {
            return new CompleteStrategyHandler<TCommandParams>();
        }
    }
}
