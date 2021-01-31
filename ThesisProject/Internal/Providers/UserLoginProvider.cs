using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThesisProject.Internal.Interfaces;
using ThesisProject.Internal.ViewModels;

namespace ThesisProject.Internal.Providers
{
    internal class UserLoginProvider: IUserLoginProvider
    {
        private UserViewModel _user;

        public UserViewModel User
        {
            get
            {
                if (_user is null)
                {
                    throw new InvalidOperationException("The user does not set.");
                }

                return _user;
            }

            set
            {
                if (value is null)
                {
                    throw new ArgumentException("User can't be null");
                }

                _user = value;
            }
        }
    }
}
