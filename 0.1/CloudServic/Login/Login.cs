
namespace CloudService.LoginService
{
    public class Login
    {

        public bool CreateUser(User user)
        {
            UserDB userDB = UserDB.Instance;
            if (userDB.ContainsUser(userDB.GetUsers(), user.UserID))
            {
                return false;
            }
            else
            {
                UserDB.Instance.Users.Add(user);
                UserDB.Instance.StoreUser(user);
                return true;
            }
        }

        public bool LoginUser(User user)
        {
            UserDB userDB = UserDB.Instance;
            return userDB.ContainsUser(userDB.GetUsers(), user.UserID) ? true : false;
        }
    }
}
