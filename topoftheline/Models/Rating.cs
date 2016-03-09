namespace topoftheline
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Rating")]
    public partial class Rating
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int FoodID { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int RestaurantID { get; set; }

        [Key]
        [Column("Rating", Order = 2)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Rating1 { get; set; }

        public virtual Food Food { get; set; }

        public virtual Restaurant Restaurant { get; set; }
    }
}
