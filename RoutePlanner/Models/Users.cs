using System;
using System.Collections.Generic;

namespace RoutePlanner
{
    public partial class Users
    {
        public Users()
        {
            Routes = new HashSet<Routes>();
        }

        public int IdUser { get; set; }
        public string Login { get; set; }
        public string Pass { get; set; }
        public string Email { get; set; }

        public ICollection<Routes> Routes { get; set; }
    }
}
