using System;
using System.Collections.Generic;

namespace RoutePlanner
{
    public partial class Routes
    {
        public Routes()
        {
            Stage = new HashSet<Stage>();
        }

        public int IdRoutes { get; set; }
        public int IdUser { get; set; }
        public string Title { get; set; }
        public DateTime DateRoutes { get; set; }

        public Users IdUserNavigation { get; set; }
        public ICollection<Stage> Stage { get; set; }
    }
}
