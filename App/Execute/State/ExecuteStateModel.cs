using App.Common.Abstractions.Factory;
using App.Common.Abstractions.State;
using App.Execute.InputModels;
using Domain.Interfaces;

namespace App.Execute.State
{
    public class ExecuteStateModel: StateModel<ExecuteCommandParameters>
    {
        public ExecuteStateModel(IDeploymentSystem system, 
            AbstractStrategyImplementationFactory<ExecuteCommandParameters> factory, 
            int retryAttempts, 
            long timeToCheckDeployment,
            ExecuteCommandParameters executeCommandParameters) 
            : base(system, factory, retryAttempts, timeToCheckDeployment)
        {
            ExecuteParameters = executeCommandParameters;
        }

        public ExecuteCommandParameters ExecuteParameters { get; set; }
    }
}
