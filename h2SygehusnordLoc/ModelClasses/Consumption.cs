namespace h2SygehusnordLoc
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Consumption")]
    public partial class Consumption
    {
        public int ID { get; set; }

        public int consumption_ID { get; set; }

        public int? consumption_type { get; set; }

        public int? consumption_value { get; set; }

        public DateTime? created_at { get; set; }

        public int? room_ID { get; set; }

        public virtual Room Room { get; set; }
    }
}
