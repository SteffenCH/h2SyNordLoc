namespace h2SygehusnordLoc
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Maintenance")]
    public partial class Maintenance
    {
        public int ID { get; set; }

        public int? room_ID { get; set; }

        public int? maintenance_ID { get; set; }

        public int? type { get; set; }

        public bool? broken { get; set; }

        public DateTime? created_at { get; set; }

        public virtual MaintenanceType MaintenanceType { get; set; }

        public virtual Room Room { get; set; }
    }
}
