using App.Common.Abstractions.Engines;
using App.Package.InputModels;
using App.Package.State;
using App.Package.StrategyImplementations;
using Domain.Interfaces;
using System;
using System.Threading;

namespace App.Package.Engine
{
    public class PackageEngine : IEngine
    {
        private readonly IDeploymentSystem _deploymentSystem;
        private PackageCommandParameters _inputParams;
        private PackageStrategyImplementationsFactory _strategyImplementationsFactory;

        public PackageEngine(IDeploymentSystem deploymentSystem)
        {
            _deploymentSystem = deploymentSystem;
        }

        public void Initialise(string[] arguments)
        {
            _inputParams = new PackageCommandParameters
            {
                Feature = arguments[1],
                Subsystem = arguments[2],
                Version = arguments[3],
                Release = arguments[4],
                Source = arguments[5],
                Purge = arguments[6]
            };

            _strategyImplementationsFactory = new PackageStrategyImplementationsFactory(_deploymentSystem);
        }

        public void Run()
        {
            var stateModel = new PackageStateModel(_deploymentSystem, _strategyImplementationsFactory, retryAttempts: 2, timeToCheckDeployment: 60000, _inputParams);

            stateModel.ChangeState();

            while (!stateModel.DeploymentComplete)
            {
                Console.WriteLine("Still Executing Package Command...");

                Thread.Sleep(10000);
            }

            Environment.Exit(stateModel.DeploymentSuccess ? 0 : 1);
        }
    }
}
