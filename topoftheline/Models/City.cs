namespace topoftheline
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("City")]
    public partial class City
    {
   
        public City()
        {
            Restaurants = new HashSet<Restaurant>();
        }

        public int CityID { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

     
        public virtual ICollection<Restaurant> Restaurants { get; set; }
    }
}
