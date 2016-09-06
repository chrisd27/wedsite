namespace wedding.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class increaseWeddingInfoSize : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Weddings", "weddingInformation", c => c.String(maxLength: 510));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Weddings", "weddingInformation", c => c.String(maxLength: 255));
        }
    }
}
