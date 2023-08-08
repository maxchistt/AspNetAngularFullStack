using Backend.SpaConfig;

namespace Backend.BuildCommands
{
    public static class AngularBuildCommandsExtension
    {
        public static void AngularScriptsPrepare(this WebApplication app)
        {
            string workDir = Directory.GetCurrentDirectory() + $"\\{AngularConfig.Source}\\";

            npmInstall(workDir).Wait();

            if (!app.Environment.IsDevelopment())
                npmRunBuild(workDir).Wait();
        }

        private static async Task npmInstall(string workDir)
        {
            using var c = new Command("npm install", workDir);
            await c.Execute();
        }

        private static async Task npmRunBuild(string workDir)
        {
            using var c = new Command("npm run build", workDir);
            await c.Execute();
        }
    }
}