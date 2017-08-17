namespace Stylepit.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class isstock : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Products", "InStock");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Products", "InStock", c => c.Boolean());
        }
    }
}
