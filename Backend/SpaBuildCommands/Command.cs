using System.Diagnostics;

namespace Backend.BuildCommands
{
    internal class Command : IDisposable
    {
        private Process pr;
        private string? workDir;
        private string command;

        public Command(string command, string? workDir = null)
        {
            this.command = command;
            this.workDir = workDir;
            pr = new Process();
        }

        ~Command()
        {
            Dispose();
        }

        public Task Execute(bool wait = true)
        {
            return Task.Run(async () =>
            {
                pr.StartInfo = new ProcessStartInfo("cmd")
                {
                    Arguments = $"/c \"{command}\"",
                    UseShellExecute = false,
                    RedirectStandardOutput = true,
                };

                if (!string.IsNullOrEmpty(workDir)) pr.StartInfo.WorkingDirectory = workDir;

                pr.OutputDataReceived += (sender, args) => Console.WriteLine($"[{command}]" + args.Data);

                pr.Start();
                pr.BeginOutputReadLine();
                if (wait)
                    await pr.WaitForExitAsync();
            });
        }

        public void Dispose()
        {
            pr.Close();
            pr.Dispose();
        }
    }
}