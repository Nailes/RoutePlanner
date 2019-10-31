using System;
using System.Collections.Generic;

namespace RoutePlanner
{
    public partial class Stage
    {
        public Stage()
        {
            Comments = new HashSet<Comments>();
        }

        public int IdStage { get; set; }
        public int IdRoutes { get; set; }
        public string Place { get; set; }
        public DateTime? DateStage { get; set; }

        public Routes IdRoutesNavigation { get; set; }
        public ICollection<Comments> Comments { get; set; }
    }
}
