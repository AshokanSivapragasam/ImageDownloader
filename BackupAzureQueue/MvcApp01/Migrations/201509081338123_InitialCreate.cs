namespace MvcApp01.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Restaurants",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        City = c.String(),
                        Country = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.RestaurantReviews",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ReviewerName = c.String(),
                        Rating = c.Int(nullable: false),
                        ReviewBody = c.String(),
                        Restaurant_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Restaurants", t => t.Restaurant_Id)
                .Index(t => t.Restaurant_Id);
            
        }
        
        public override void Down()
        {
            DropIndex("dbo.RestaurantReviews", new[] { "Restaurant_Id" });
            DropForeignKey("dbo.RestaurantReviews", "Restaurant_Id", "dbo.Restaurants");
            DropTable("dbo.RestaurantReviews");
            DropTable("dbo.Restaurants");
        }
    }
}
