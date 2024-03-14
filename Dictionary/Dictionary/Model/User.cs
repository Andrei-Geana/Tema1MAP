using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dictionary.Model
{
    public class User
    {
        [JsonProperty("username")]
        private string _username;
        [JsonProperty("password")]
        private string _password;
        public User(string username, string password)
        {
            Username = username;
            Password = password;
        }

        [JsonIgnore]
        public string Username { get => _username; set => _username = value; }
        [JsonIgnore]
        public string Password { get => _password; set => _password = value; }

        public override bool Equals(object obj)
        {
            return obj is User user &&
                   Username == user.Username &&
                   Password == user.Password;
        }

        public override int GetHashCode()
        {
            int hashCode = -2019462723;
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Username);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Password);
            return hashCode;
        }
    }
}
