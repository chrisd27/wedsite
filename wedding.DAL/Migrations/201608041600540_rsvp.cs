namespace wedding.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class rsvp : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Guests", "RSVP", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Guests", "RSVP");
        }
    }
}
