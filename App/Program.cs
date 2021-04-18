using App.BootStrap;
using App.Common;
using App.Common.Abstractions.Engines;
using Domain.Interfaces;
using Serilog;
using StructureMap;
using System;

namespace App
{
    class Program
    {
        public Program()
        {
            _deploymentSystem = _container.GetInstance<IDeploymentSystem>();
        }

        static void Main(string[] args)
        {
            _container = Bootstrapper.GetContainer();

            Log.Logger = new LoggerConfiguration()
                .CreateLogger();

            var program = new Program();

            program.Run(args);
        }

        private static Container _container;
        private readonly IDeploymentSystem _deploymentSystem;

        private void Run(string[] args)
        {
            string deploymentCommand = args[0];
            IEngine engine;

            try
            {
                var engineFactory = new EngineFactory(_deploymentSystem);

                engine = engineFactory.CreateEngine(deploymentCommand);
                engine.Initialise(args);
                engine.Run();
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Something went wrong");
                throw ex;
            }
        }
    }
}
