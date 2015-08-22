namespace AppConsig.Dados.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _1249 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Usuario", "Foto", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Usuario", "Foto", c => c.Binary());
        }
    }
}
