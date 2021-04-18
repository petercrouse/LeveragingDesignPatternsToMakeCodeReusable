using App.Common.Abstractions.State;
using App.Common.Abstractions.StrategyAbstractions;
using App.Common.Exceptions;
using App.Package.InputModels;
using App.Package.State;
using Domain.Interfaces;
using Domain.Models;

namespace App.Package.StrategyImplementations
{
    internal class PackageStartStrategyHandler : StartStrategyHandler<PackageCommandParameters>
    {
        private IDeploymentSystem _deploymentSystem;

        public PackageStartStrategyHandler(IDeploymentSystem deploymentSystem)
        {
            _deploymentSystem = deploymentSystem;
        }

        public override DeploymentProcess RequestDeploymentProcesses(StateModel<PackageCommandParameters> stateModel)
        {
            if (!(stateModel is PackageStateModel model))
            {
                throw new InvalidStateModelException(nameof(PackageStateModel));
            }

            return _deploymentSystem.StartDeploymentCommand(model.PackageParameters.GetDeploymentCommandParameters());
        }
    }
}