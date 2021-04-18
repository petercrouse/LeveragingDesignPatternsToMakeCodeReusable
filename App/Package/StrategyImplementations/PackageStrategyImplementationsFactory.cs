using App.Common.Abstractions.Factory;
using App.Common.Abstractions.StrategyAbstractions;
using App.Package.InputModels;
using Domain.Interfaces;

namespace App.Package.StrategyImplementations
{
    public class PackageStrategyImplementationsFactory : AbstractStrategyImplementationFactory<PackageCommandParameters>
    {
        public PackageStrategyImplementationsFactory(IDeploymentSystem system) : base(system)
        {
        }

        public override StartStrategyHandler<PackageCommandParameters> CreateStartStrategyHandler()
        {
            return new PackageStartStrategyHandler(System);
        }
    }
}
