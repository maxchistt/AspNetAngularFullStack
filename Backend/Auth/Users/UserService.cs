namespace Backend.Auth.Users
{
    public class UserService : IUserService
    {
        public readonly Role adminRole = new Role(Roles.Admin);
        public readonly Role userRole = new Role(Roles.User);

        public List<User> users;

        public UserService()
        {
            users = new List<User>
            {
                new User("tom@gmail.com", "12345", adminRole),
                new User("bob@gmail.com", "55555", userRole),
            };
        }

        public User? FindPerson(LoginDTO data)
        {
            return users.Find(p => p.Email == data.Email && p.Password == data.Password);
        }
    }
}