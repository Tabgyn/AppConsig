namespace AppConsig.Dados.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _1556 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Auditoria", "SessionId", c => c.String());
            AddColumn("dbo.Auditoria", "Controle", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Auditoria", "Controle");
            DropColumn("dbo.Auditoria", "SessionId");
        }
    }
}
