using App.Common.Abstractions.DeploymentCommandParams;
using App.Common.Abstractions.Factory;
using App.Common.Abstractions.State;
using App.Common.Abstractions.StrategyAbstractions;
using Domain.Interfaces;

namespace App.DeploymentStrategies
{
    public class StartStrategy<TCommandParams> : DeploymentStrategy<TCommandParams>
        where TCommandParams : ICommandParams
    {
        private readonly StartStrategyHandler<TCommandParams> _handler;

        public StartStrategy(IDeploymentSystem system, 
            AbstractStrategyImplementationFactory<TCommandParams> factory, 
            StateModel<TCommandParams> stateModel)
            : base(system, factory, stateModel)
        {
            _handler = factory.CreateStartStrategyHandler();
        }

        protected override void ExecuteInternal()
        {
            var deploymentProcess = _handler.RequestDeploymentProcesses(StateModel);
            deploymentProcess.AttachChangeStateDelegate(StateModel.ChangeState);
            StateModel.RunningProcess = deploymentProcess;

            StateModel.CheckHealthStatusTimer.Start();
            StateModel.CheckHealthStatus = false;
            StateModel.Start = false;
        }
    }
}
