using App.Common.Enumerations;
using App.Common.Abstractions.Engines;
using App.Execute.Engine;
using Domain.Interfaces;
using System;

namespace App.Common
{
    public class EngineFactory
    {
        private readonly IDeploymentSystem _system;

        public EngineFactory(IDeploymentSystem system)
        {
            _system = system;
        }

        public IEngine CreateEngine(string command)
        {
            if (command.ToLower() == nameof(DeploymentCommand.Execute).ToLower())
            {
                return new ExecuteEngine(_system);
            }
            else
            {
                throw new Exception("Invalid Deployment Command");
            }
        }
    }
}
