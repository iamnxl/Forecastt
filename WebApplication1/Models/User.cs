namespace WebApplication1.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class User
    {
        [Required]
        [Key]
        [Column(Order = 0)]
        public string Account { get; set; }

        [Required]
        [Key]
        [Column(Order = 1)]
        public string Password { get; set; }
        [Required]
        [EmailAddress]
        [Key]
        [Column(Order = 2)]
        [StringLength(100)]
        public string Email { get; set; }
    }
}
