namespace wedding.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RSVPinitial : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Guests", "RSVPTimestampInitial", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Guests", "RSVPTimestampInitial");
        }
    }
}
