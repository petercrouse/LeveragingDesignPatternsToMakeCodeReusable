using App.Common.Abstractions.DeploymentCommandParams;
using App.Common.Abstractions.Factory;
using App.Common.Abstractions.State;
using App.Common.Abstractions.StrategyAbstractions;
using Domain.Interfaces;

namespace App.DeploymentStrategies
{
    public class CompleteStrategy<TCommandParams> : DeploymentStrategy<TCommandParams>
        where TCommandParams : ICommandParams
    {
        private readonly CompleteStrategyHandler<TCommandParams> _handler;

        public CompleteStrategy(IDeploymentSystem system, 
            AbstractStrategyImplementationFactory<TCommandParams> factory, 
            StateModel<TCommandParams> stateModel)
            : base(system, factory, stateModel)
        {
            _handler = factory.CreateCompleteStrategyHandler();
        }

        protected override void ExecuteInternal()
        {
            StateModel.DeploymentSuccess = _handler.FinalizeDeployment(StateModel, StateModel.RunningProcess.Success);

            StateModel.DeploymentComplete = true;
        }
    }
}
