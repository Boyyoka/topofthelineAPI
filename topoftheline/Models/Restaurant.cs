namespace topoftheline
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Restaurant")]
    public partial class Restaurant
    {
   
        public Restaurant()
        {
            Ratings = new HashSet<Rating>();
        }

        public int RestaurantID { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        public int CityID { get; set; }

        public virtual City City { get; set; }

  
        public virtual ICollection<Rating> Ratings { get; set; }
    }
}
