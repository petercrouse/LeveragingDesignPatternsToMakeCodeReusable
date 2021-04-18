using App.Common.Abstractions.Factory;
using App.Common.Abstractions.State;
using App.Package.InputModels;
using Domain.Interfaces;

namespace App.Package.State
{
    public class PackageStateModel : StateModel<PackageCommandParameters>
    {
        public PackageStateModel(IDeploymentSystem system, 
            AbstractStrategyImplementationFactory<PackageCommandParameters> factory, 
            int retryAttempts, 
            long timeToCheckDeployment,
            PackageCommandParameters packageCommandParameters)
            : base(system, factory, retryAttempts, timeToCheckDeployment)
        {
            PackageParameters = packageCommandParameters;
        }

        public PackageCommandParameters PackageParameters { get; set; }
    }
}
