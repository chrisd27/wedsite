namespace wedding.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class nullableDateTIme : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Guests", "RSVPTimestampInitial", c => c.DateTime());
            AlterColumn("dbo.Guests", "RSVPTimestamp", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Guests", "RSVPTimestamp", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Guests", "RSVPTimestampInitial", c => c.DateTime(nullable: false));
        }
    }
}
