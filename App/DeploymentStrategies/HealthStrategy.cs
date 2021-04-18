using App.Common.Abstractions.DeploymentCommandParams;
using App.Common.Abstractions.Factory;
using App.Common.Abstractions.State;
using App.Common.Abstractions.StrategyAbstractions;
using Domain.Interfaces;

namespace App.DeploymentStrategies
{
    public class HealthStrategy<TCommandParams> : DeploymentStrategy<TCommandParams>
        where TCommandParams : ICommandParams
    {
        private readonly HealthStrategyHandler<TCommandParams> _handler;

        public HealthStrategy(IDeploymentSystem system, AbstractStrategyImplementationFactory<TCommandParams> factory, StateModel<TCommandParams> stateModel)
            : base(system, factory, stateModel)
        {
            _handler = factory.CreateHealthStrategyHandler();
        }

        protected override void ExecuteInternal()
        {
            bool healthyDeployment = _handler.IsDeploymentHealthy(StateModel);

            if (!healthyDeployment)
            {
                StateModel.RunningProcess.Complete = true;
                StateModel.RunningProcess.Success = false;
                StateModel.RunningProcess.Process.Close();
                StateModel.RunningProcess.Process.Dispose();
                StateModel.CheckHealthStatus = false;
                StateModel.ChangeState();
                return;
            }

            StateModel.CheckHealthStatus = false;
        }
    }
}
