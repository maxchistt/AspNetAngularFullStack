using Backend.Shared;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Reflection;

namespace Backend.AngularSpa
{
    public static class AngularConfig
    {
        public static string Source
        {
            get => "../Frontend/";
        }

        public static string PublishedSPAFolderName
        {
            get => "SPA/";
        }

        private static string SpaBuildSubfolder
        {
            get => "dist/build";
        }

        private static string? tryFindOutput()
        {
            var outputSubdir = "/" + ClientBuildOutput.Replace("\\", "/");
            var curDir = Directory.GetCurrentDirectory().Replace("\\", "/");

            if (EnvConfig.IsDebug)
            {
                var d = JsonConvert.SerializeObject(Subdirs(curDir), Formatting.Indented);
                Console.WriteLine(d);
                Debug.WriteLine(d);
            }

            if (Directory.Exists(curDir + outputSubdir))
                return ClientBuildOutput;

            return null;
        }

        private static string? tryFindPublished()
        {
            var publishSubdir = "/" + PublishedSpaStatic.Replace("\\", "/");
            var asmDir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)?.Replace("\\", "/");
            var asmDirRelease = asmDir?.Replace("Debug", "Release");

            var d = JsonConvert.SerializeObject(Subdirs(asmDir ?? throw new DirectoryNotFoundException("No Assembly dir")), Formatting.Indented);

            Console.WriteLine(d);
            Debug.WriteLine(d);

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

        public static Dictionary<string, object> Subdirs(string dir)
        {
            var dict = new Dictionary<string, object>();
            Directory.GetDirectories(dir).ToList().ForEach(d => dict[d] = Subdirs(d));
            return dict;
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

        private static string ClientBuildOutput { get => Source + SpaBuildSubfolder; }

        private static string PublishedSpaStatic { get => PublishedSPAFolderName + SpaBuildSubfolder; }
    }
}