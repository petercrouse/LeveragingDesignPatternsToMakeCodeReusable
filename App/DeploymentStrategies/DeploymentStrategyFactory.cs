using App.Common.Enumerations;
using App.Common.Abstractions.DeploymentCommandParams;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using App.Common.Abstractions.Factory;
using App.Common.Abstractions.State;

namespace App.DeploymentStrategies
{
public static class DeploymentStrategyFactory<TCommandParams>
    where TCommandParams : ICommandParams
{
    private static readonly Dictionary<DeploymentState, Func<IDeploymentSystem, AbstractStrategyImplementationFactory<TCommandParams>, 
        StateModel<TCommandParams>, DeploymentStrategy<TCommandParams>>> _executionStrategies =
        new Dictionary<DeploymentState, Func<IDeploymentSystem, AbstractStrategyImplementationFactory<TCommandParams>, 
            StateModel<TCommandParams>, DeploymentStrategy<TCommandParams>>>()
    {
        { DeploymentState.Complete, (system, factory, stateModel) => new CompleteStrategy<TCommandParams>(system, factory, stateModel) },
        { DeploymentState.Start, (system, factory, stateModel) => new StartStrategy<TCommandParams>(system, factory, stateModel) },
        { DeploymentState.Retry, (system, factory, stateModel) => new RetryStrategy<TCommandParams>(system, factory, stateModel) },
        { DeploymentState.CheckStaleExecution, (system, factory, stateModel) => new HealthStrategy<TCommandParams>(system, factory, stateModel) }
    };

    public static DeploymentStrategy<TCommandParams> Create(IDeploymentSystem system, 
        AbstractStrategyImplementationFactory<TCommandParams> factory, 
        StateModel<TCommandParams> stateModel)
    {
        if (stateModel.Start)
        {
            return _executionStrategies[DeploymentState.Start](system, factory, stateModel);
        }
        else if (!stateModel.RunningProcess.Complete && stateModel.CheckHealthStatus)
        {
            return _executionStrategies[DeploymentState.CheckStaleExecution](system, factory, stateModel);
        }
        else if (stateModel.RetryAttempts != 0 && stateModel.RunningProcess.Complete && !stateModel.RunningProcess.Success)
        {
            return _executionStrategies[DeploymentState.Retry](system, factory, stateModel);
        }
        else
        {
            return _executionStrategies[DeploymentState.Complete](system, factory, stateModel);
        }
    }
}
}
