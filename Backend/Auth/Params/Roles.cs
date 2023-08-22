namespace Backend.Auth.Params
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
    }
}