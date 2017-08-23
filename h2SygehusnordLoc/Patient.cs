namespace h2SygehusnordLoc
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Patient")]
    public partial class Patient
    {
        public int ID { get; set; }

        [StringLength(64)]
        public string name { get; set; }

        [StringLength(11)]
        public string cpr_nr { get; set; }
    }
}
