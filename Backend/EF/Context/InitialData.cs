using Backend.EF.Models;
using Backend.Shared.Auth.Params;

namespace Backend.EF.Context
{
    public static class InitialData
    {
        public static readonly User[] Users = {
            new User() {Id = 1, Email = "tom@gmail.com", Password = "12345", Role = Roles.Admin },
            new User() {Id = 2, Email = "bob@gmail.com", Password = "55555", Role = Roles.Client },
        };
    }
}