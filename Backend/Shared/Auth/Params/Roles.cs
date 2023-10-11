namespace Backend.Shared.Auth.Params
{
    public static class Roles
    {
        public const string Client = "client";
        public const string Admin = "admin";
        public const string Worker = "worker";

        public static class Combinations
        {
            public const string AdminAndWorker = $"{Admin},{Worker}";
            public const string AllAllowed = $"{Client},{Admin},{Worker}";
        }

        public enum Enum
        { Client, Admin, Worker }

        public static string GetRoleByEnum(Enum type) => dict[type];

        private static readonly Dictionary<Enum, string> dict = new()
        { { Enum.Worker, Worker }, { Enum.Admin, Admin }, { Enum.Client, Client } };
    }
}