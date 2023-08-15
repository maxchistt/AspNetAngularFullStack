using System;
using System.Diagnostics;

namespace Backend.AngularSpa
{
    public class AngularDevServer : IDisposable
    {
        private Process? _process;
        private readonly ProcessStartInfo _info;
        public AngularDevServer(string workdir, IHostApplicationLifetime appLifetime)
        {
            var p = new ProcessStartInfo("cmd");
            p.UseShellExecute = true;
            p.Arguments = $"/c npm start -- --configuration development --open";
            p.WorkingDirectory = workdir;
            _info = p;

            AppDomain.CurrentDomain.ProcessExit += new EventHandler((sen, args) => this.Dispose());
            AppDomain.CurrentDomain.DomainUnload += new EventHandler((sen, args) => this.Dispose());

            appLifetime.ApplicationStopped.Register(this.Dispose);
            appLifetime.ApplicationStopping.Register(this.Dispose);
        }

        public void Dispose()
        {
            if(_process is not null)
            {
                Console.WriteLine("!!!!!!! Closing");
                Debug.WriteLine("!!!!!!! Closing");
                
                _process?.CloseMainWindow();
                _process?.Kill(entireProcessTree: true);
                _process = null;

                Console.WriteLine("!!!!!!! Closed");
                Debug.WriteLine("!!!!!!! Closed");
            }

        }

        public void Start()
        {
            if (_process is not null) return;

            _process = Process.Start(_info);
        }
    }
}
