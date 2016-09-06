namespace wedding.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RSVPTimestamp : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Guests", "RSVPTimestamp", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Guests", "RSVPTimestamp");
        }
    }
}
