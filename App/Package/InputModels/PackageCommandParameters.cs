using App.Common.Abstractions.DeploymentCommandParams;

namespace App.Package.InputModels
{
    public class PackageCommandParameters : ICommandParams
    {
        public string Subsystem { get; set; }
        public string Feature { get; set; }
        public string Version { get; set; }
        public string Release { get; set; }
        public string Source { get; set; }
        public string Purge { get; set; }

        public string GetDeploymentCommandParameters()
        {
            return $"--package --subsystem {Subsystem} --feature {Feature} --version {Version} --release {Release} --source \"{Source}\" --purge {Purge}";
        }
    }
}
