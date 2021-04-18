using System;
using System.Diagnostics;

namespace Domain.Models
{
    public class DeploymentProcess
    {
        private Action _changeState;

        public string Output { get; set; } = string.Empty;
        public string Error { get; set; } = string.Empty;
        public bool Complete { get; set; }
        public bool Success { get; set; }
        public Process Process { get; set; } = new Process();

        public virtual void ErrorReceived(object sender, DataReceivedEventArgs e)
        {
            Error += $"{e.Data}\n";
        }

        public virtual void OutputReceived(object sender, DataReceivedEventArgs e)
        {
            Output += $"{e.Data}\n";
        }

        public virtual void OnComplete(object sender, EventArgs e)
        {
            var process = sender as Process;
            Success = process.ExitCode == 0;
            Complete = true;
            process.Close();
            process.Dispose();
            _changeState();
        }

        public void AttachChangeStateDelegate(Action changeState)
        {
            _changeState = changeState;
        }
    }
}
