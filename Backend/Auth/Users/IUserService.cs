namespace Backend.Auth.Users
{
    public interface IUserService
    {
        public User? FindPerson(LoginDTO data);
    }
}