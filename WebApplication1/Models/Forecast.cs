namespace WebApplication1.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Forecast")]
    public partial class Forecast
    {
        public int Id { get; set; }

        public int FactoryID { get; set; }

        public DateTime Time { get; set; }

        public double? Capacity { get; set; }

        public double Ghi { get; set; }

        public double EnviromentTemp { get; set; }
    }
}
