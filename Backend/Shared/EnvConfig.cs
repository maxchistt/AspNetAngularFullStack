namespace Backend.Shared
{
    public static class EnvConfig
    {
        public static bool IsDebug
        {
            get =>
#if DEBUG
                true;
#else
                false;
#endif
        }
    }
}