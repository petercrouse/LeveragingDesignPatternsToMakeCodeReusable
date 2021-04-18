namespace App.Common.Enumerations
{
    public enum DeploymentState
    {
        Start = 0,
        Running = 1,
        Retry = 2,
        Complete = 3,
        CheckStaleExecution = 4,
    }
}
