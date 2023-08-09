using System.Reflection;

namespace Backend.AngularSpa
{
    public static class AngularConfig
    {
        public static string Source
        {
            get => Assembly
                .GetExecutingAssembly()
                .GetCustomAttribute<SpaRootAttribute>()
                ?.Value
                .Replace("\\", "/")
                .TrimEnd('/')
                ?? throw new ArgumentNullException("SpaRootAttribute is null");
        }

        public static string PublishedSPAFolderName
        {
            get => Assembly
                .GetExecutingAssembly()
                .GetCustomAttribute<SpaPublishedAttribute>()
                ?.Value
                .Replace("\\", "/")
                .TrimEnd('/')
                ?? throw new ArgumentNullException("SpaPublishedAttribute is null");
        }

        private static string BuildSubfolder
        {
            get => Assembly
                .GetExecutingAssembly()
                .GetCustomAttribute<SpaDistBuildAttribute>()
                ?.Value
                .Replace("\\", "/")
                .TrimEnd('/')
                .TrimStart('/')
                .Insert(0, "/")
                ?? throw new ArgumentNullException("SpaRootAttribute is null");
        }

        private static string? tryFindOutput()
        {
            var outputSubdir = "/" + ClientBuildOutput.Replace("\\", "/");
            var curDir = Directory.GetCurrentDirectory().Replace("\\", "/");

            if (Directory.Exists(curDir + outputSubdir))
                return ClientBuildOutput;

            return null;
        }

        private static string? tryFindPublished()
        {
            var publishSubdir = "/" + PublishedSpaStatic.Replace("\\", "/");
            var asmDir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)?.Replace("\\", "/");
            var asmDirRelease = asmDir?.Replace("Debug", "Release");

            if (Directory.Exists(asmDir + publishSubdir))
                return asmDir + publishSubdir;

            if (Directory.Exists(asmDirRelease + publishSubdir))
                return asmDirRelease + publishSubdir;

            if (Directory.Exists(asmDir + "/publish" + publishSubdir))
                return asmDir + "/publish" + publishSubdir;

            if (Directory.Exists(asmDirRelease + "/publish" + publishSubdir))
                return asmDirRelease + "/publish" + publishSubdir;

            return null;
        }

        public static string SpaStaticRoot
        {
            get
            {
                var output = tryFindOutput();
                if (!string.IsNullOrEmpty(output)) return output;

                var published = tryFindPublished();
                if (!string.IsNullOrEmpty(published)) return published;

                throw new DirectoryNotFoundException("No SPA static root dir");
            }
        }

        private static string ClientBuildOutput { get => Source + BuildSubfolder; }

        private static string PublishedSpaStatic { get => PublishedSPAFolderName + BuildSubfolder; }
    }
}