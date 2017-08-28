amespace h2SygehusnordLoc
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Security")]
    public partial class Security
    {
        public int ID { get; set; }

        public int? card_type { get; set; }

        public int? room_ID { get; set; }

        public int? department_ID { get; set; }

        public int? patient_ID { get; set; }

        public DateTime? created_at { get; set; }
    }
}
