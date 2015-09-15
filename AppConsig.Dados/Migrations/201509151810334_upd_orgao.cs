namespace AppConsig.Dados.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class upd_orgao : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Orgao", "Nome", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Orgao", "Nome", c => c.String());
        }
    }
}
