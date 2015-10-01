namespace AppConsig.Dados.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class add_nullable_parent_id : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Permissao", "ParenteId", c => c.Guid());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Permissao", "ParenteId", c => c.Guid(nullable: false));
        }
    }
}
