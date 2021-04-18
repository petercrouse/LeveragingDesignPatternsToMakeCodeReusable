using App.Common.Abstractions.Engines;
using App.Execute.InputModels;
using App.Execute.State;
using App.Execute.StrategyImplementations;
using Domain.Interfaces;
using System;
using System.Threading;

namespace App.Execute.Engine
{
    public class ExecuteEngine : IEngine
    {
        private readonly IDeploymentSystem _deploymentSystem;
        private ExecuteCommandParameters _inputParams;
        private ExecuteStrategyImplementationsFactory _strategyImplementationsFactory;

        public ExecuteEngine(IDeploymentSystem deploymentSystem)
        {
            _deploymentSystem = deploymentSystem;
        }

        public void Initialise(string[] arguments)
        {
            _inputParams = new ExecuteCommandParameters
            {
                Package = arguments[1],
                Server = arguments[2],
                InstallScript = arguments[3],
                Environment = arguments[4],
                SystemName = arguments[5],
                ScriptParameters = (arguments.Length > 6) ? arguments[6] : null,
            };

            _strategyImplementationsFactory = new ExecuteStrategyImplementationsFactory(_deploymentSystem);
        }

        public void Run()
        {
            var stateModel = new ExecuteStateModel(_deploymentSystem, _strategyImplementationsFactory, retryAttempts: 2, timeToCheckDeployment: 60000, _inputParams);

            stateModel.ChangeState();

            while (!stateModel.DeploymentComplete)
            {
                Console.WriteLine("Still Executing Execute Command...");

                Thread.Sleep(10000);
            }

            Environment.Exit(stateModel.DeploymentSuccess ? 0 : 1);
        }
    }
}
