using Backend.Auth.Models;
using Backend.Auth.Params;

namespace Backend.EF
{
    public static class InitialData
    {
        public static readonly User[] Users = {
            new User("tom@gmail.com", "12345", Roles.Admin),
            new User("bob@gmail.com", "55555", Roles.Client),
        };
    }
}