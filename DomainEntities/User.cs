using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainEntities
{
    public class User: IEquatable<User>
    {
        public string Login { get; set; }
        public string Password { get; set; }

        public bool Equals(User other)
        {
            return Login == other.Login &&
                   Password == other.Password;
        }

        public override bool Equals(object obj)
        {
            return obj is User user &&
                   Login == user.Login &&
                   Password == user.Password;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Login, Password);
        }

        public static bool operator ==(User left, User right)
        {
            return EqualityComparer<User>.Default.Equals(left, right);
        }

        public static bool operator !=(User left, User right)
        {
            return !(left == right);
        }
    }
}
