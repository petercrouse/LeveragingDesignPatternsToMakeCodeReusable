using App.Common.Abstractions.DeploymentCommandParams;
using App.Common.Abstractions.Factory;
using App.DeploymentStrategies;
using Domain.Interfaces;
using Domain.Models;
using System.Timers;

namespace App.Common.Abstractions.State
{
    public abstract class StateModel<TCommandParams>
        where TCommandParams : ICommandParams
    {
        private readonly IDeploymentSystem _system;
        private readonly AbstractStrategyImplementationFactory<TCommandParams> _factory;

        protected StateModel(IDeploymentSystem system, 
            AbstractStrategyImplementationFactory<TCommandParams> factory, 
            int retryAttempts, 
            long timeToCheckDeployment)
        {

            RetryAttempts = retryAttempts;
            TimeToCheckForStartedDeployment = timeToCheckDeployment;
            _system = system;
            _factory = factory;

            Start = true;
            CheckHealthStatus = false;
            CheckHealthStatusTimer = new Timer(TimeToCheckForStartedDeployment);
            CheckHealthStatusTimer.AutoReset = false;

            CheckHealthStatusTimer.Elapsed += (o, s) =>
            {
                CheckHealthStatus = true;
                CheckHealthStatusTimer.Stop();
                ChangeState();
            };
        }

        public bool Start { get; set; }
        public int RetryAttempts { get; set; }
        public long TimeToCheckForStartedDeployment { get; set; }
        public bool CheckHealthStatus { get; set; }
        public DeploymentProcess RunningProcess { get; set; }
        public Timer CheckHealthStatusTimer { get; private set; }

        public bool DeploymentComplete { get; set; }
        public bool DeploymentSuccess { get; internal set; }

        public void ChangeState()
        {
            DeploymentStrategy<TCommandParams> strategy = 
                DeploymentStrategyFactory<TCommandParams>.Create(_system, _factory, this);
            strategy.Execute();
        }
    }
}
