using Domain.Models;
using System;
using System.Diagnostics;

namespace Domain.Wrappers
{
    internal static class DeploymentProcessWrapper
    {
        public static DeploymentProcess CreateRunningProcess(string commandParameters, DeploymentProcess deploymentProcess)
        {
            deploymentProcess.Process.StartInfo.FileName = "./dummy/location/deploymentSystem.exe";
            deploymentProcess.Process.StartInfo.Arguments = commandParameters;
            deploymentProcess.Process.StartInfo.UseShellExecute = false;
            deploymentProcess.Process.StartInfo.RedirectStandardOutput = true;
            deploymentProcess.Process.StartInfo.RedirectStandardError = true;

            deploymentProcess.Process.EnableRaisingEvents = true;
            deploymentProcess.Process.OutputDataReceived += new DataReceivedEventHandler(deploymentProcess.OutputReceived);
            deploymentProcess.Process.ErrorDataReceived += new DataReceivedEventHandler(deploymentProcess.ErrorReceived);
            deploymentProcess.Process.Exited += new EventHandler(deploymentProcess.OnComplete);

            deploymentProcess.Process.Start();
            deploymentProcess.Process.BeginErrorReadLine();
            deploymentProcess.Process.BeginOutputReadLine();

            return deploymentProcess;
        }
    }
}
