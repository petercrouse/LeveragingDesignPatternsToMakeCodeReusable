using App.Common.Abstractions.Factory;
using App.Common.Abstractions.StrategyAbstractions;
using App.Execute.InputModels;
using Domain.Interfaces;

namespace App.Execute.StrategyImplementations
{
    public class ExecuteStrategyImplementationsFactory : AbstractStrategyImplementationFactory<ExecuteCommandParameters>
    {
        public ExecuteStrategyImplementationsFactory(IDeploymentSystem system) : base(system)
        {
        }

        public override HealthStrategyHandler<ExecuteCommandParameters> CreateHealthStrategyHandler()
        {
            return new ExecuteHealthStrategyHandler(System);
        }

        public override CompleteStrategyHandler<ExecuteCommandParameters> CreateCompleteStrategyHandler()
        {
            return new ExecuteCompleteStrategyHandler(System);
        }

        public override RetryStrategyHandler<ExecuteCommandParameters> CreateRetryStrategyHandler()
        {
            return new ExecuteRetryStrategyHandler(System);
        }

        public override StartStrategyHandler<ExecuteCommandParameters> CreateStartStrategyHandler()
        {
            return new ExecuteStartStrategyHandler(System);
        }
    }
}
