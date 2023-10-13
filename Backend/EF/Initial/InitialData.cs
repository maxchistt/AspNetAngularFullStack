using Backend.Models;
using Backend.Shared.AuthParams;

namespace Backend.EF.Initial
{
    public static class InitialData
    {
        public static readonly User[] Users = {
            new User() { Id = 1, Email = "Admin", Password = "Admin", Role = Roles.Admin },
            new User() { Id = 2, Email = "Manager", Password = "Manager", Role = Roles.Worker },
            new User() { Id = 3, Email = "Client", Password = "Client", Role = Roles.Client },
        };
    }
}