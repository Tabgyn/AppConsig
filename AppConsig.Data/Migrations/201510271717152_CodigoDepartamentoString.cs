namespace AppConsig.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CodigoDepartamentoString : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Departamento", "CodigoDepartamento", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Departamento", "CodigoDepartamento", c => c.Long(nullable: false));
        }
    }
}
