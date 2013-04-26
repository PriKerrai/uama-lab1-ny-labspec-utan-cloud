
namespace CloudService.LoginService
{
    public class Login
    {
        LoginDB loginDB = LoginDB.Instance;

        public bool CreateUser(User user)
        {
            if (loginDB.users.Contains(user))
            {
                return false;
            }
            else
            {
                loginDB.users.Add(user);
                return true;
            }
        }

        public bool LoginUser(User user)
        {
            return loginDB.users.Contains(user) ? true: false;
        }
    }
}
