using System.Collections.Generic;
using Nancy.Security;

namespace Fate.Common.Data
{
    public class WebUserData : IUserIdentity
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public IEnumerable<string> Claims { get; set; }
        public bool Remember { get; set; }
    }
}
