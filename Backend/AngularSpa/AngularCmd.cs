using System.Diagnostics;

namespace Backend.AngularSpa
{
    public static class AngularCmd
    {
        public static Process? StartAngularDev(string workdir)
        {
            var p = new ProcessStartInfo();
            p.UseShellExecute = true;
            p.Arguments = "/C npm start -- --configuration development --open";
            p.FileName = "cmd";
            p.WorkingDirectory = workdir;

            return Process.Start(p);
        }
    }
}
