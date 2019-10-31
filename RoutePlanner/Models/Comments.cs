using System;

namespace RoutePlanner
{
    public partial class Comments
    {
        public int IdComments { get; set; }
        public int IdStage { get; set; }
        public string Note { get; set; }
        public DateTime? DateNote { get; set; }

        public Stage IdStageNavigation { get; set; }
    }
}
