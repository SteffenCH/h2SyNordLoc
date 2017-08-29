namespace h2SygehusnordLoc
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Login")]
    public partial class Login
    {
        public int ID { get; set; }

        [Required]
        [StringLength(64)]
        public string username { get; set; }

        [Required]
        [StringLength(64)]
        public string password_hash { get; set; }

        [StringLength(4)]
        public string pin { get; set; }

        public byte? privilige_level { get; set; }
    }
}
