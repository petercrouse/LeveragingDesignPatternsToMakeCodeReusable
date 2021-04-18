using App.Common.Abstractions.DeploymentCommandParams;
using App.Common.Abstractions.Factory;
using App.Common.Abstractions.State;
using Domain.Interfaces;
using System;

namespace App.DeploymentStrategies
{
    public abstract class DeploymentStrategy<TCommandParams>
        where TCommandParams : ICommandParams
    {
        public DeploymentStrategy(IDeploymentSystem system, 
            AbstractStrategyImplementationFactory<TCommandParams> factory, 
            StateModel<TCommandParams> stateModel)
        {
            System = system;
            StateModel = stateModel;
            Factory = factory;
        }

        protected IDeploymentSystem System { get; }
        protected StateModel<TCommandParams> StateModel { get; set; }
        protected AbstractStrategyImplementationFactory<TCommandParams> Factory { get; }

        public void Execute() 
        {
            try
            {
                ExecuteInternal();
            }
            catch(Exception ex)
            {
                Console.WriteLine($"Something went wrong: {ex}");
            }
        }

        protected abstract void ExecuteInternal();
    }
}
