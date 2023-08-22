namespace Backend.Auth
{
    public static class Roles
    {
        public const string User = "user";
        public const string Admin = "admin";
    }

    public interface IUserService
    {
        public Person? FindPerson(LoginDTO data);
    }

    public class UserService : IUserService
    {
        public readonly Role adminRole = new Role(Roles.Admin);
        public readonly Role userRole = new Role(Roles.User);

        public List<Person> people;

        public UserService()
        {
            people = new List<Person>
            {
                new Person("tom@gmail.com", "12345", adminRole),
                new Person("bob@gmail.com", "55555", userRole),
            };
        }

        public Person? FindPerson(LoginDTO data)
        {
            return people.Find(p => p.Email == data.Email && p.Password == data.Password);
        }
    }

    public class Person
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public Role Role { get; set; }

        public Person(string email, string password, Role role)
        {
            Email = email;
            Password = password;
            Role = role;
        }
    }

    public record Role(string Name);
}