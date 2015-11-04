namespace AppConsig.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AvisoModel : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Aviso", "Texto", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Aviso", "Texto", c => c.String(nullable: false, maxLength: 256));
        }
    }
}
