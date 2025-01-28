using Interface_OnlineShop3.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interface_OnlineShop3.Users.Exceptions
{
    internal class NullAdminException : Exception
    {
        public NullAdminException() : base(ExceptionMessages.NullAdminException)
        {

        }
    }
}
