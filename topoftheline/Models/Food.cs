namespace topoftheline
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Food")]
    public partial class Food
    {
      
        public Food()
        {
            Ratings = new HashSet<Rating>();
        }

        public int FoodID { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [Required]
        [StringLength(50)]
        public string Description { get; set; }

        
        public virtual ICollection<Rating> Ratings { get; set; }
    }
}
