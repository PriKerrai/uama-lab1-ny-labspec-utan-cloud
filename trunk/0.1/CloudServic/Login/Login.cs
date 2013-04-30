
namespace CloudService.LoginService
{
    public class Login
    {

        public bool CreateUser(User user)
        {
            if (UserDB.Instance.Users.Contains(user))
            {
                return false;
            }
            else
            {
                UserDB.Instance.Users.Add(user);
                return true;
            }
        }

        public bool LoginUser(User user)
        {
            return UserDB.Instance.Users.Contains(user) ? true : false;
        }
    }
}
