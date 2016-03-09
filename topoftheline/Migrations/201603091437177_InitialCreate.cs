namespace topoftheline.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.City",
                c => new
                    {
                        CityID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.CityID);
            
            CreateTable(
                "dbo.Restaurant",
                c => new
                    {
                        RestaurantID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50, unicode: false),
                        CityID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.RestaurantID)
                .ForeignKey("dbo.City", t => t.CityID)
                .Index(t => t.CityID);
            
            CreateTable(
                "dbo.Rating",
                c => new
                    {
                        FoodID = c.Int(nullable: false),
                        RestaurantID = c.Int(nullable: false),
                        Rating = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.FoodID, t.RestaurantID, t.Rating })
                .ForeignKey("dbo.Food", t => t.FoodID)
                .ForeignKey("dbo.Restaurant", t => t.RestaurantID)
                .Index(t => t.FoodID)
                .Index(t => t.RestaurantID);
            
            CreateTable(
                "dbo.Food",
                c => new
                    {
                        FoodID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50, unicode: false),
                        Description = c.String(nullable: false, maxLength: 50, unicode: false),
                    })
                .PrimaryKey(t => t.FoodID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Restaurant", "CityID", "dbo.City");
            DropForeignKey("dbo.Rating", "RestaurantID", "dbo.Restaurant");
            DropForeignKey("dbo.Rating", "FoodID", "dbo.Food");
            DropIndex("dbo.Rating", new[] { "RestaurantID" });
            DropIndex("dbo.Rating", new[] { "FoodID" });
            DropIndex("dbo.Restaurant", new[] { "CityID" });
            DropTable("dbo.Food");
            DropTable("dbo.Rating");
            DropTable("dbo.Restaurant");
            DropTable("dbo.City");
        }
    }
}
