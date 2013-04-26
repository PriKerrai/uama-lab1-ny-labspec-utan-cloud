
namespace CloudService.LoginService
{
    public class User
    {
        public string id { get; set; }
        public string pw { get; set; }

        public User(string id, string pw)
        {
            this.id = id;
            this.pw = pw;
        }
    }
}
