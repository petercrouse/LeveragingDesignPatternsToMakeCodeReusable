using App.Common.Abstractions.DeploymentCommandParams;

namespace App.Execute.InputModels
{
    public class ExecuteCommandParameters : ICommandParams
    {
        public string Package { get; set; }
        public string Server { get; set; }
        public string InstallScript { get; set; }
        public string ScriptParameters { get; set; }
        public string Environment { get; set; }
        public string SystemName { get; internal set; }

        public string GetDeploymentCommandParameters()
        {
            return $"--execute --package {Package} --machine {Server} --script {InstallScript} --parameters \"{ScriptParameters}\" --environment {Environment}";              
        }
    }
}
