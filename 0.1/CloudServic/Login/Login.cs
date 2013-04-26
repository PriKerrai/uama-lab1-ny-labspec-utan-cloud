
namespace CloudService.LoginService
{
    public class Login
    {
        LoginDB loginDB = LoginDB.Instance;

        public bool CreateUser(User user)
        {
            if (loginDB.Users.Contains(user))
            {
                return false;
            }
            else
            {
                loginDB.Users.Add(user);
                return true;
            }
        }

        public bool LoginUser(User user)
        {
            return loginDB.Users.Contains(user) ? true : false;
        }
    }
}
